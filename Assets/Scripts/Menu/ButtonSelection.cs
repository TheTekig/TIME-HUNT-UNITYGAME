using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ButtonSelection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private AudioSource SoundOnEnter;
    [SerializeField] private AudioSource SoundOnExit;





    public void OnPointerEnter(PointerEventData eventData)
    {
        
        SoundOnEnter.Play();
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {

        SoundOnExit.Play();

    }

  
}
