using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimeenemymovc : MonoBehaviour
{
    [Header("For patrolling")]
    [SerializeField] float movespeed;
    private float moveDirection = 1;
    private bool facingRight = true;
    [SerializeField] Transform groundcheck;
    [SerializeField] Transform wallcheck;
    [SerializeField] float circleradius;
    [SerializeField] LayerMask groundLayer;
    private bool checkingGround;
    private bool checkingWall;

    [Header("For Jump Attack")]
    [SerializeField] float jumpheight1;
    [SerializeField] float jumpheight2;
    //[SerializeField] Transform player;
    [SerializeField] Transform groundcheckattack;
    [SerializeField] Vector2 boxsize;
    [SerializeField] private bool isgrounded;

    [Header("For Seeing Player")]
    [SerializeField] Vector2 lineofsite;
    [SerializeField] LayerMask playerLayer;
    private bool canseeplayer;
    private Animator enemyAnim;
    public Animator deathanimation;

    [Header("Others")]
    private Rigidbody2D enemyRB;
    private SpriteRenderer renderer;
    private BoxCollider2D collider;
    //particles
    [SerializeField] private ParticleSystem explosion = default;


    //Player Ref
    private player_controller playerControll;

    public PlayerHealthUI playerHealthUI;
    public GameObject PlayerUI;
    private GameObject player;

    //sound
    public GameObject soundObject;


    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        enemyRB = GetComponent<Rigidbody2D>();
        enemyAnim = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        PlayerUI = GameObject.FindGameObjectWithTag("UI");
        if (PlayerUI != null)
        {
            playerHealthUI = PlayerUI.GetComponent<PlayerHealthUI>();
        }
        playerControll = player.GetComponent<player_controller>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        checkingGround = Physics2D.OverlapCircle(groundcheck.position, circleradius, groundLayer);
        checkingWall = Physics2D.OverlapCircle(wallcheck.position, circleradius, groundLayer);
        isgrounded = Physics2D.OverlapBox(groundcheckattack.position, boxsize, 0, groundLayer);
        canseeplayer = Physics2D.OverlapBox(transform.position, lineofsite, 0, playerLayer);
        AnimationController();
        if (!canseeplayer && isgrounded)
        {

            Patrolling();
        }




    }

    void Patrolling()
    {
        if (!checkingGround || checkingWall)
        {
            if (facingRight)
            {
                Flip();
            }
            else if (!facingRight)
            {
                Flip();
            }
        }

        enemyRB.velocity = new Vector2(movespeed * moveDirection, enemyRB.velocity.y);
    }

    void JumpAttack()
    {
        float distanceFromPlayer = player.transform.position.x - transform.position.x;

        if (isgrounded)
        {
            enemyRB.AddForce(new Vector2(distanceFromPlayer * 4, Random.Range(jumpheight1, jumpheight2)), ForceMode2D.Impulse);
        }


    }

    void FlipTowardsPlayer()
    {

        float playerposition = player.transform.position.x - transform.position.x;
        if (playerposition < 0 && facingRight)
        {
            Flip();
        }
        else if (playerposition > 0 && facingRight)
        {
            Flip();
        }

    }


    void Flip()
    {
        moveDirection *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    void AnimationController()
    {
        enemyAnim.SetBool("canseeplayer", canseeplayer);
        enemyAnim.SetBool("isgrounded", isgrounded);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, lineofsite);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ShotgunBullet" || collision.gameObject.tag == "HandgunBullet" || collision.gameObject.tag == "explosion" || collision.gameObject.tag == "explosion_alone" || collision.gameObject.tag == "explosion_rocket" || collision.gameObject.tag == "explosion_super" || collision.gameObject.tag == "Rocket")
        {
            GameObject.Instantiate(soundObject, this.transform.position, Quaternion.identity);
            deathanimation.SetBool("Death", true);
            collider.enabled = false;
            StartCoroutine(FadeOutSprite(0.5f));
            explosion.Play();
        }
        else
        {
            Flip();
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
                Flip();
            }
        }

    }
    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    private IEnumerator FadeOutSprite(float duration)
    {
        float counter = 0;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, counter / duration);
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, alpha);
            yield return null;
        }
    }

}
