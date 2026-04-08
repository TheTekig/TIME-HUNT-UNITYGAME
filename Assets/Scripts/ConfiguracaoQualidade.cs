using UnityEngine;

public class ConfiguracaoQualidade : MonoBehaviour
{
    void Start()
    {
        QualitySettings.vSyncCount = 0;
    }

    public void AlterarQualidade(int nivel)
    {
        QualitySettings.SetQualityLevel(nivel, true);
        QualitySettings.vSyncCount = 0;
    }

    public void LimitarFPS(int fps)
    {
        Application.targetFrameRate = fps;
    }

    public void AlterarModoTela(bool telaCheia)
    {
        Screen.fullScreen = telaCheia;
    }

    void Update()
    {
        
    }
}
