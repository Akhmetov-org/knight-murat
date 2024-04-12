using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*public class PlayerController : MonoBehaviour
{
    public float jump_Impulse = 20f;
    [Header("Базовые настройки")]
    public float baseHealth;
    public float base_Damage;
    [Header("Прочие настройки")]
    private float health;
    //==========================//
    //        Компоненты        //
    //==========================//
    public  Rigidbody2D       rigidbody;
    private CapsuleCollider2D capsule_collider;
    private Animator          animation;
    private Vector2           child_box_size;
    private CameraShake       camShake;
    private PortalScript      portal_script;
    //=========================//
    //          Атака          //
    //=========================//
    private float attack_time_base;
    private float attack_time = 0f;
    private int   attack_set  = 0;
    private bool  isAttacking = false;
    private bool  isAlive = true;
    //=====================//
    //     Полоса жизни    //
    //=====================//
    private Image healthBar;
    private Image healthBar2;
    private float fadeTime_base = 255f;
    private float fadeTime;
    //=============================//
    //   Статитические переменные  //
    //=============================//
    public static bool allPause = false;
    public static bool isDeath  = false;
    private bool pause = false;
    //=============================//
    //           Зелье             //
    //=============================//
    //public TextMeshProUGUI progress_icon;
    //private int  total_poition;
    //private bool sfx_check;
    //=============================//
    //        Победа поражение     //
    //=============================//
   // public static bool lose_win;
    //public Canvas      Lose, Win;

    void Start()
    {
        animation = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        capsule_collider = GetComponent<CapsuleCollider2D>();
        child_box_size   = transform.GetChild(0).GetComponent<BoxCollider2D>().size;
        portal_script    = FindObjectOfType<PortalScript>();
        total_poition    = GameObject.FindGameObjectsWithTag("poition").Length;

        camShake = FindObjectOfType<CameraShake>();

        //=========================
        isDeath = false;
        lose_win = false;
        animation.speed = 0.2f;
        portal_script.setActive(false);
        progress_icon.text = total_poition.ToString();


        UpdateAnimClipTimes();
        //=========================
        health     = baseHealth;
        healthBar  = transform.GetChild(2).transform.GetChild(1).GetComponent<Image>(); // red
        healthBar2 = transform.GetChild(2).transform.GetChild(0).GetComponent<Image>(); // black

    }


    public void JumpMethod(float impulse)
    {

        rigidbody.velocity = Vector2.zero;
        rigidbody.AddForce(new Vector2(0, impulse), ForceMode2D.Impulse);

    }

    private void getEffects()
    {
        Shake(0.12f, 0.1f);
        Instantiate(Resources.Load("Effects/slashEffect") as GameObject, transform.GetChild(1).position, Quaternion.identity);
    }

    private void Attack()
    {
        if (attack_time <= 0f)
        {
            animation.speed = 0.2f;
            isAttacking = false;

            //~~~~~~~~~~BossAttack~~~~~~~~~~// 
            Collider2D boss = Physics2D.OverlapCircle(transform.GetChild(1).position, 0.3f, 1 << LayerMask.NameToLayer("Boss"));
            if(boss != null)
            {
                getEffects();
                Sound.PlaySound("S_essence_found");
                boss.GetComponent<BossScript>().DamageTake(base_Damage);
            }


            //~~~~~~~~~~Attacking~~~~~~~~~~// 

            Collider2D[] enemy = Physics2D.OverlapCircleAll(transform.GetChild(1).position, 0.3f, 1 << LayerMask.NameToLayer("Monsters"));
            for (int i = 0; i < enemy.Length; i++)
            {
                getEffects();
                Sound.PlaySound("S_essence_found");
                enemy[i].GetComponent<EnemyScript>().DamageTaken(base_Damage);
                break;
            }
            
        }
        else
        {
            attack_time = attack_time - Time.deltaTime;
            animation.SetInteger("anim", attack_set);
        }
    }

    private void Moving()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        float speedX = Input.GetAxis("Horizontal") * 6;
        //~~~~~~~~~~Moving~~~~~~~~~~// 

        rigidbody.velocity = new Vector2(speedX, rigidbody.velocity.y);

        //~~~~~~~~~Animation~~~~~~~~//
        bool groundCheck = Physics2D.OverlapBox(transform.GetChild(0).position, child_box_size, 0f, 1 << LayerMask.NameToLayer("ground"));

        if (!groundCheck)
        {
            animation.SetInteger("anim", 4);
        }
        else
        {
            if (speedX != 0)
                animation.SetInteger("anim", 1);
            else
                animation.SetInteger("anim", 0);
        }

        //~~~~~~~~~Quaternion~~~~~~~~// 
        if (speedX < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (speedX > 0)
            transform.localScale = new Vector3(1, 1, 1);

        //~~~Star collision~~~//
        Collider2D star = Physics2D.OverlapCircle(transform.position, 0.5f, 1 << LayerMask.NameToLayer("Poition"));
        if (star != null)

        {
            //~~~~Text Ui~~~~//

            total_poition = total_poition - 1;
            progress_icon.text = total_poition.ToString();
            if (total_poition == 0)
            {
                portal_script.setActive(true);
                Instantiate(Resources.Load("Effects/S_witch_ward") as GameObject, portal_script.gameObject.transform.position, Quaternion.identity);
                Instantiate(Resources.Load("Effects/S_sound_attack") as GameObject, portal_script.gameObject.transform.position, Quaternion.identity);
                Sound.PlaySound("S_witch_ward");
                Sound.PlaySound("S_sound_attack");
            }

            //~~~Star Destroy~~~//
            Instantiate(Resources.Load("Effects/S_royal_beat") as GameObject, new Vector3(star.transform.position.x, star.transform.position.y, -10f), Quaternion.identity);
            Sound.PlaySound("S_technology");
            Destroy(star.gameObject);
            Debug.Log(total_poition);
        }

        //~~~Walking Effects~~~//
        if (!groundCheck)
        {
            if (animation.speed >= 0.3f)
                animation.speed = animation.speed / 1.5f;
            sfx_check = true;
        }

        if (groundCheck && sfx_check)
        {
            if (animation.speed < 0.3f)
                animation.speed = animation.speed = 0.2f;
            sfx_check = false;
            Sound.PlaySound("Jump_sound");
            Instantiate(Resources.Load("Effects/dustsfxParticle"), new Vector3(transform.GetChild(0).position.x,transform.GetChild(0).position.y, -10f), Quaternion.identity);
        }

        //~~~~~~~~~Jump~~~~~~~~//
        if (Input.GetKeyDown(KeyCode.Space) && groundCheck)
        {
            JumpMethod(jump_Impulse);
        }
    }

    private void OnMove()
    {
        if (!allPause && !pause && !isDeath)
        {

            if (Input.GetKeyDown(KeyCode.R) && attack_time <= 0f)
            {
                Sound.PlaySound("HeavySword");
                animation.speed = 0.4f;
                isAttacking = true;
                attack_set = Random.Range(2, 4);
                attack_time = attack_time_base * 2.5f;
            }
            //~~~~~~~~~~Attack~~~~~~~~~~// 

            if (isAttacking)
                Attack();
            else
                Moving();
        }
   
    }


    public void TakeDamage(float takeDamage)
    {
        health = health - takeDamage;
        if (health <= 0f)
        {
            isDeath = true;
            FreezePlayer(true);
            animation.SetInteger("anim", 5);
        }
        else
        {
            healthBar.fillAmount = (health / baseHealth);
            fadeTime = fadeTime_base;
        }
        Debug.Log("damageTaked");
    }

    void Update()
    {
        if(!isDeath)
        OnMove();
    }

    private void FixedUpdate()
    {
        //===========================//
        //       healthBar           //
        //===========================//
        if (fadeTime <= 0f)
        {
            healthBar.color = new Color32(255, 0, 0, 0);
            healthBar2.color = new Color32(0, 0, 0, 0);
        }
        else
        {
            fadeTime = fadeTime - 3f;
            healthBar.color = new Color32(255, 0, 0, (byte)fadeTime);
            healthBar2.color = new Color32(0, 0, 0, (byte)fadeTime);
        }
    }

    public void Shake(float shakeDuration, float shakeAmount)
    {
        camShake.shakeDuration = shakeDuration;
        camShake.shakeAmount = shakeAmount;
        camShake.decreaseFactor = 1f;
    }
    public void FreezePlayer(bool action)
    {
        pause = action;
        rigidbody.velocity = Vector2.zero;
        rigidbody.isKinematic = action;
        capsule_collider.isTrigger = action;
    }

    public void setLose()
    {
        lose_win = true;
        Lose.enabled = true;
        FreezePlayer(true);
        Sound.PlaySound("lose_sound");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "portal")
        {
            lose_win = true;
            FreezePlayer(true);
            Sound.PlaySound("win_sound");
            portal_script.SetActivatePortal();
        }
        else if (collision.gameObject.tag == "deathzone")
        {
            setLose();
        }
    }

    public void UpdateAnimClipTimes()
    {
        AnimationClip[] clips = animation.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            switch (clip.name)
            {
                case "attack_1":
                    attack_time_base = clip.length;
                break;
            }
        }
    }
}

*/
