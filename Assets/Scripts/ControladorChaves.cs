using UnityEngine;
using System.Collections.Generic;

public class ControladorChaves : MonoBehaviour
{
    [SerializeField] private GameObject chavesPrefab;
    [SerializeField] private List<Transform> pontosSpawn;
    [SerializeField] private GameObject efeitoParticulas;
    [SerializeField] private AudioSource chaveColetadaAudioSource;

    void Start()
    {
        InstanciaChaves();
    }

    // Update is called once per frame
    private void InstanciaChaves()
    {
        for( int i =0; i<3; i++)
        {
            int pos = Random.Range(0, pontosSpawn.Count);
            Instantiate(chavesPrefab, pontosSpawn[pos].position, Quaternion.identity);
            pontosSpawn.RemoveAt(pos);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "chave")
        {
            chaveColetadaAudioSource.Play();
            Instantiate(efeitoParticulas, collision.transform.position, collision.transform.rotation);
            collision.collider.enabled = false;
            ControladorPartida.Instance.NovaChaveColetada();
            Destroy(collision.gameObject);
        }

    }
}
