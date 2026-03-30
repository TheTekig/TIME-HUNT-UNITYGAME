using UnityEngine;
using UnityEngine.UI;

public class JogadorUI : MonoBehaviour
{
    //variaveis de referencia das habilidades do personagem

    [SerializeField] private Image espadaProgresso;
    [SerializeField] private Image dashProgresso;
    [SerializeField] private Image bolaFogoProgresso;

    [SerializeField] private Slider barraVidaSlider;


    public void AtualizarVidaMaxima(int vidaAtual, int vidaMaxima)
    {
        barraVidaSlider.maxValue = vidaMaxima;
        barraVidaSlider.value = vidaAtual;
    }

    public void AtualizarVidaAtual(int modificador, int vidaAtual)
    {
        barraVidaSlider.value = vidaAtual;
    }
    public void AtualizarProgressoEspada(float progresso)
    {
        espadaProgresso.fillAmount = progresso;
    }

    public void AtualizarBolaFogoProgresso(float progresso)
    {
        bolaFogoProgresso.fillAmount = progresso;
    }

    public void AtualizarProgressoDash(float progresso)
    {
        dashProgresso.fillAmount = progresso;

    }

}
