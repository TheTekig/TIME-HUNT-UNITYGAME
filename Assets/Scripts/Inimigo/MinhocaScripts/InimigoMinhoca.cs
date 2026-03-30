using UnityEngine;

public class InimigoMinhoca : ControladorEstado
{
    void Start()
    {
        MudarEstado(typeof(MinhocaAtaque));
    }

    public void morrer()
    {
        MudarEstado(typeof(InimigoGenericoMorrer));
    }
}
