using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour
{
    [SerializeField] public float Speed, Impulse,Rotation_Speed;
    
    private float cirlce;
    private Rigidbody2D rb;
    private float min_x, max_x;

    private PlayerScript player_Script;
    void Start()
    {
        rb     = GetComponent<Rigidbody2D>();
        cirlce = GetComponent<CircleCollider2D>().radius;
        min_x  = transform.GetChild(0).position.x;
        max_x  = transform.GetChild(1).position.x;

        player_Script = FindObjectOfType<PlayerScript>();

        Debug.Log(cirlce);
    }

    void Update()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        if (min_x > x)
            Speed = Mathf.Abs(Speed);
        else if (max_x < x)
            Speed = Mathf.Abs(Speed) * (-1f);
        transform.position = new Vector2(x + Speed * Time.deltaTime, y);
        bool groundCheck = Physics2D.OverlapCircle(transform.position, cirlce, 1 << LayerMask.NameToLayer("ground"));
        if (groundCheck)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(0, Impulse), ForceMode2D.Impulse);
        }
        if (Speed > 0)
            transform.Rotate(new Vector3(0, 0, -Rotation_Speed));
        else if(Speed < 0)
            transform.Rotate(new Vector3(0, 0, Rotation_Speed));

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "player")
        {
            player_Script.setLose();
        }
    }

}
