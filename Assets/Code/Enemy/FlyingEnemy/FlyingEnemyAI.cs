using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System.Threading;


public class FlyingEnemyAI : MonoBehaviour
{

    public Transform Target;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;


    public Transform flyingenemy;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    //range
    public float detectdistance;


    //explode Timer
    private bool readytoExplode = false;
    private float time = 0;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private bool isRed = false;
    private bool effectstarted = false;
    //particles
    [SerializeField] private ParticleSystem explosion = default;

    //radius
    public GameObject explosionradius;

    //sound
    public GameObject soundObject;
    public AudioSource audioSource;
    public AudioClip soundClip;
    private bool soundplayed = false;
    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        InvokeRepeating("UpdatePath", 0f, .5f);
        Target = GameObject.FindGameObjectWithTag("Player").transform;


    }

    void UpdatePath()
    {
        float distance = Vector2.Distance(rb.transform.position, Target.position);
        if (distance < detectdistance)
        {
            if (seeker.IsDone())
            {
                seeker.StartPath(rb.position, Target.position, OnPathComplete);
            }
        }


    }


    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path= p;
            currentWaypoint= 0;
        }
    }



    private void Update()
    {
        if (readytoExplode)
        {
            PreparetoExplode();
            if (!effectstarted)
            {
                StartCoroutine(ToggleBetweenRedAndOriginal());
                effectstarted= true;
            }

        }
        float distancefromplayer = Vector2.Distance(transform.position, Target.transform.position);
        if (distancefromplayer < 20)
        {
            readytoExplode = true;
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            readytoExplode = true;

            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;


        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if (force.x >= 0.01f)
        {
            flyingenemy.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (force.x <= -0.01f)
        {
            flyingenemy.localScale = new Vector3(1f, 1f, 1f);
        }




    }

    void PreparetoExplode()
    {

        time += Time.deltaTime;
        if (time >= 3)
        {
            explosionradius.SetActive(true);
            explosion.Play();
            spriteRenderer.sprite = null;
            Invoke("killself", 1f);
            time = 0;

        }

    }

    IEnumerator ToggleBetweenRedAndOriginal()
    {
        while (true)
        {
            if (isRed)
            {
                spriteRenderer.color = originalColor;
            }
            else
            {
                spriteRenderer.color = Color.black;
                audioSource.PlayOneShot(soundClip);
            }
            isRed = !isRed;

            yield return new WaitForSeconds(.5f);
        }
    }
    void killself()
    {
        Destroy(gameObject);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "ShotgunBullet"))
        {
            if (!soundplayed)
            {
                GameObject.Instantiate(soundObject, this.transform.position, Quaternion.identity);
                soundplayed = true;
            }
            explosionradius.SetActive(true);
            explosion.Play();
            spriteRenderer.sprite = null;
            Invoke("killself", 1f);
        }
        else if ((collision.gameObject.tag == "HandgunBullet"))
        {
            if (!soundplayed)
            {
                GameObject.Instantiate(soundObject, this.transform.position, Quaternion.identity);
                soundplayed = true;
            }
            explosionradius.SetActive(true);
            explosion.Play();
            spriteRenderer.sprite = null;
            Invoke("killself", 1f);
        }
        else if (collision.gameObject.tag == "explosion" || collision.gameObject.tag == "explosion_alone" || collision.gameObject.tag == "explosion_rocket" || collision.gameObject.tag == "explosion_super")
        {
            if (!soundplayed)
            {
                GameObject.Instantiate(soundObject, this.transform.position, Quaternion.identity);
                soundplayed = true;
            }
            explosionradius.SetActive(true);
            explosion.Play();
            spriteRenderer.sprite = null;
            Invoke("killself", 1f);
        }

    }

}
