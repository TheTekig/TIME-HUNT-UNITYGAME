using UnityEngine;
using System.Collections;
public class Ataque : MonoBehaviour
{
    [SerializeField] private ControladorHitBox controladorHitbox;
    [SerializeField] private JogadorUI jogadorUI;
    [SerializeField] private GameObject efeitoCriacaoBoladeFogo;
    [SerializeField] private Transform PosicaoefeitoBoladeFogo;

    [SerializeField] private Projetil bolaDeFogo;
    [SerializeField] private Transform pontoDeLancamento;

    [SerializeField] private int danoEspada = 30;
    [SerializeField] private int danoBolaDeFogo = 50;

    private bool bolaDeFogoLiberada = true;
    private bool espadaLiberada = true;


    [SerializeField] private AudioSource ataqueEspadaAudioSource;
    [SerializeField] private AudioSource ataqueBolaDeFogoAudioSource;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && espadaLiberada)
        {
            StartCoroutine(AtacarComEspada());
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && bolaDeFogoLiberada)
        {
            StartCoroutine(AtacarComBolaDeFogo());
        }
    }

    private IEnumerator AtacarComEspada()
    {
        ataqueEspadaAudioSource.Play();

        espadaLiberada = false;
        animator.SetTrigger("ataqueEspada");

        controladorHitbox.AplicarDano(danoEspada);

        float contador = 0f;

        while(contador < 0.8f)
        {
            contador += Time.deltaTime; 
            jogadorUI.AtualizarProgressoEspada(contador/0.8f);
            yield return null;
        }
         //Tempo de ataque da espada

        espadaLiberada = true;
    }

    private IEnumerator AtacarComBolaDeFogo()
    {
        ataqueBolaDeFogoAudioSource.Play();

        bolaDeFogoLiberada = false;

        Instantiate(efeitoCriacaoBoladeFogo, PosicaoefeitoBoladeFogo.position, PosicaoefeitoBoladeFogo.rotation);
        animator.SetTrigger("ataqueBolaDeFogo");
        yield return new WaitForSeconds(0.3f); // Tempo de preparação da bola de fogo

        Projetil projetil = Instantiate(bolaDeFogo, pontoDeLancamento.position, pontoDeLancamento.rotation);
        projetil.IniciarLancamento(null, 5, danoBolaDeFogo, false);

        float contador = 0f;
        while(contador < 3f)
        {
            contador += Time.deltaTime; 
            jogadorUI.AtualizarBolaFogoProgresso(contador/3f);
            yield return null;
        }
        bolaDeFogoLiberada = true;
    }
}
