using UnityEngine;
using System;

public class MageMorrer : InimigoEstado
{
    [SerializeField] private float tempoMorte = 3f;
    [SerializeField] private GameObject efeitoMorrer;
    [SerializeField] private int tempoExtra;
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override void OnEnter()
    {
        ControladorPartida.Instance.NovoInimigoDerrotado(tempoExtra);

        Instantiate(efeitoMorrer, transform.position, transform.rotation);

        animator.SetTrigger("morrer");
        Destroy(gameObject, tempoMorte);
    }

    public override void OnExit()
    {
    }

    public override Type OnUpdate()
    {
        return null;
    }
}
