using System.Collections;
using UnityEngine;

public class Movimento : MonoBehaviour
{
    //Variavel para acessar o componente RigidBody associado ao personagem
    private Rigidbody2D rb;
    private float entradaHorizontal;
    private bool estaNoChao;
    private DirecaoPersonagem direcaoAtual;

    //Variavel que indica se o personagem tem direito a um salto extra
    private bool saltoExtra;

    //Variavel que libera o impulso do personagem
    private bool dashLiberado = true;
    //Variavel responsavel por parar a movimentacao do personagem durante o dash
    private bool executandoDash = false;


    private Animator animator;
    //SerializeField faz com que consigamos alterar o valor desta variavel pela unity
    [SerializeField] private float velocidade = 5f;
    [SerializeField] private Transform pePersonagem;
    [SerializeField] private LayerMask cenarioLayer;
    [SerializeField] private float tempoDash = 0.3f;
    [SerializeField] private float forcaDash = 20f;
    [SerializeField] private float CoolDownDash = 0.2f;
    [SerializeField] private TrailRenderer[] trailRenderer;
    [SerializeField] private JogadorUI jogadorUI;

    void Start()
    {
        //Este script esta associado ao personagem; o metodo GetComponent busca no 
        //objetos associados, ou seja, no personagem, o componenteindicado (Rigidbody2D)
        rb = GetComponent<Rigidbody2D>();

        direcaoAtual = DirecaoPersonagem.DIREITA;

        animator = GetComponent<Animator>();

        foreach (TrailRenderer trail in trailRenderer)
        {
            trail.emitting = false;
        }
            
    }

    //Update e chamado um ver por frame
    void Update()
    {
        //Input le a entrada do jogador. o metodo GetAxis le o valor do eixo indicado
        entradaHorizontal = Input.GetAxis("Horizontal");

        //Cria um circulo invisivel no ponto pePersonagem.position de raio 0.3 e analisa 
        //todos os colisores que for do cenarioLayer
        estaNoChao = Physics2D.OverlapCircle(pePersonagem.position, 0.3f, cenarioLayer);


        if (entradaHorizontal > 0)
        {
            GirarPersonagem(DirecaoPersonagem.DIREITA);
        }
        else if (entradaHorizontal < 0)
        {
            GirarPersonagem(DirecaoPersonagem.ESQUERDA);
        }

        animator.SetBool("correr", entradaHorizontal != 0);
        animator.SetBool("estaNoChao", estaNoChao);

        //Verifica se o usuario clicou na tecla SPACE
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (estaNoChao)
            {
                saltoExtra = true;
                ExecutaSalto();
            }
            else 
            { 
                if (saltoExtra)
                {
                    saltoExtra = false;
                    ExecutaSalto();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashLiberado)
        {
            StartCoroutine(RealizarDash());
        }
    }

    private void FixedUpdate()
    {
        if (!executandoDash)
        {
            rb.linearVelocity = new Vector2(entradaHorizontal * velocidade, rb.linearVelocity.y);
        }
    }

    private void ExecutaSalto()
    {
        //Zera a velocidade do personagem
        rb.linearVelocity= Vector2.zero;

        //Adiciona uma forca de intensidade de 300 para cima
        rb.AddForce(Vector2.up * 300);

        animator.SetTrigger("saltar");
    }

    private void GirarPersonagem(DirecaoPersonagem direcao)
    {
        if (direcaoAtual == direcao)
        {
            return; 
        }

        direcaoAtual = direcao;

        if (direcaoAtual == DirecaoPersonagem.DIREITA)
        {
            transform.eulerAngles = new Vector3(0,0,0);
        }
        else
        {
            transform.eulerAngles= new Vector3(0,180,0); //Rotaciona o personagem em 180 graus no eixo Y
        }
    }

    private IEnumerator RealizarDash()
    {
        foreach (TrailRenderer trail in trailRenderer)
        {
            trail.emitting = true;
        }

        dashLiberado = false;
        //Parar de mover o personagem durante o dash
        executandoDash = true;

        //Elimina as forcas de velocidade que atuam sobre o personagem
        rb.linearVelocity = Vector2.zero;

        //Elimina a acao da gravidade sobre o personagem durante o dash
        rb.gravityScale = 0;

        if (direcaoAtual == DirecaoPersonagem.DIREITA)
        {
            rb.AddForce(Vector2.right * forcaDash, ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(Vector2.left * forcaDash, ForceMode2D.Impulse);
        }


        //Espera 0.3 segundos para o dash ser executado
        //Duracao do dash
        yield return new WaitForSeconds(tempoDash);

        foreach (TrailRenderer trail in trailRenderer)
        {
            trail.emitting = false;
        }
        executandoDash = false;
        rb.gravityScale = 1;
        //Zera a velocidade do personagem apos o dash
        rb.linearVelocity = Vector2.zero;

        //libera a execucao do dash apos um tempo de recarga
        //yield return new WaitForSeconds(3f);

        float contador = 0f;
        while (contador < CoolDownDash)
        {   
            contador += Time.deltaTime;
            jogadorUI.AtualizarProgressoDash(contador / 3f);

            yield return null;
        }
        dashLiberado = true;


    }
}

enum DirecaoPersonagem { DIREITA, ESQUERDA};

