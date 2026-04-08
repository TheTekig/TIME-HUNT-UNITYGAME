using UnityEngine;
using System;

public class MageAttack : InimigoEstado
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

    [SerializeField] private AudioSource bolaDeFogoSound;

    [SerializeField] private ControladorHitBox controladorHitBoxFuga;

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
        if(controladorHitBoxFuga.ExisteAlvosDisponiveis())
        {
            contador = 0;
            return typeof(MageFugir);
        }


        if (controladorHitbox.ExisteAlvosDisponiveis())
        {
            girarInimigo();

            contador += Time.deltaTime;
            if (contador >= intervaloAtaque)
            {
                animator.SetTrigger("atacar");
                contador = 0;
            }
        }
        else
        {
            contador = 0;
            return typeof(MageMoviment);
        }
            return null;
    }

    public void RealizarAtaque()
    {
        bolaDeFogoSound.Play();
        Projetil projetil = Instantiate(bolaDeFogo, pontoDeLancamento.position, Quaternion.identity);

        projetil.IniciarLancamento(player, velocidade, dano, true);
    }

    private void girarInimigo()
    {
        if (player.position.x > transform.position.x)
        {
            transform.eulerAngles = new Vector3(0,0,0);
        }
        else if (player.position.x < transform.position.x) 
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
}
