using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Joystick : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    private PlayerMoveJoystick playerMove;

    private void Start()
    {
        playerMove = GameObject.Find("Player").GetComponent<PlayerMoveJoystick>();
    }

    public void OnPointerUp(PointerEventData data) // release a button
    {
        playerMove.StopMoving();
    }

    public void OnPointerDown(PointerEventData data) // touch a button
    {
        if(gameObject.name == "Left")
            playerMove.SetMoveLeft(true);
        else
            playerMove.SetMoveLeft(false);
    }

}
