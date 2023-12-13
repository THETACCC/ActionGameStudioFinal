using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNT_explosion : MonoBehaviour
{
    public float explosionRadius = 5f;
    public int explosionDamage = 10;
    public LayerMask targetLayer;
    public targetDummy Dummy;
    public GameObject explosionRadiusIndicator;
    public GameObject explosionRadiusIndicator_alone;
    public GameObject explosionRadiusIndicator_super;
    public float timeToExplode = 5f;
    public float flashInterval = 0.5f;
    public Color flashColor = Color.red;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    //particles
     private ParticleSystem explosionsmall = default;
     private ParticleSystem explosionbig = default;
     private GameObject smallexplosion;
     private GameObject bigexplosion;


    //audio
    public GameObject Soundobject;
    private bool soundplayed = false;

    //instantiation
    private bool instantiated = false;

    void Start()
    {
        smallexplosion = GameObject.FindGameObjectWithTag("particles");
        bigexplosion = GameObject.FindGameObjectWithTag("particlesbig");
        explosionsmall = smallexplosion.GetComponent<ParticleSystem>();
        explosionbig = bigexplosion.GetComponent<ParticleSystem>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        StartCoroutine(FlashAndExplode());
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "HandgunBullet" || collision.gameObject.tag == "ShotgunBullet" || collision.gameObject.tag == "explosion" || collision.gameObject.tag == "explosion_rocket")
        {
            if (!soundplayed)
            {
                GameObject.Instantiate(Soundobject, this.transform.position, Quaternion.identity);
                smallexplosion.transform.position = this.transform.position;
                soundplayed = true;
            }

            explosionsmall.Play();
            Explode();
            StartCoroutine(SelfDestruct());
        }
        else if(collision.gameObject.tag == "explosion_alone")
        {
            if (!soundplayed)
            {
                GameObject.Instantiate(Soundobject, this.transform.position, Quaternion.identity);
                GameObject indicator = Instantiate(explosionRadiusIndicator_alone, transform.position, Quaternion.identity);
                StartCoroutine(SelfDestruct());
                soundplayed = true;
            }
            smallexplosion.transform.position = this.transform.position;
            explosionsmall.Play();


        }
        else if(collision.gameObject.tag == "Rocket")
        {
            if (!soundplayed)
            {
                GameObject.Instantiate(Soundobject, this.transform.position, Quaternion.identity);
                GameObject indicator = Instantiate(explosionRadiusIndicator_super, transform.position, Quaternion.identity);
                soundplayed = true;
                StartCoroutine(SelfDestruct());
            }
            bigexplosion.transform.position = this.transform.position;
            explosionbig.Play();



        }
        else if (collision.gameObject.tag == "SeekerEnemy")
        {
            if (!soundplayed)
            {
                GameObject.Instantiate(Soundobject, this.transform.position, Quaternion.identity);
                GameObject indicator = Instantiate(explosionRadiusIndicator_alone, transform.position, Quaternion.identity);
                soundplayed = true;
                StartCoroutine(SelfDestruct());
            }
            smallexplosion.transform.position = this.transform.position;
            explosionsmall.Play();


        }
        else if (collision.gameObject.tag == "Boss")
        {
            if (!soundplayed)
            {
                GameObject.Instantiate(Soundobject, this.transform.position, Quaternion.identity);
                GameObject indicator = Instantiate(explosionRadiusIndicator_alone, transform.position, Quaternion.identity);
                soundplayed = true;

                StartCoroutine(SelfDestruct());
            }
            smallexplosion.transform.position = this.transform.position;
            explosionsmall.Play();

        }
        else if (collision.gameObject.tag == "Enemy")
        {
            if (!soundplayed)
            {
                GameObject indicator = Instantiate(explosionRadiusIndicator_alone, transform.position, Quaternion.identity);
                GameObject.Instantiate(Soundobject, this.transform.position, Quaternion.identity);
                soundplayed = true;

                StartCoroutine(SelfDestruct());
            }
            smallexplosion.transform.position = this.transform.position;
            explosionsmall.Play();

        }
    }

    void Explode()
    {
 
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(transform.position, explosionRadius, targetLayer);
        ShowExplosionRadius();
    }

    void ShowExplosionRadius()
    {
        GameObject indicator = Instantiate(explosionRadiusIndicator, transform.position, Quaternion.identity);
        StartCoroutine(DestroyIndicator(indicator, 3f));
    }

    IEnumerator DestroyIndicator(GameObject indicator, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(indicator);
    }

    private IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
    IEnumerator FlashAndExplode()
    {
        float elapsed = 0f;
        while (elapsed < timeToExplode)
        {
            spriteRenderer.color = (spriteRenderer.color == originalColor) ? flashColor : originalColor;
            yield return new WaitForSeconds(flashInterval);
            elapsed += flashInterval;
        }
        if (!soundplayed)
        {
            GameObject.Instantiate(Soundobject, this.transform.position, Quaternion.identity);
            soundplayed = true;
        }
        GameObject indicator = Instantiate(explosionRadiusIndicator_alone, transform.position, Quaternion.identity);
        StartCoroutine(SelfDestruct());
    }
}
