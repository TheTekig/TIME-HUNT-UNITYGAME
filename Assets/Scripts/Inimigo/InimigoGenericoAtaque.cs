using System;
using UnityEngine;

public class InimigoGenericoAtaque : InimigoEstado
{
    [SerializeField] private int dano = 10;
    [SerializeField] private ControladorHitBox controladorHitbox;
    [SerializeField] private float tempoReacao;

    [SerializeField] private AudioSource ataqueEspada;

    private Animator animator;
    private Type proximoEstado = null;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override void OnEnter()
    {
        Invoke(nameof(IniciarAtaque), tempoReacao);
    }

    public override void OnExit()
    {
        proximoEstado = null;
        CancelInvoke();
    }

    public override Type OnUpdate()
    {
        return proximoEstado;
    }

    private void IniciarAtaque()
    {
        ataqueEspada.Play();
        animator.SetTrigger("ataqueEspada");
    }

    public void AuferirDano()
    {
        controladorHitbox.AplicarDano(dano);

        Invoke(nameof(FinalizarDano), tempoReacao);
    }

    private void FinalizarDano()
    {
        proximoEstado = typeof(InimigoGenericoMovimento);
    }

 
}
