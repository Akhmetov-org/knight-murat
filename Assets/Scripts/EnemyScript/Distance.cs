using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distance : MonoBehaviour
{
    PlayerScript player_Script;
    EnemyScript  enemy_Script;
    void Start()
    {
        player_Script = FindObjectOfType<PlayerScript>();
        enemy_Script  = GetComponent<EnemyScript>();
    }

    void FixedUpdate()
    {
        float distance = Vector2.Distance(player_Script.transform.position, transform.position);
        if (distance <= 10f && !enemy_Script.enabled)
        {
            enemy_Script.enabled = true;
        }
        else if (enemy_Script.enabled)
        {
            enemy_Script.enabled = false;
        }
    }
}
