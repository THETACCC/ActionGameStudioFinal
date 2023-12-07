using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyShoot : MonoBehaviour
{
    //Game Start
    public bool isactivate = false;


    //particles
    [SerializeField] private ParticleSystem explosion_handgun = default;
    [SerializeField] private ParticleSystem explosionsmall = default;

    public GameObject bullet;
    public GameObject spawning;
    public GameObject spawningblock;
    public GameObject spawningslime;
    public Transform bulletpos;
    public Transform spawnpos;

    private float timer;

    private float spamtimer;

    private float bulletspamcount = 0;


    private float ultimatetimer;

    private float ultimatebulletcount;

    //vertical attack
    private bool playerposChecked = false;
    private float verticaltimer;

    private float bulletverticalcount  = 0;

    //CenterSpamAttack

    private float centerspambullet = 0;
    private float centerspamtimer = 0;
    private GameObject player;

    //Laser Attack
    public GameObject LaserGun;
    private float lasertimer = 0;
    public GameObject LaserGun2;
    public float detectdistance;
    [SerializeField] float force;

    //Dashing Attack
    private bool normalmovement = true;
    private float ragetimer = 0;
    public float dashspeed = 600f;


    //Self related
    private Rigidbody2D Myrb;
    [Header("For patrolling")]
    [SerializeField] float movespeed;
    private float moveDirection = 1;
    private bool facingRight = true;
    [SerializeField] Transform playerpos;


    //Movement
    [SerializeField] private float current_speed = 0f;
    public float acceleration = 5f;
    public float decceleration = 10f;
    private float lerpspeed = 2f;
    private float rotatetimer = 0f;
    public float max_hspeed = 80f;
    //vertical movement

    [SerializeField] private float current_vspeed = 0f;
    public float max_vspeed = 80f;
    private float verticaldirection = 1f;

    //find and shoot the player
    public float ShootingRange;


    //Health 
    public float Health;

    //Player Ref
    private player_controller playerControll;

    public PlayerHealthUI playerHealthUI;
    public GameObject PlayerUI;

    //invisible time
    private float invisibletime = 0f;
    private bool isinvisible = false;


    //Cinemachine 
    private CinemachineImpulseSource impluseSrouce;

    private enum State
    {
        Idle,
        SpotPlayer,
        NormalAttack,
        HeavyAttack,
        SpamAttack,
        CenterSpamAttack,
        SuperSpam,
        LaserAttack,
        SpamAttackSpawnBlock,
        SpamAttackSpawnSlime,
        SpamAttackSpawnBoomer,
        VerticalAttack,
        UltimateAttack,
        HeavyAttackV2,
        SpamAttackV2,
        SuperSpamV2,
        CenterSpamAttackV2,
        LaserAttackV2,
        RageDash,
        RageDashV2,

    }


    private State state;

    void Start()
    {
        impluseSrouce = GetComponent<CinemachineImpulseSource>();
        Health = 10000;
        Myrb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        PlayerUI = GameObject.FindGameObjectWithTag("UI");
        if (PlayerUI != null)
        {
            playerHealthUI = PlayerUI.GetComponent<PlayerHealthUI>();
        }
        playerControll = player.GetComponent<player_controller>();
    }

    void Update()
    {
        if(Health <= 0f)
        {
            Destroy(gameObject);
        }

        if(!isactivate)
        {
            return;
        }


        if(isinvisible)
        {
            invisibletime += Time.deltaTime;
            if(invisibletime >= 1)
            {
                isinvisible = false;
            }
        }

        

        float distancefromplayer = Vector2.Distance(transform.position, player.transform.position);
        float playerpositionturn = playerpos.position.x - transform.position.x;
        float playerverticaldist = transform.position.y - playerpos.position.y;

        if(normalmovement)
        {
            if (Health > 5000)
            {
                current_speed = Mathf.Clamp(current_speed, -max_hspeed, max_hspeed);
                current_vspeed = Mathf.Clamp(current_vspeed, -max_vspeed, max_vspeed);


                if (playerpositionturn < 0)
                {
                    current_speed -= acceleration * Time.deltaTime;
                }
                else if (playerpositionturn > 0)
                {
                    current_speed += acceleration * Time.deltaTime;
                }
                if (playerverticaldist < 10)
                {
                    current_vspeed += acceleration * Time.deltaTime;
                }
                else if (playerverticaldist > 10)
                {
                    current_vspeed -= acceleration * Time.deltaTime;
                }
            }
            else if (Health <= 5000)
            {
                current_speed = Mathf.Clamp(current_speed, -max_hspeed * 1.5f, max_hspeed * 1.5f);
                current_vspeed = Mathf.Clamp(current_vspeed, -max_vspeed * 1.5f, max_vspeed * 1.5f);


                if (playerpositionturn < 0)
                {
                    current_speed -= acceleration * Time.deltaTime * 1.5f;
                }
                else if (playerpositionturn > 0)
                {
                    current_speed += acceleration * Time.deltaTime * 1.5f;
                }
                if (playerverticaldist < 10)
                {
                    current_vspeed += acceleration * Time.deltaTime * 1.5f;
                }
                else if (playerverticaldist > 10)
                {
                    current_vspeed -= acceleration * Time.deltaTime * 1.5f;
                }
            }


            rotatetimer += Time.deltaTime;
        }

        if (distancefromplayer < detectdistance && distancefromplayer > ShootingRange)
        {
 
            //current_speed += acceleration * Time.deltaTime;
            //current_vspeed += acceleration * Time.deltaTime;
        }
















        Myrb.velocity = new Vector2(current_speed , current_vspeed);
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
                if (distance < detectdistance)
                {
                     
                   state = State.SpotPlayer;
                }
                break;

            case State.SpotPlayer:
                //float distancefromplayer = Vector2.Distance(transform.position, player.transform.position);

                if (distancefromplayer < ShootingRange)
                {
                    //current_speed -= decceleration * Time.deltaTime;
                    //current_vspeed -= decceleration * Time.deltaTime;
                    if (Health > 5000)
                    {
                        timer += Time.deltaTime;
                        if (timer > 0.5)
                        {
                            timer = 0;

                            int attack = UnityEngine.Random.Range(0, 16);

                            if (attack == 0)
                            {
                                state = State.NormalAttack;
                            }
                            else if (attack == 1 || attack == 2)
                            {
                                state = State.HeavyAttack;
                            }
                            else if (attack == 3 || attack == 4 || attack == 5 || attack == 9)
                            {
                                state = State.SuperSpam;
                            }
                            else if (attack == 6 || attack == 7 || attack == 8 )
                            {
                                state = State.RageDash;
                            }
                            else if (attack == 10 || attack == 11 || attack == 12)
                            {
                                state = State.SpamAttack;
                            }
                            else if (attack == 13 || attack == 14 || attack == 15)
                            {
                                state = State.LaserAttack;
                            }


                        }
                    }
                    else if (Health <= 5000)
                    {
                        timer += Time.deltaTime;
                        if (timer > 0.3)
                        {
                            timer = 0;

                            int attack = UnityEngine.Random.Range(0, 29);

                            if (attack == 0 || attack == 1 || attack == 2 || attack == 24)
                            {
                                state = State.HeavyAttackV2;
                            }
                            else if (attack == 3 || attack == 4 || attack == 5)
                            {
                                state = State.SuperSpamV2;
                            }
                            else if (attack == 6 || attack == 7 || attack == 8 || attack == 9)
                            {
                                state = State.CenterSpamAttackV2;
                            }
                            else if (attack == 10 || attack == 11 || attack == 12)
                            {
                                state = State.SpamAttackV2;
                            }
                            else if (attack == 13 || attack == 14 || attack == 15 || attack == 23)
                            {
                                state = State.LaserAttackV2;
                            }
                            else if (attack == 16 || attack == 17 || attack == 22 || attack == 21)
                            {
                                state = State.SpamAttackSpawnBlock;
                            }
                            else if (attack == 18 || attack == 19 || attack == 20 )
                            {
                                state = State.RageDashV2;
                            }
                            else if (attack == 25 || attack == 26 || attack == 27 || attack == 28)
                            {
                                state = State.SpamAttackSpawnBoomer;
                            }

                        }
                    }

                }
                //Myrb.velocity = Vector2.Lerp(Myrb.velocity, new Vector2(current_speed * moveDirection, current_vspeed * verticaldirection), Time.deltaTime * lerpspeed);
 






                break;

            case State.NormalAttack:
                NormalShoot();



                break;

            case State.HeavyAttack:
                HeavyShoot();





                break;

            case State.SpamAttack:
                SpamAttack();

                break;

            case State.CenterSpamAttack:
                centerSpam();

                break;
            case State.SuperSpam:
                SuperSpam();


                break;

            case State.LaserAttack:
                LaserAttack();

                break;

            case State.SpamAttackSpawnBlock:
                SpamAttackSpawnBlock();

                break;
            case State.SpamAttackSpawnSlime:
                SpamAttackSpawnSlime();

                break;
            case State.SpamAttackSpawnBoomer:
                SpamAttackSpawnBoomer();

                break;
            case State.HeavyAttackV2:
                HeavyAttackV2();



                break;
            case State.SuperSpamV2:
                SuperSpamV2();



                break;
            case State.CenterSpamAttackV2:
                centerSpamV2();

                break;
            case State.SpamAttackV2:
                SpamAttackV2();

                break;
            case State.LaserAttackV2:
                LaserAttackV2();

                break;
            case State.RageDash:
                normalmovement = false;
                RageDash();

                break;
            case State.RageDashV2:
                normalmovement = false;
                RageDashV2();

                break;
            case State.VerticalAttack:
                if(!playerposChecked)
                {
                    float playerposition = playerpos.position.x - transform.position.x;
                    if (playerposition < 0)
                    {
                        moveDirection = -1;
                    }
                    else if (playerposition > 0)
                    {
                        moveDirection = 1;
                    }
                    playerposChecked = true;
                }
                VerticalAttack();








                break;





        }








    }

    void NormalShoot()
    {
        GameObject bullet_obj = Instantiate(bullet,bulletpos.position, Quaternion.identity);
        Vector3 direction = player.transform.position - transform.position;
        Rigidbody2D rb = bullet_obj.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        state = State.Idle;
    }
    void HeavyShoot()
    {
        for (int i = 0;i < 4; i++)
        {
            GameObject bullet_obj = Instantiate(bullet, bulletpos.position, Quaternion.identity);
            Vector3 direction = player.transform.position - transform.position;
            Rigidbody2D rb = bullet_obj.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(direction.x + i*5, direction.y + i * 5).normalized * force;
        }


        state = State.Idle;
    }
    void SpamAttack()
    {
        current_speed = Mathf.Clamp(current_speed, -10, 10);
        current_vspeed = Mathf.Clamp(current_vspeed, -10, 10);
        //Myrb.velocity = new Vector2(0,0);
        if (bulletspamcount < 30)
        {

            spamtimer += Time.deltaTime;
            if (spamtimer > 0.15)
            {

                spamtimer = 0;
                GameObject bullet_obj = Instantiate(bullet, bulletpos.position, Quaternion.identity);
                Vector3 direction = player.transform.position - transform.position;
                Rigidbody2D rb = bullet_obj.GetComponent<Rigidbody2D>();
                rb.velocity = new Vector2(direction.x + UnityEngine.Random.Range(-25,25), direction.y + UnityEngine.Random.Range(-5, 5)).normalized * force;
                bulletspamcount += 1;
            }
        }
        else if (bulletspamcount >= 30)
        {
            bulletspamcount = 0;
            state = State.Idle;
        }
    }

    void centerSpam()
    {
        current_speed = Mathf.Clamp(current_speed, -1, 1);
        current_vspeed = Mathf.Clamp(current_vspeed, -1, 1);
        if (centerspambullet < 3)
        {
            centerspamtimer+= Time.deltaTime;
            if(centerspamtimer > 1)
            {
                for (int i = 0; i < 24; i++)
                {

                    GameObject bullet_obj = Instantiate(bullet, bulletpos.position, Quaternion.identity);
                    Vector3 direction = player.transform.position - transform.position;
                    // Calculate the rotation angle for this bullet
                    float rotationAngle = i * 15;
                    // Create a rotation Quaternion
                    Quaternion rotation = Quaternion.Euler(0, 0, rotationAngle);

                    // Rotate the direction vector by the rotation
                    Vector3 rotatedDirection = rotation * direction.normalized;

                    Rigidbody2D rb = bullet_obj.GetComponent<Rigidbody2D>();
                    rb.velocity = rotatedDirection * force;

                    // Apply the rotation to the bullet object for visual orientation
                    bullet_obj.transform.rotation = rotation;

                }
                centerspamtimer = 0;
                centerspambullet++;
            }
        }
        else if (centerspambullet >= 3)
        {

            centerspambullet = 0;
            state = State.SpotPlayer;
        }

    }

    void SuperSpam()
    {
        current_speed = Mathf.Clamp(current_speed, -20, 20);
        current_vspeed = Mathf.Clamp(current_vspeed, -20, 20);
        //Myrb.velocity = new Vector2(0,0);
        if (bulletspamcount < 100)
        {

            spamtimer += Time.deltaTime;
            if (spamtimer > 0.05)
            {
                spamtimer = 0;
                GameObject bullet_obj = Instantiate(bullet, bulletpos.position, Quaternion.identity);
                Vector3 direction = player.transform.position - transform.position;
                // Calculate the rotation angle for this bullet
                float rotationAngle = bulletspamcount * 10;
                // Create a rotation Quaternion
                Quaternion rotation = Quaternion.Euler(0, 0, rotationAngle);

                // Rotate the direction vector by the rotation
                Vector3 rotatedDirection = rotation * direction.normalized;

                Rigidbody2D rb = bullet_obj.GetComponent<Rigidbody2D>();
                rb.velocity = rotatedDirection * force;

                // Apply the rotation to the bullet object for visual orientation
                bullet_obj.transform.rotation = rotation;
                bulletspamcount += 1;
            }
        }
        else if (bulletspamcount >= 100)
        {
            bulletspamcount = 0;
            state = State.SpotPlayer;
        }

    }

    void SpamAttackSpawnBlock()
    {
        Myrb.velocity = new Vector2(0, 0);
        if (bulletspamcount < 2)
        {

            spamtimer += Time.deltaTime;
            if (spamtimer > 0.15)
            {

                spamtimer = 0;
                GameObject bullet_obj = Instantiate(spawningblock, spawnpos.position, Quaternion.identity);
                Vector3 direction = player.transform.position - transform.position;
                Rigidbody2D rb = bullet_obj.GetComponent<Rigidbody2D>();
                DashingEnemy dashing = bullet_obj.GetComponent<DashingEnemy>();
                dashing.triggered = true;
                rb.velocity = new Vector2(direction.x + UnityEngine.Random.Range(-5, 5), direction.y + UnityEngine.Random.Range(-5, 5)).normalized * force;
                bulletspamcount += 1;
            }
        }
        else if (bulletspamcount >= 2)
        {
            bulletspamcount = 0;
            state = State.Idle;
        }
    }

    void LaserAttack()
    {

        current_speed = Mathf.Clamp(current_speed, -1, 1);
        current_vspeed = Mathf.Clamp(current_vspeed, -1, 1);
        LaserGun.SetActive(true);
        lasertimer += Time.deltaTime;
        if (lasertimer >= 10)
        {
            LaserGun.gameObject.transform.rotation = Quaternion.Euler(0, 0, -90);
            LaserGun.SetActive(false);
            lasertimer = 0;
            state = State.SpotPlayer;

        }
        
    }


    void SpamAttackSpawnSlime()
    {
        Myrb.velocity = new Vector2(0, 0);
        if (bulletspamcount < 2)
        {

            spamtimer += Time.deltaTime;
            if (spamtimer > 0.15)
            {

                spamtimer = 0;
                GameObject bullet_obj = Instantiate(spawningslime, spawnpos.position, Quaternion.identity);
                Vector3 direction = player.transform.position - transform.position;
                Rigidbody2D rb = bullet_obj.GetComponent<Rigidbody2D>();
                rb.velocity = new Vector2(direction.x + UnityEngine.Random.Range(-5, 5), direction.y + UnityEngine.Random.Range(-5, 5)).normalized * force;
                bulletspamcount += 1;
            }
        }
        else if (bulletspamcount >= 2)
        {
            bulletspamcount = 0;
            state = State.Idle;
        }
    }

    void SpamAttackSpawnBoomer()
    {
        Myrb.velocity = new Vector2(0, 0);
        if (bulletspamcount < 5)
        {

            spamtimer += Time.deltaTime;
            if (spamtimer > 0.15)
            {

                spamtimer = 0;
                GameObject bullet_obj = Instantiate(spawning, spawnpos.position, Quaternion.identity);
                Vector3 direction = player.transform.position - transform.position;
                Rigidbody2D rb = bullet_obj.GetComponent<Rigidbody2D>();
                rb.velocity = new Vector2(direction.x + UnityEngine.Random.Range(-5, 5), direction.y + UnityEngine.Random.Range(-5, 5)).normalized * force;
                bulletspamcount += 1;
            }
        }
        else if (bulletspamcount >= 2)
        {
            bulletspamcount = 0;
            state = State.SpotPlayer;
        }
    }


    void VerticalAttack()
    {
        current_speed += acceleration * Time.deltaTime;


        float playerverticaldist = transform.position.y - playerpos.position.y;
        if (playerverticaldist < 10)
        {
            verticaldirection = 1;
            current_vspeed += acceleration * Time.deltaTime;
        }
        else if (playerverticaldist > 10 && playerverticaldist < 15)
        {
            verticaldirection = 0;
            current_vspeed -= decceleration * Time.deltaTime;
        }
        else if (playerverticaldist > 15)
        {
            verticaldirection = -1;
            current_vspeed += acceleration * Time.deltaTime;
        }
        Myrb.velocity = Vector2.Lerp(Myrb.velocity, new Vector2(current_speed * moveDirection, current_vspeed * verticaldirection), Time.deltaTime * lerpspeed);
        if (bulletverticalcount < 10)
        {

            verticaltimer += Time.deltaTime;
            if (verticaltimer > 0.3)
            {
                verticaltimer = 0;
                GameObject bullet_obj = Instantiate(bullet, bulletpos.position, Quaternion.identity);
                Vector3 direction = player.transform.position - transform.position;
                Rigidbody2D rb = bullet_obj.GetComponent<Rigidbody2D>();
                rb.velocity = new Vector2(0, -1).normalized * force;
                bulletverticalcount += 1;
            }
        }
        else
        {
            playerposChecked = false;
            bulletverticalcount = 0;
            state = State.SpotPlayer;
        }




    }

    void HeavyAttackV2()
    {
        for (int i = 0; i < 8; i++)
        {
            GameObject bullet_obj = Instantiate(bullet, bulletpos.position, Quaternion.identity);
            Vector3 direction = player.transform.position - transform.position;
            Rigidbody2D rb = bullet_obj.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(direction.x + i * 5, direction.y + i * 5).normalized * force;
        }


        state = State.Idle;
    }

    void SuperSpamV2()
    {
        current_speed = Mathf.Clamp(current_speed, -50, 50);
        current_vspeed = Mathf.Clamp(current_vspeed, -50, 50);
        //Myrb.velocity = new Vector2(0,0);
        if (bulletspamcount < 100)
        {

            spamtimer += Time.deltaTime;
            if (spamtimer > 0.1)
            {
                spamtimer = 0;
                GameObject bullet_obj = Instantiate(bullet, bulletpos.position, Quaternion.identity);
                GameObject bullet_obj2 = Instantiate(bullet, bulletpos.position, Quaternion.identity);
                Vector3 direction = player.transform.position - transform.position;
                // Calculate the rotation angle for this bullet
                float rotationAngle = bulletspamcount * 10;
                // Create a rotation Quaternion
                Quaternion rotation = Quaternion.Euler(0, 0, rotationAngle);
                Quaternion rotation2 = Quaternion.Euler(0, 0, rotationAngle + 90);
                // Rotate the direction vector by the rotation
                Vector3 rotatedDirection = rotation * direction.normalized;
                Vector3 rotatedDirection2 = rotation2 * direction.normalized;
                Rigidbody2D rb = bullet_obj.GetComponent<Rigidbody2D>();
                rb.velocity = rotatedDirection * force;
                Rigidbody2D rb2 = bullet_obj2.GetComponent<Rigidbody2D>();
                rb2.velocity = rotatedDirection2 * force;
                // Apply the rotation to the bullet object for visual orientation
                bullet_obj.transform.rotation = rotation;
                bulletspamcount += 1;
            }
        }
        else if (bulletspamcount >= 100)
        {
            bulletspamcount = 0;
            state = State.SpotPlayer;
        }

    }

    void centerSpamV2()
    {
        current_speed = Mathf.Clamp(current_speed, -50, 50);
        current_vspeed = Mathf.Clamp(current_vspeed, -50, 50);
        if (centerspambullet < 5)
        {
            centerspamtimer += Time.deltaTime;
            if (centerspamtimer > 1)
            {
                for (int i = 0; i < 24; i++)
                {

                    GameObject bullet_obj = Instantiate(bullet, bulletpos.position, Quaternion.identity);
                    Vector3 direction = player.transform.position - transform.position;
                    // Calculate the rotation angle for this bullet
                    float rotationAngle = i * 15f;
                    // Create a rotation Quaternion
                    Quaternion rotation = Quaternion.Euler(0, 0, rotationAngle);

                    // Rotate the direction vector by the rotation
                    Vector3 rotatedDirection = rotation * direction.normalized;

                    Rigidbody2D rb = bullet_obj.GetComponent<Rigidbody2D>();
                    rb.velocity = rotatedDirection * force;

                    // Apply the rotation to the bullet object for visual orientation
                    bullet_obj.transform.rotation = rotation;

                }
                centerspamtimer = 0;
                centerspambullet++;
            }
        }
        else if (centerspambullet >= 5)
        {

            centerspambullet = 0;
            state = State.SpotPlayer;
        }

    }

    void SpamAttackV2()
    {
        //Myrb.velocity = new Vector2(0,0);
        if (bulletspamcount < 60)
        {

            spamtimer += Time.deltaTime;
            if (spamtimer > 0.1)
            {

                spamtimer = 0;
                GameObject bullet_obj = Instantiate(bullet, bulletpos.position, Quaternion.identity);
                Vector3 direction = player.transform.position - transform.position;
                Rigidbody2D rb = bullet_obj.GetComponent<Rigidbody2D>();
                rb.velocity = new Vector2(direction.x + UnityEngine.Random.Range(-50,50), direction.y + UnityEngine.Random.Range(-50, 50)).normalized * force;
                bulletspamcount += 1;
            }
        }
        else if (bulletspamcount >= 60)
        {
            bulletspamcount = 0;
            state = State.Idle;
        }
    }

    void LaserAttackV2()
    {

        current_speed = Mathf.Clamp(current_speed, -10, 10);
        current_vspeed = Mathf.Clamp(current_vspeed, -10, 10);
        LaserGun.SetActive(true);
        LaserGun2.SetActive(true);
        lasertimer += Time.deltaTime;
        if (lasertimer >= 10)
        {
            LaserGun2.gameObject.transform.rotation = Quaternion.Euler(0, 0, -270);
            LaserGun.gameObject.transform.rotation = Quaternion.Euler(0, 0, -90);
            LaserGun.SetActive(false);
            LaserGun2.SetActive(false);
            lasertimer = 0;
            state = State.SpotPlayer;

        }

    }

    void RageDash()
    {
        ragetimer += Time.deltaTime;
        current_speed = Mathf.Clamp(current_speed, -max_hspeed * 3, max_hspeed * 3);
        current_vspeed = Mathf.Clamp(current_vspeed, -max_vspeed * 3, max_vspeed * 3);
        float distancefromplayer = Vector2.Distance(transform.position, player.transform.position);
        float playerpositionturn = playerpos.position.x - transform.position.x;
        float playerverticaldist = transform.position.y - playerpos.position.y;

        if (playerpositionturn < 0)
        {
            current_speed -= acceleration * Time.deltaTime * 3;
        }
        else if (playerpositionturn > 0)
        {
            current_speed += acceleration * Time.deltaTime * 3;
        }
        if (playerverticaldist < 0)
        {
            current_vspeed += acceleration * Time.deltaTime * 3;
        }
        else if (playerverticaldist > 0)
        {
            current_vspeed -= acceleration * Time.deltaTime * 3;
        }
        if (ragetimer >= 7)
        {
            normalmovement = true;
            ragetimer = 0;
            state = State.SpotPlayer;
        }
    }

    void RageDashV2()
    {
        ragetimer += Time.deltaTime;
        current_speed = Mathf.Clamp(current_speed, -max_hspeed * 6, max_hspeed * 6);
        current_vspeed = Mathf.Clamp(current_vspeed, -max_vspeed * 6, max_vspeed * 6);
        float distancefromplayer = Vector2.Distance(transform.position, player.transform.position);
        float playerpositionturn = playerpos.position.x - transform.position.x;
        float playerverticaldist = transform.position.y - playerpos.position.y;

        if (playerpositionturn < 0)
        {
            current_speed -= acceleration * Time.deltaTime * 6;
        }
        else if (playerpositionturn > 0)
        {
            current_speed += acceleration * Time.deltaTime * 6;
        }
        if (playerverticaldist < 0)
        {
            current_vspeed += acceleration * Time.deltaTime * 6;
        }
        else if (playerverticaldist > 0)
        {
            current_vspeed -= acceleration * Time.deltaTime * 6;
        }
        if (ragetimer >= 7)
        {
            normalmovement = true;
            ragetimer = 0;
            state = State.SpotPlayer;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
            if (collision.gameObject.tag == "HandgunBullet")
            {
                bool isCriticalHit = false;
                DamagePopup.Create(collision.transform.position, 25, isCriticalHit);
                explosion_handgun.transform.position = collision.transform.position + new Vector3(1, 0, 0);
                explosion_handgun.Play();
                Health -= 25;
            }
            else if (collision.gameObject.tag == "ShotgunBullet")
            {
                bool isCriticalHit = false;
                DamagePopup.Create(collision.transform.position, 50, isCriticalHit);
                explosion_handgun.transform.position = collision.transform.position + new Vector3(1, 0, 0);
                explosion_handgun.Play();
                Health -= 50;
            }
            else if (collision.gameObject.tag == "explosion" && !isinvisible)
            {
                bool isCriticalHit = false;
                DamagePopup.Create(collision.transform.position, 100, isCriticalHit);
                Health -= 100;
                isinvisible = true;

            }
            else if (collision.gameObject.tag == "explosion_alone" && !isinvisible)
            {
                bool isCriticalHit = false;
                DamagePopup.Create(collision.transform.position, 100, isCriticalHit);
                Health -= 100;
                isinvisible = true;
            }
            else if (collision.gameObject.tag == "explosion_rocket" && !isinvisible)
            {
                bool isCriticalHit = false;
                DamagePopup.Create(collision.transform.position, 150, isCriticalHit);

                explosionsmall.transform.position = collision.transform.position + new Vector3(1, 0, 0);
                explosionsmall.Play();
                Health -= 150;
                isinvisible = true;

            }
            else if (collision.gameObject.tag == "explosion_super" && !isinvisible)
            {
                bool isCriticalHit = true;
                DamagePopup.Create(collision.transform.position + new Vector3(4, 0, 0), 500, isCriticalHit);
                explosionsmall.transform.position = collision.transform.position + new Vector3(1, 0, 0);
                explosionsmall.Play();
                Health -= 500;
                isinvisible = true;
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


    public void Acitavte()
    {
        isactivate= true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectdistance);
        Gizmos.DrawWireSphere(transform.position, ShootingRange);
    }

}
