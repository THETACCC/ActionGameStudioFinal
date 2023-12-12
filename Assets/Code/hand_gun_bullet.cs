using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hand_gun_bullet : MonoBehaviour
{


    public float accelerationRate = 0.7f;
    public Rigidbody2D rb;
    //effects
    private float speed = 2f;

    public MMFeedbacks deadfeedback;
    private SpriteRenderer renderer;
    private bool effectplayed = false;

    //sound
    public GameObject soundObject;
    private bool soundplayed = false;

    void Update()
    {
        renderer = GetComponent<SpriteRenderer>();
        Vector3 newScale = transform.localScale;
        newScale.x = Mathf.Lerp(newScale.x, 0.5f, Time.deltaTime * speed);
        newScale.y = Mathf.Lerp(newScale.y, 2f, Time.deltaTime * speed);
        transform.localScale = newScale;
        rb.velocity = rb.velocity.normalized * (rb.velocity.magnitude + accelerationRate * Time.deltaTime);
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Dummy")
        {

            bool isCriticalHit = false;
            DamagePopup.Create(gameObject.transform.position, 25, isCriticalHit);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "EnemyBullet")
        {
            Destroy(collision.gameObject);
        }
        else if (!(collision.gameObject.tag == "InvisibleWall"))
        {
            if (!soundplayed)
            {
                //GameObject.Instantiate(soundObject, this.transform.position, Quaternion.identity);
                soundplayed = true;
            }
            renderer.enabled = false;
            rb.velocity = new Vector2 (0f, 0f);
            if(effectplayed == false)
            {
                deadfeedback?.PlayFeedbacks();
                effectplayed = true;
            }

        }


    }
}
