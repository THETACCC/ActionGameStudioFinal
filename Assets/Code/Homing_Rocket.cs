using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homing_Rocket : MonoBehaviour
{
    public float explosionRadius = 5f;
    public int explosionDamage = 10;
    public LayerMask targetLayer;

    public Transform target;
    public float speed = 5f;
    private Rigidbody2D rb;
    public targetDummy Dummy;
    public GameObject explosionRadiusIndicator;

    //particles
    [SerializeField] private ParticleSystem explosion = default;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        UpdateTarget(); // Initial target update
    }

    void Update()
    {
        Vector2 direction = (Vector2)target.position - rb.position;
        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction, transform.up).z;

        rb.angularVelocity = -rotateAmount * 200f;
        rb.velocity = transform.up * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        explosion.Play();
        if (collision.gameObject.tag == "HandgunBullet" || collision.gameObject.tag == "ShotgunBullet" || collision.gameObject.tag == "explosion" || collision.gameObject.tag == "Dummy" || collision.gameObject.tag == "TNT")
        {
            explosion.Play();
            Explode();
            StartCoroutine(SelfDestruct());
        }
        else if (collision.gameObject.tag == "explosion_alone")
        {
            Explode();
 
            //GameObject indicator = Instantiate(explosionRadiusIndicator_alone, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Boss")
        {
            explosion.Play();
            Explode();
            StartCoroutine(SelfDestruct());
        }
        else if (collision.gameObject.tag == "EnemyBUllet")
        {
            explosion.Play();
            Explode();
            Destroy(collision.gameObject);
        }
        else
        {
            explosion.Play();
            Explode();
            StartCoroutine(SelfDestruct());
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
    }

    private IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(0.05f);
        Destroy(gameObject);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null) // You can adjust the range as needed
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }


}
