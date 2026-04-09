using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ConfiguracaoQualidade : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI qualityButtonWrite;
    [SerializeField] TMP_InputField fpsLimit;
    [SerializeField] TMP_Dropdown resolutionDropDown;

    List<Resolution> resolucoesUnicas = new List<Resolution>();
    List<string> OpcoesQualidade = new List<string> { "BAIXO", "MEDIO", "ALTO" };

    private int qualityLevel;
    int frameRate = 60;
    int resolucaoSelecionada = 0;

    void Start()
    {
        QualitySettings.vSyncCount = 0;
        qualityLevel = 0;
        qualityButtonWrite.text = OpcoesQualidade[qualityLevel];
        fpsLimit.text = frameRate.ToString() + " FPS";
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

    // OnEndEdit do InputField
    public void LimitarFPS(string input)
    {
        string apenasNumeros = input.Replace("FPS", "").Trim();

        frameRate = (int.TryParse(apenasNumeros, out int novoFPS) && novoFPS > 0) ? novoFPS : 60;

        Application.targetFrameRate = frameRate;
        fpsLimit.text = frameRate.ToString() + " FPS";
    }

    public void AlterarModoTela(bool telaCheia)
    {
        // No Unity 6 Windows: sempre seta resolução junto com o modo
        if (resolucaoSelecionada < resolucoesUnicas.Count)
        {
            Resolution res = resolucoesUnicas[resolucaoSelecionada];
            FullScreenMode modo = telaCheia ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
            Screen.SetResolution(res.width, res.height, modo, res.refreshRateRatio);
        }
    }

    void ConfigurarDropDownResolucao()
    {
        Resolution[] todasResolucoes = Screen.resolutions;
        resolutionDropDown.ClearOptions();
        resolucoesUnicas.Clear();

        List<string> opcoes = new List<string>();
        int resolucaoAtualIndex = 0;

        foreach (Resolution res in todasResolucoes)
        {
            float proporcao = (float)res.width / res.height;
            if (Mathf.Abs(proporcao - 1.777778f) >= 0.01f) continue;

            string opcao = res.width + "x" + res.height;

            if (!opcoes.Contains(opcao))
            {
                opcoes.Add(opcao);
                resolucoesUnicas.Add(res);

                if (res.width == Screen.currentResolution.width &&
                    res.height == Screen.currentResolution.height)
                {
                    resolucaoAtualIndex = opcoes.Count - 1;
                }
            }
        }

        resolutionDropDown.AddOptions(opcoes);
        resolutionDropDown.value = resolucaoAtualIndex;
        resolucaoSelecionada = resolucaoAtualIndex; // salva o índice atual
        resolutionDropDown.RefreshShownValue();
    }

    public void AlterarResolucao(int indiceResolucao)
    {
        if (indiceResolucao < 0 || indiceResolucao >= resolucoesUnicas.Count) return;

        resolucaoSelecionada = indiceResolucao; // salva pra usar no AlterarModoTela também

        Resolution res = resolucoesUnicas[indiceResolucao];

        // Unity 6: passa refreshRateRatio em vez de bool
        Screen.SetResolution(res.width, res.height, Screen.fullScreenMode, res.refreshRateRatio);
    }
}