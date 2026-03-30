using UnityEngine;

public class Projetil : MonoBehaviour
{
    private int dano;
    private int velocidade;
    [SerializeField] private GameObject explosao;
    [SerializeField] private bool ignorarInimigos;
    void Start()
    {
        Invoke(nameof(DestruirProjetil), 5f);
    }

    void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * velocidade);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!(collision.gameObject.tag == "inimigo" && ignorarInimigos))
        {
            collision.gameObject.GetComponent<Vida>()?.ReduzirVida(dano);
        }

        DestruirProjetil();
    }

    public void IniciarLancamento(Transform alvo, int velocidade, int dano, bool ignorarInimigos)
    {
        if (alvo != null)
        {
            transform.right = alvo.position - transform.position;
        }

        this.velocidade = velocidade;
        this.dano = dano;
        this.ignorarInimigos = ignorarInimigos;

    }

    private void DestruirProjetil()
    {
        Instantiate (explosao, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
