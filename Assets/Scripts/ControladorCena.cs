using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorCena : MonoBehaviour
{
    public void CarregarCena(string cenaIndex)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(cenaIndex);
    }
}
