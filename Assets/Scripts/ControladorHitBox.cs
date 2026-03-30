using UnityEngine;
using System.Collections.Generic;

public class ControladorHitBox : MonoBehaviour
{
    //Variavel que contera todos os inimigos que estao dentro da area do hitbox
    [SerializeField] private List<GameObject> objetosDentroDaArea;

    //Define tag dos objetos que serao considerados alvos do nosso hitbox
    [SerializeField] private string alvoTag;

   
    //Medoto chamado quando um objeto entrar n zona de comlisao com nosso trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Verifica se o objeto Entrou em contato com a trigger possui a
        // mesma Tag definida como alvo do hitbox

        if (collision.gameObject.CompareTag(alvoTag))
        {
            objetosDentroDaArea.Add(collision.gameObject);
        }
    }

    //Medoto chamado quando um objeto sair da zona de comlisao com nosso trigger
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Verifica se o objeto saiu em contato com a trigger possui a mesma TAG definida como alvo do hitbox
        if (collision.gameObject.CompareTag(alvoTag))
        {
            objetosDentroDaArea.Remove(collision.gameObject);
        }
    }

    public bool ExisteAlvosDisponiveis()
    {
        return objetosDentroDaArea.Count > 0;
    }

    public void AplicarDano(int dano)
    {
        for(int i = 0; i < objetosDentroDaArea.Count; i++)
        {
            objetosDentroDaArea[i].GetComponent<Vida>().ReduzirVida(dano);
        }
    }



}
