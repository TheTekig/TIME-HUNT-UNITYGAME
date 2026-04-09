using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public static string sceneToLoad;

    void Start()
    {
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        Debug.Log("Cena a carregar: " + sceneToLoad);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);
        operation.allowSceneActivation = false;

        Debug.Log("Iniciou async load");

        while (operation.progress < 0.9f)
        {
            Debug.Log("Progress: " + operation.progress);
            yield return null;
        }

        Debug.Log("Chegou em 0.9, ativando cena...");
        yield return new WaitForSecondsRealtime(3f);
        operation.allowSceneActivation = true;
        Debug.Log("Cena ativada!");

    }
}
