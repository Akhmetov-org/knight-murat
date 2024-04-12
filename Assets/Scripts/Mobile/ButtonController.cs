using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float cur_speed;


    public void OnPointerDown(PointerEventData eventData)
    {
        if (cur_speed < 0)
            PlayerScript.speed = Mathf.Abs(cur_speed) * (-1);
        else
            PlayerScript.speed = Mathf.Abs(cur_speed);

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        PlayerScript.speed = 0f;
    }

}
