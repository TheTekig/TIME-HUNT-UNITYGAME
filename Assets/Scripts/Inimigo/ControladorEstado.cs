using UnityEngine;
using System.Collections.Generic;
using System;

public abstract class ControladorEstado : MonoBehaviour
{
    protected InimigoEstado estadoAtual;
    [SerializeField] protected List<InimigoEstado> estadosDisponiveis;

    private void Update()
    {
        var tipoEstado = estadoAtual.OnUpdate();
        MudarEstado(tipoEstado);
    }

    public void MudarEstado(Type estado)
    {
        if (estado != null && estado != estadoAtual?.GetType())
        {
            var novoEstado = estadosDisponiveis.Find(e => e.GetType() == estado);
            if (novoEstado != null)
            {
                estadoAtual?.OnExit();
                estadoAtual = novoEstado;
                estadoAtual.OnEnter();
            }
        }
    }
}
