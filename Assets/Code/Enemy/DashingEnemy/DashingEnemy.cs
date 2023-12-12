using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashingEnemy : MonoBehaviour
{

    //Player Ref
    private player_controller playerControll;

    public PlayerHealthUI playerHealthUI;
    public GameObject PlayerUI;
    private GameObject player;

    public float detectdistance;
    [SerializeField] float force;

    //Self related
    private Rigidbody2D Myrb;
    [Header("For patrolling")]
    [SerializeField] float movespeed;
    private float moveDirection = 1;
    private bool facingRight = true;
    [SerializeField] Transform playerpos;
    private SpriteRenderer spriteRenderer;
    //Movement
    private float current_speed = 0f;
    public float acceleration = 5f;
    public float decceleration = 10f;
    private float lerpspeed = 2f;
    private float rotatetimer = 0f;
    private float max_hspeed = 20f;


    //find and shoot the player
    public float DashingRange;
    public float dashspeed = 200f;
    private float dashspeedHype = 0f;
    public float bouncespeed = 5000f;


    //other reference
    public bool isshield = true;

    //mannual trigger
    public bool triggered = false;
    //sound
    public GameObject soundObject;
    private bool soundplayed = false;


    public AudioSource audioSource;
    public AudioClip soundClip;
    private bool sheildbreaksound = false;
    //particles
    [SerializeField] private ParticleSystem explosion = default;
    private enum State
    {
        Idle,
        SpotPlayer,
        NormalAttack

    }
    private State state;
    // Start is called before the first frame update
    void Start()
    {
        dashspeedHype = dashspeed * 3;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Myrb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        PlayerUI = GameObject.FindGameObjectWithTag("UI");
        if (PlayerUI != null)
        {
            playerHealthUI = PlayerUI.GetComponent<PlayerHealthUI>();
        }
        playerControll = player.GetComponent<player_controller>();

        playerpos = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isshield)
        {
            if(!sheildbreaksound)
            {
                audioSource.PlayOneShot(soundClip);
                sheildbreaksound= true;
            }
        }


        current_speed = Mathf.Clamp(current_speed, 0, max_hspeed);
        switch (state)
        {
            default:
            case State.Idle:
                float distance = Vector2.Distance(transform.position, player.transform.position);

                if (current_speed > 0)
                {
                    current_speed -= decceleration * Time.deltaTime;
                }

                Myrb.velocity = new Vector2(current_speed * moveDirection, Myrb.velocity.y);


                if (triggered)
                {
                    state = State.SpotPlayer;
                }

                
                break;

            case State.SpotPlayer:

                Debug.Log("moving");
                float distancefromplayer = Vector2.Distance(transform.position, player.transform.position);
                float playerpositionturn = playerpos.position.x - transform.position.x;
                float playerverticaldist = transform.position.y - playerpos.position.y;
                rotatetimer += Time.deltaTime;
                if (rotatetimer > 2)
                {
                    if (playerpositionturn < 0)
                    {
                        moveDirection = -1;
                    }
                    else if (playerpositionturn > 0)
                    {
                        moveDirection = 1;
                    }
                    else if (playerpositionturn == 0)
                    {
                        moveDirection = 0;
                    }
                    rotatetimer = 0;
                }







                if (distancefromplayer < detectdistance && distancefromplayer > DashingRange)
                {
                    current_speed += acceleration * Time.deltaTime;
                }
                else if (current_speed > 2f)
                {
                    current_speed -= decceleration * Time.deltaTime;
                }
                else
                {
                    state = State.NormalAttack;
                }
                break;
            case State.NormalAttack:
                Vector2 direction = ((Vector2)player.transform.position - Myrb.position).normalized;
                if (isshield)
                {
                    Vector2 force = direction * dashspeed * Time.deltaTime;
                    Myrb.AddForce(force);
                }
                else
                {
                    Vector2 force = direction * dashspeedHype * Time.deltaTime;
                    Myrb.AddForce(force);
                }





                


                break;

        }




    }
    public void Trigger()
    {
        triggered = true;
        Debug.Log("ok");
    }
    void killself()
    {
        Destroy(gameObject);
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 direction = (Myrb.position - (Vector2)collision.transform.position).normalized;
        Vector2 force = direction * bouncespeed * Time.deltaTime;
        Myrb.AddForce(force, ForceMode2D.Impulse);

        if ((collision.gameObject.tag == "ShotgunBullet") && !isshield)
        {
            if (!soundplayed)
            {
                GameObject.Instantiate(soundObject, this.transform.position, Quaternion.identity);
                soundplayed = true;
            }
            explosion.Play();
            Invoke("killself", 0.5f);
            spriteRenderer.sprite = null;
        }
        else if (collision.gameObject.tag == "explosion" || collision.gameObject.tag == "explosion_alone" || collision.gameObject.tag == "explosion_rocket" || collision.gameObject.tag == "explosion_super" || collision.gameObject.tag == "Rocket")
        {
            if (!soundplayed)
            {
                GameObject.Instantiate(soundObject, this.transform.position, Quaternion.identity);
                soundplayed = true;
            }
            explosion.Play();
            spriteRenderer.sprite = null;
            Invoke("killself", 0.5f);
        }

        if (collision.gameObject.tag == "Player")
        {
            if (!playerControll.isinvisible)
            {
                playerHealthUI.health -= 10;
                bool isCriticalHit = true;
                DamagePopup.Create(collision.gameObject.transform.position, 10, isCriticalHit);
                playerControll.isinvisible = true;
                playerControll.TakeDamage();
            }
        }


    }

}
