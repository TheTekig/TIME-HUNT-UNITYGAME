using UnityEngine;

public class DestruirObjeto : MonoBehaviour
{
    [SerializeField] private float delay;
    void Start()
    {
        Destroy(gameObject, delay);
    }
}
