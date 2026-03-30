using UnityEngine;

public class InimigoGenerico : ControladorEstado
{    
    void Start()
    {
        MudarEstado(typeof(InimigoGenericoMovimento));
    }

    public void ReceberDano()
    {
        MudarEstado(typeof(InimigoGenericoAtordoado));
    }

    public void Morrer()
    {
        MudarEstado(typeof(InimigoGenericoMorrer));
    }
}
