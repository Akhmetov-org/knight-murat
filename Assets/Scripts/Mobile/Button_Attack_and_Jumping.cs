using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button_Attack_and_Jumping : MonoBehaviour, IPointerDownHandler,IPointerUpHandler
{
    public bool is_attack;

    private PlayerScript player_Script;

    public static bool is_jump_activate;
    public static bool is_attack_activate;

    private void Start()
    {
        player_Script = FindObjectOfType<PlayerScript>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (is_attack)
        {
            is_attack_activate = true;
        }
        else
        {
            is_jump_activate = true;
            player_Script.JumpMobile();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (is_attack)
        {
            is_attack_activate = false;
        }
        else
        {
            is_jump_activate = false;
        }
    }
}
