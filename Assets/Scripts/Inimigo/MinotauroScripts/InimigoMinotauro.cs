using UnityEngine;

public class InimigoMinotauro : ControladorEstado
{
    void Start()
    {
        MudarEstado(typeof(InimigoMinotauroMovimento));
    }

    public void ReceberDano()
    {
        MudarEstado(typeof(InimigoMinotauroAtordoado));
    }

    public void Morrer()
    {
        MudarEstado(typeof(InimigoGenericoMorrer));
    }
}
