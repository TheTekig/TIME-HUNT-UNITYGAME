using System.Collections;
using Unity.VisualScripting;
using UnityEngine;  
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UILevelSelect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler 
{


    [SerializeField] GameObject icon;

    [Header("Slide Animation")]
    [SerializeField] float TransitionSpeed = 800f;
    [SerializeField] float hiddenPosX = 747f;
    [SerializeField] float visiblePosX = -275.7484f;
    [SerializeField] private RectTransform panelRect;
    [SerializeField] private AudioSource SoundOnEnter;
    [SerializeField] private AudioSource SoundOnExit;

    private Coroutine slideCoroutine;

    void Start()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SoundOnEnter.Play();
        if (icon != null) icon.SetActive(true);

        if (slideCoroutine != null) StopCoroutine(slideCoroutine);
        slideCoroutine = StartCoroutine(SlidePanel(visiblePosX));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
        SoundOnExit.Play();
        if (slideCoroutine != null) StopCoroutine(slideCoroutine);
        slideCoroutine = StartCoroutine(SlidePanel(hiddenPosX));
    }




    IEnumerator SlidePanel(float targetX)
    {
        while (Mathf.Abs(panelRect.anchoredPosition.x - targetX) > 0.5f)
        {
            float newX = Mathf.Lerp(panelRect.anchoredPosition.x, targetX, Time.unscaledDeltaTime * TransitionSpeed);
            panelRect.anchoredPosition = new Vector2(newX, panelRect.anchoredPosition.y);
            yield return null;
        }
        panelRect.anchoredPosition = new Vector2(targetX, panelRect.anchoredPosition.y);
    }
}
