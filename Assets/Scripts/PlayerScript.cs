using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerScript : MonoBehaviour
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
    public   Canvas Lose, Win;
    public   TextMeshProUGUI progress_icon;
    private  PortalScript portal_script;


    public   Rigidbody2D rigidbody;
    private  CapsuleCollider2D capsule_collider;
    private  Animator animation;
    private  Vector2 child_box_size;
    private  CameraShake camShake;
    //=========================//
    //          Атака          //
    //=========================//
    private float attack_time_base;
    private float attack_time = 0f;
    private int   attack_set = 0;
    private bool  isAttacking = false;
    private bool  isAlive = true;

    private bool  is_attack_check = false;
    private bool sfx_check;
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
    public static bool lose_win = false;
    private bool       pause    = false;
    //=============================//
    //       Прочие перемнные      //
    //=============================//
    public int total_monsters;
    public static float speed = 0f;

    void Start()
    {
        animation = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        capsule_collider = GetComponent<CapsuleCollider2D>();
        child_box_size = transform.GetChild(0).GetComponent<BoxCollider2D>().size;
        portal_script = FindObjectOfType<PortalScript>();
        total_monsters = GameObject.FindGameObjectsWithTag("Monsters").Length;

        camShake = FindObjectOfType<CameraShake>();

        //=========================
        health     = baseHealth;
        healthBar  = transform.GetChild(2).transform.GetChild(1).GetComponent<Image>(); // red
        healthBar2 = transform.GetChild(2).transform.GetChild(0).GetComponent<Image>(); // black


        //=========================
        isDeath  = false;
        lose_win = false;
        allPause = false;
        animation.speed = 0.3f;
        portal_script.setActive(false);
        progress_icon.text = total_monsters.ToString();

        Debug.Log(total_monsters);
        UpdateAnimClipTimes();
       
    }


    public void portalActive()
    {
        portal_script.gameObject.SetActive(true);
    }

    public void JumpMethod(float impulse)
    {
        rigidbody.velocity = Vector2.zero;
        rigidbody.AddForce(new Vector2(0, impulse), ForceMode2D.Impulse);

    }

    public void UpdateText()
    {
        total_monsters = total_monsters - 1;
        progress_icon.text = total_monsters.ToString();
        if (total_monsters == 0)
        {
            portalActive();
            Instantiate(Resources.Load("Effects/S_sound_attack") as GameObject, portal_script.transform.position, Quaternion.identity);
            Sound.PlaySound("S_sound_attack");
            Instantiate(Resources.Load("Effects/S_witch_ward") as GameObject, portal_script.transform.position, Quaternion.identity);
            Sound.PlaySound("S_witch_ward");
        }
    }

    public void TakeDamage(float takeDamage)
    {
        health = health - takeDamage;
        if (health <= 0f)
        {
            isDeath = true;
            FreezePlayer(true);
            animation.SetInteger("anim", 8);
        }
        else
        {
            healthBar.fillAmount = (health / baseHealth);
            fadeTime = fadeTime_base;
        }
    }
    float speedX;

    public bool MobileButton;
    public void ClickMovingButton__A()
    {
        
        speedX = -1f;
    }   
    public void ClickMovingButton__D()
    {
        
        speedX = 1f;
    }
    private void Moving()
    {
        float x = transform.position.x;
        float y = transform.position.y; speedX = speed;
        //~~~~~~~~~~Moving~~~~~~~~~~// 
       // if(!MobileButton) speedX = Input.GetAxis("Horizontal") * 6f;
        rigidbody.velocity = new Vector2(speedX, rigidbody.velocity.y);

        //~~~~~~~~~Animation~~~~~~~~//
        bool groundCheck = Physics2D.OverlapBox(transform.GetChild(0).position, child_box_size, 0f, 1 << LayerMask.NameToLayer("ground"));

        if (!groundCheck)
        {
            animation.SetInteger("anim", 2);
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
            Instantiate(Resources.Load("Effects/dustStone"), new Vector3(transform.GetChild(0).position.x, transform.GetChild(0).position.y, -10f), Quaternion.identity);
        }

        //~~~~~~~~~Jump~~~~~~~~//
        if (Input.GetKeyDown(KeyCode.Space) && groundCheck)
        {
            JumpMethod(jump_Impulse);
        }
    }

    public void JumpMobile()
    {
        bool groundCheck = Physics2D.OverlapBox(transform.GetChild(0).position, child_box_size, 0f, 1 << LayerMask.NameToLayer("ground"));
        if (groundCheck) JumpMethod(jump_Impulse);
    }

    // Эффекты //
    private void getEffects()
    {
        Shake(0.12f, 0.1f); // Трясение камерой
        Instantiate(Resources.Load("Effects/slashEffect") as GameObject, transform.GetChild(1).position, Quaternion.identity);
    }
    public void Shake(float shakeDuration, float shakeAmount)
    {
        // задаем аргументы для ShakeCamera
        camShake.shakeDuration  = shakeDuration;
        camShake.shakeAmount    = shakeAmount;
        camShake.decreaseFactor = 1f;
    }

    private void Attack()
    {
        if (attack_time <= 0f)
        {
            animation.speed = 0.3f;
            isAttacking = false;

            //~~~~~~~~~~Attacking~~~~~~~~~~// 

            Collider2D[] enemy = Physics2D.OverlapCircleAll(transform.GetChild(1).position, 0.6f, 1 << LayerMask.NameToLayer("Monsters"));

            for (int i = 0; i < enemy.Length; i++)
            {
                getEffects();
                Sound.PlaySound("S_essence_found");
                enemy[i].GetComponent<EnemyScript>().DamageTaken(base_Damage);
                break;
            }

            Collider2D boss = Physics2D.OverlapCircle(transform.GetChild(1).position, 0.5f, 1 << LayerMask.NameToLayer("Boss"));
            if(boss != null)
            {
                boss.GetComponent<BossScript>().DamageTake(base_Damage);
                Debug.Log("damageTaked");
            }

            is_attack_check = false;
        }
        else
        {
            attack_time = attack_time - Time.deltaTime;
            animation.SetInteger("anim", attack_set);
        }
    }


    void Update()
    {
        if (!allPause && !pause && !isDeath)
        {

            if ((Input.GetKeyDown(KeyCode.R) || Button_Attack_and_Jumping.is_attack_activate) && attack_time <= 0f && !is_attack_check)
            {
                
                is_attack_check = true;
                Sound.PlaySound("HeavySword"); // звук меча
                animation.speed = 0.5f; // увеличиваем анимацию атаки
                isAttacking = true;  // даем разрешение на атаку
                attack_set = Random.RandomRange(4, 7); //  рандом атаки между 1-3;
                attack_time = attack_time_base * 2; // так как анимация рано 0.5, увеличиваем до 1 чтобы небыло повторной анимации
            }
            //~~~~~~~~~~Attack~~~~~~~~~~// 

            if (isAttacking)
                Attack(); 
            else
                Moving();
        }
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

    //===============================//
    //         Замораживание         //
    //===============================//
    public void FreezePlayer(bool action)
    {
        pause = action;
        rigidbody.velocity = Vector2.zero;
        rigidbody.isKinematic = action;
        capsule_collider.isTrigger = action;
    }
    //===============================//
    //           Поражение           //
    //===============================//
    public void setLose()
    {
        lose_win = true;
        Lose.enabled = true;
        FreezePlayer(true);
        Sound.PlaySound("lose_sound");
    }
    //===============================//
    //           Победа              //
    //===============================//
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
    //===============================//
    //   Установка анимаций атаки    //
    //===============================//
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
