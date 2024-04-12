using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissiblesRanges : MonoBehaviour
{
    public float speed, distance, damage;
    public string sfxEffect;
    private PlayerScript player_Script;
    private void Start()
    {
        player_Script = FindObjectOfType<PlayerScript>();
    }


    public void setVariables(float speed, float distance, float damage, string sfx)
    {
        this.speed     = speed;
        this.distance  = distance;
        this.damage    = damage;
        this.sfxEffect = sfx;
    }

    private void GetEffects()
    {
        Instantiate(Resources.Load("Effects/" + sfxEffect), transform.position, Quaternion.identity);
    }


    private void FixedUpdate()
    {
        if (distance <= 0f)
        {
            Destroy(gameObject);
        }
        else
        {
            distance = distance - Time.deltaTime;
            bool groundCheck = Physics2D.OverlapCircle(transform.position, 0.4f, 1 << LayerMask.NameToLayer("ground"));
            bool damage = Physics2D.OverlapCircle(transform.position, 0.4f, 1 << LayerMask.NameToLayer("Player"));
            if (groundCheck) { 
                GetEffects();
                Destroy(gameObject);
            }
            if (damage)
            {
                player_Script.TakeDamage(this.damage);
                GetEffects();
                Destroy(gameObject);
            }
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        }
    }


}


