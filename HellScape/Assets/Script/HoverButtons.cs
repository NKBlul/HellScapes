using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverButtons : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.instance.PlaySFX("ButtonHover");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.instance.PlaySFX("ButtonPressed");
    }
}
