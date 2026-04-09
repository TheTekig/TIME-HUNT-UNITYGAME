using System.Collections;
using Unity.VisualScripting;
using UnityEngine;  
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UILevelSelect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler 
{
    [SerializeField] private TextMeshProUGUI m_TextMeshPro;
    int TextSize = 40;

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
        m_TextMeshPro.fontSize = TextSize;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SoundOnEnter.Play();
        m_TextMeshPro.fontSize = TextSize + 5;

        if (icon != null) icon.SetActive(true);

        if (slideCoroutine != null) StopCoroutine(slideCoroutine);
        slideCoroutine = StartCoroutine(SlidePanel(visiblePosX));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
        SoundOnExit.Play();
        m_TextMeshPro.fontSize = TextSize;
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
