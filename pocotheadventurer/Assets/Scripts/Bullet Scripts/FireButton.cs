using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FireButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool IsPressed { get; set; }

    public void OnPointerDown(PointerEventData eventData)
    {
        IsPressed = true;
        Debug.Log("True");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        IsPressed = false;
        Debug.Log("False");
    }

}
