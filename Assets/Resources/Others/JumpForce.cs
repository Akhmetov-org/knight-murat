using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpForce : MonoBehaviour
{
    public Color normal_color;
    public Color collision_color;
    public float jump_force;

    private ParticleSystem render;
    private PlayerScript playerScript;
    private bool activate = false;

    private void Start()
    {
        playerScript = FindObjectOfType<PlayerScript>();
        render = GetComponent<ParticleSystem>();
    }


    IEnumerator setColor(Color color)
    {
        for (int i = 0; i < 10; i++)
        {
            render.startColor = color;
            yield return new WaitForSeconds(0.01f);
        }
        if (render.startColor != normal_color)
            render.startColor = normal_color;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bootles")
        {
            activate = true;

            if (render.startColor != collision_color)
                StartCoroutine(setColor(collision_color));
        }
    }

    private void Update()
    {
        if (activate)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Button_Attack_and_Jumping.is_jump_activate)
            {
                playerScript.JumpMethod(jump_force);
                Instantiate(Resources.Load("Effects/S_essence_found"), transform.position, Quaternion.identity);
                Sound.PlaySound("S_essence_found");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        activate = false;

        if (render.startColor != normal_color)
            render.startColor = normal_color;
    }
}
