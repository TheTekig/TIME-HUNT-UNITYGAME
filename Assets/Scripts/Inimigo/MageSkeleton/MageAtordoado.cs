using UnityEngine;
using System;

public class MageAtordoado : InimigoEstado
{
    [SerializeField] private float tempoAtordoamento;
    private Animator animator;
    private float contador;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override void OnEnter()
    {
        animator.SetTrigger("receberDano");
    }

    public override void OnExit()
    {
        contador = 0;
    }

    public override Type OnUpdate()
    {
        contador += Time.deltaTime;
        if (contador > tempoAtordoamento)
        {
            return typeof(MageFugir);
        }
        return null;
    }
}
