using UnityEngine;
using System.Collections;
using TMPro;


public class ControladorPartida : MonoBehaviour
{
    private int tempoRestante = 60;
    private int tempoTotalPartida;
    private int inimigosDerrotados;
    private int danoSofrido;
    private int chavesColetadas;
    [SerializeField] private TMP_Text tempoRestanteText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text chavesColetadasText;
    [SerializeField] private TMP_Text inimigosDerrotadosGameOverText;
    [SerializeField] private TMP_Text TempoJogadoGameOverText;
    [SerializeField] private TMP_Text DanoSofridoGameOverText;
    [SerializeField] private TMP_Text chavesColetadasGameOverText;
    [SerializeField] private TMP_Text scoreGameOverText;

    [SerializeField] private AudioSource gameOverAudioSource;


    public static ControladorPartida Instance { get; private set; }


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        StartCoroutine(ContadorTempo());
    }

    private IEnumerator ContadorTempo()
    {
        while (tempoRestante > 0)
        {
            yield return new WaitForSeconds(1f);
            tempoRestante--;
            tempoTotalPartida++;
            tempoRestanteText.text = tempoRestante + "s";
        }
        finalizarPartida(false);
    }

    public void finalizarPartida(bool vitoria)
    {
        gameOverAudioSource.Play();

        Time.timeScale = 0;
        gameOverPanel.SetActive(true);

        TempoJogadoGameOverText.text = tempoTotalPartida + "s";
        inimigosDerrotadosGameOverText.text = inimigosDerrotados.ToString();
        DanoSofridoGameOverText.text = danoSofrido.ToString();
        chavesColetadasGameOverText.text = chavesColetadas + "/3";

        if (vitoria)
        {
            scoreGameOverText.text = "SCORE: " + CalcularScore();
        }
        else
        {
            scoreGameOverText.text = "SCORE: 0000!";
        }
    }

    private int CalcularScore()
    {
        return (tempoTotalPartida * 10) + (inimigosDerrotados * 100) + (chavesColetadas * 500) - (danoSofrido * 2);
    }

    public void NovoInimigoDerrotado(int TempoExtra)
    {
        tempoRestante += TempoExtra;
        inimigosDerrotados++;
    }

    public void adicionarDanoSofrido(int danoRecebido, int vidaAtual)
    {
        danoSofrido += danoRecebido;
    }

    public void NovaChaveColetada()
    {
        chavesColetadas++;
        chavesColetadasText.text = chavesColetadas + "/3";
        if (chavesColetadas >= 3)
        {
            finalizarPartida(true);
        }
    }


}
