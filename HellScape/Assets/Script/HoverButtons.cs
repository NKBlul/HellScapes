using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    Color32 defaultColor = new Color32(48, 48, 48, 0);
    Color32 hoverColor = new Color32(48, 48, 48, 48);
    private void Start()
    {
        GetComponent<Image>().color = defaultColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.instance.PlaySFX("ButtonHover");
        GetComponent<Image>().color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().color = defaultColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.instance.PlaySFX("ButtonPressed");
    }
}
