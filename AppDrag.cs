using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AppDrag : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public RectTransform app;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    void Start()
    {
        canvas = FindFirstObjectByType<Canvas>();
        canvasGroup = FindFirstObjectByType<CanvasGroup>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {

        app.transform.SetAsLastSibling();
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        app.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
    }
}
