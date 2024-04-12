using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cultist : BossScript
{

    public GameObject hitSfx1;
    public GameObject hitSfx2;

    public GameObject Missible;
    private enum Change { ATTACK1,ATTACK2,ATTACK3};
    private Change change;

    private GameObject missible;
    private void Start()
    {
        RandomAttacks();
    }

    private void RandomAttacks()
    {
        int i = Random.RandomRange(1, 4);
        switch (i)
        {
            case 1:
                change = Change.ATTACK1;
                attack_distance = 3f;
                break;
            case 2:
                change = Change.ATTACK2;
                attack_distance = 5.5f;
                break;
            case 3:
                change = Change.ATTACK3;
                attack_distance = 12.5f;
                break;
        }
    }
    protected override void GetEffects()
    {
        if (transform.position.x > player_Script.transform.position.x)
        {
            Instantiate(Resources.Load("bloodEffect/blueBlood") as GameObject, transform.position, Quaternion.Euler(0f, 90, 90f));
            Instantiate(Resources.Load("Effects/sfxSword") as GameObject, transform.position, Quaternion.Euler(0f, 90, 90f));
        }
        else
        {
            Instantiate(Resources.Load("bloodEffect/blueBlood") as GameObject, transform.position, Quaternion.Euler(-180, 90, 90f));
            Instantiate(Resources.Load("Effects/sfxSword") as GameObject, transform.position, Quaternion.Euler(-180, 90, 90f));
        }
        player_Script.Shake(0.20f, 0.20f);
    }

    private void OnDamage( Vector2 position, float damage,float radius)
    {
        Collider2D take = Physics2D.OverlapCircle(position, radius, 1 << LayerMask.NameToLayer("Player"));
        if (take != null)
            take.GetComponent<PlayerScript>().TakeDamage(damage);
    }

    protected override void Attack()
    {
        if (attack_duration <= 0)
        {
            is_attack = false;
            switch (change)
            {
                case Change.ATTACK1:
                    player_Script.Shake(0.30f, 0.20f);
                    player_Script.rigidbody.AddForce(new Vector2(localScale.x * 5f, 12f), ForceMode2D.Impulse);
                    Instantiate(hitSfx1, transform.GetChild(0).position, Quaternion.identity);
                    OnDamage(transform.GetChild(0).position, 5f, 3f);
                    Sound.PlaySound("S_rubysun");
                    break;

                case Change.ATTACK2:
                    Vector2 position = transform.GetChild(0).position;
                    if (localScale.x < 0)
                        position.x = position.x - 1.6f;
                    else
                        position.x = position.x + 1.6f;
                    OnDamage(position, 2f, 2f);
                    player_Script.Shake(0.45f, 0.22f);
                    player_Script.rigidbody.AddForce(new Vector2(localScale.x * 4f, 12f), ForceMode2D.Impulse);
                    Instantiate(hitSfx2, position, Quaternion.identity);
                    OnDamage(transform.GetChild(0).position, 3f, 2.5f);
                    Sound.PlaySound("S_rock_monster");
                    break;

                case Change.ATTACK3:
                    player_Script.Shake(0.08f, 0.20f);
                    missible = Instantiate(Missible, transform.GetChild(0).position, Quaternion.identity);
                    missible.GetComponent<Missible>().setVariables(localScale.x * 10f, 10f, 3f);
                    Sound.PlaySound("S_Forward_thurder");
                    break;
            }
            RandomAttacks();
        }
        else
        {
            attack_duration = attack_duration - Time.deltaTime;
            anim.SetInteger("anim", 4); // attack animation
        }
    }
}
