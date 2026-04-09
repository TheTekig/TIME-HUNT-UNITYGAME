using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;


public class ConfiguracaoQualidade : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI qualityButtonWrite;
    [SerializeField] TMP_InputField fpsLimit;
    [SerializeField] TMP_Dropdown resolutionDropDown;
    Resolution[] resolutionsSuportadas;

    List<string> OpcoesQualidade = new List<string> { "BAIXO", "MEDIO", "ALTO" };
    private int qualityLevel;
    int frameRate = 60;

    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Screen.fullScreenMode = FullScreenMode.Windowed;
        qualityLevel = 0;
        qualityButtonWrite.text = OpcoesQualidade[qualityLevel];

        fpsLimit.text = frameRate.ToString() + "FPS";
        Application.targetFrameRate = frameRate;

        ConfigurarDropDownResolucao();
    }

    public void AlterarQualidade()
    {
        qualityLevel++;
        if (qualityLevel >= 3) qualityLevel = 0;   
        
        qualityButtonWrite.text = OpcoesQualidade[qualityLevel];
        QualitySettings.SetQualityLevel(qualityLevel, true);

        QualitySettings.vSyncCount = 0;
            
            
    }

    public void LimitarFPS(string input)
    {
        string apenasNumeros = input.Replace("FPS", "").Trim();
        
        if (int.TryParse(apenasNumeros, out int novoFPS))
        {
            frameRate = novoFPS;
        }
        else
        {
            frameRate = 60;
        }

            Application.targetFrameRate = frameRate;
        fpsLimit.text = frameRate.ToString() + "FPS";
    }

    public void AlterarModoTela(bool telaCheia)
    {
        if (telaCheia)
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }

        
    }

    void ConfigurarDropDownResolucao()
    {

        Resolution[] todasResolucoes = Screen.resolutions;
        resolutionDropDown.ClearOptions();

        List<string> opcoes = new List<string>();
        List<Resolution> resolucoes169 = new List<Resolution>();
        int resolucaoAtualIndex = 0;

        for (int i = 0; i < todasResolucoes.Length; i++)
        {
            float proporcao = (float)todasResolucoes[i].width / todasResolucoes[i].height;

            if (Mathf.Abs(proporcao - 1.777778f) < 0.01f)
            {
                resolucoes169.Add(todasResolucoes[i]);

                string opcao = todasResolucoes[i].width + "x" + todasResolucoes[i].height;

                if (!opcoes.Contains(opcao))
                {
                    opcoes.Add(opcao);
                }

                if (todasResolucoes[i].width == Screen.currentResolution.width && todasResolucoes[i].height == Screen.currentResolution.height)
                {
                    resolucaoAtualIndex = opcoes.Count - 1;
                }
            }
        }

        resolutionsSuportadas = resolucoes169.ToArray();

        resolutionDropDown.AddOptions(opcoes);
        resolutionDropDown.value = resolucaoAtualIndex;
        resolutionDropDown.RefreshShownValue();
    }

    public void AlterarResolucao(int indiceResolucao)
    {
        Resolution res = resolutionsSuportadas[indiceResolucao];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }

    void Update()
    {
        
    }
}
