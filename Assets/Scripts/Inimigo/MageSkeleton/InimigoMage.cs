using UnityEngine;

public class InimigoMage : ControladorEstado
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MudarEstado(typeof(MageMoviment));
    }

    public void ReceberDano()
    {
        MudarEstado(typeof(MageAtordoado));
    }

    public void Morrer()
    {
        MudarEstado(typeof(MageMorrer));
    }
}
