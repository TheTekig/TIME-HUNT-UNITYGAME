using System;
using UnityEngine;

public class MinhocaAtaque : InimigoEstado
{
    [SerializeField] private ControladorHitBox controladorHitbox;
    private Animator animator;
    private Transform player;
    [SerializeField] private float intervaloAtaque;
    private float contador = 0;
    [SerializeField] private Projetil bolaDeFogo;
    [SerializeField] private Transform pontoDeLancamento;
    [SerializeField] private int dano;
    [SerializeField] private int velocidade;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void OnEnter()
    {
    }

    public override void OnExit()
    {
    }

    public override Type OnUpdate()
    {
        girarInimigo();

        if (controladorHitbox.ExisteAlvosDisponiveis())
        {
            contador += Time.deltaTime;
            if (contador >= intervaloAtaque)
            {
                animator.SetTrigger("atacar");
                contador = 0;
            }
        }
        return null;
    }

    public void RealizarAtaque()
    {

        Projetil projetil = Instantiate(bolaDeFogo, pontoDeLancamento.position, Quaternion.identity);

        projetil.IniciarLancamento(player, velocidade, dano, true);
    }

    private void girarInimigo()
    {
        if (player.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
