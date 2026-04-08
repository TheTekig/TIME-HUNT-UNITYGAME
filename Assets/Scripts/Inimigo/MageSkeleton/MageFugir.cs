using UnityEngine;
using System;

public class MageFugir : InimigoEstado
{
    [SerializeField] private float velocidadeFuga = 4.5f;
    [SerializeField] private ControladorHitBox fugaHitbox;

    private Animator animator;
    private Rigidbody2D rb;
    private Transform player;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void OnEnter()
    {
        animator.SetBool("andar", true);
    }

    public override void OnExit()
    {
        rb.linearVelocity = Vector2.zero;
        animator.SetBool("andar", false);
    }

    public override Type OnUpdate()
    {
        
        

        if (!fugaHitbox.ExisteAlvosDisponiveis())
        {
            return typeof(MageAttack);
        }

        RealizarFuga();
        return null;

    }

    private void RealizarFuga()
    {

        float diferencaX = transform.position.x - player.position.x;

        if (diferencaX > 0) 
        {
            rb.linearVelocity = new Vector2(velocidadeFuga, rb.linearVelocity.y);
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else 
        {
            rb.linearVelocity = new Vector2(-velocidadeFuga, rb.linearVelocity.y);
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
}
