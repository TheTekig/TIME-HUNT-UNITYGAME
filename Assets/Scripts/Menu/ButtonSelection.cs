using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonSelection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private AudioSource SoundOnEnter;
    [SerializeField] private AudioSource SoundOnExit;

    [SerializeField] private TextMeshProUGUI TextMeshPro;
    [SerializeField] private int sizeIncrese = 5;
    private float fontSize;


    void Start()
    {
        try { fontSize = TextMeshPro.fontSize; }
        catch { }       
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        try {TextMeshPro.fontSize = fontSize + sizeIncrese;}
        catch { }
        SoundOnEnter.Play();
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        try { TextMeshPro.fontSize = fontSize; }
        catch { }
        SoundOnExit.Play();

    }

  
}
