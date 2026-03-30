using UnityEngine;

public class Plataforma : MonoBehaviour
{

    [SerializeField] private GameObject[] checkPoints;

    private int checkpointDestino;

    [SerializeField] private float velocidade = 2f;

    void Start()
    {
        
    }


    void Update()
    {

        //Calcula a distancia entre dois pontos
        //Se a posicao do proximo checkpoint menos a posicao atual da plataforma for menor que 0.1, indica 
        //que a plataforma praticamente chegou no checkpoint

        if (Vector2.Distance(checkPoints[checkpointDestino].transform.position, transform.position) < 0.1f)
        {
            checkpointDestino++;
            if (checkpointDestino >= checkPoints.Length)
            {
                checkpointDestino = 0;
            }

        }

        transform.position = Vector2.MoveTowards(
            transform.position,
            checkPoints[checkpointDestino].transform.position,
            Time.deltaTime * velocidade);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(null);
        }
    }
}

