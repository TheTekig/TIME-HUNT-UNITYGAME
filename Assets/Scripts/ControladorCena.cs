using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ControladorCena : MonoBehaviour
{
    public void CarregarCena(string cenaName)
    {
        Time.timeScale = 1;
        StartCoroutine(Delay(0.37f, cenaName));
    }

    IEnumerator Delay(float tempo, string cenaName)
    {
        yield return new WaitForSecondsRealtime(tempo);
        LoadingManager.sceneToLoad = cenaName;
        SceneManager.LoadScene("LoadingScene");
    }
}


