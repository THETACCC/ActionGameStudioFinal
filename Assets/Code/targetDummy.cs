using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class targetDummy : MonoBehaviour
{
    //particles
    [SerializeField] private ParticleSystem explosion_handgun = default;
    [SerializeField] private ParticleSystem explosionsmall = default;


    //Color lerp
    [SerializeField][Range(0f, 100f)] float lerpTime;
    [SerializeField] Color[] myColors;
    public int colorIndex = 0;
    float t = 0f;   
    int len;
    private bool colorchange = false;
    private float timer = 0.1f;
    private float time = 0f;

    //squash when hit
    private bool starteffect;
    private float effect_speed = 20f;
    public Transform pivot;


    private SpriteRenderer spriteRenderer;
    public TextMeshProUGUI healthText;
    public float velocityDamageMultiplier = 100f;
    public int points = 0;

    //sound
    public AudioClip beinghit;
    public AudioClip explode;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        len = myColors.Length;
    }

    public void UpdateHealthText()
    {
        healthText.text = "Points:" + points;
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "HandgunBullet")
        {
            bool isCriticalHit = false;
            DamagePopup.Create(collision.transform.position, 25, isCriticalHit);
            explosion_handgun.transform.position = collision.transform.position + new Vector3(1, 0, 0);
            explosion_handgun.Play();

            Destroy(collision.gameObject);


        }
        else if (collision.gameObject.tag == "ShotgunBullet")
        {
            bool isCriticalHit = false;
            DamagePopup.Create(collision.transform.position, 50, isCriticalHit);
            explosion_handgun.transform.position = collision.transform.position + new Vector3(1, 0, 0);
            explosion_handgun.Play();
  
            Destroy(collision.gameObject);


        }
        else if (collision.gameObject.tag == "explosion" )
        {
            bool isCriticalHit = false;
            DamagePopup.Create(collision.transform.position, 100, isCriticalHit);

            Destroy(collision.gameObject);




        }
        else if (collision.gameObject.tag == "explosion_alone" )
        {
            bool isCriticalHit = false;
            DamagePopup.Create(collision.transform.position, 100, isCriticalHit);

            Destroy(collision.gameObject);

        }
        else if (collision.gameObject.tag == "explosion_rocket" )
        {
            bool isCriticalHit = false;
            DamagePopup.Create(collision.transform.position, 150, isCriticalHit);

            explosionsmall.transform.position = collision.transform.position + new Vector3(1, 0, 0);
            explosionsmall.Play();
            Destroy(collision.gameObject);


        }
        else if (collision.gameObject.tag == "explosion_super" )
        {
            bool isCriticalHit = true;
            DamagePopup.Create(collision.transform.position + new Vector3(4, 0, 0), 500, isCriticalHit);
            explosionsmall.transform.position = collision.transform.position + new Vector3(1, 0, 0);
            explosionsmall.Play();
            Destroy(collision.gameObject);
        }
        colorIndex = 1;
        colorchange = true;
        time = timer;
        starteffect = true;
        UpdateHealthText();
    }

    private void FixedUpdate()
    {
        if (starteffect)
        {
            Vector3 newScale = pivot.localScale;
            newScale.x = Mathf.Lerp(newScale.x, 0.6f, Time.deltaTime * effect_speed);
            newScale.y = Mathf.Lerp(newScale.y, 1.25f, Time.deltaTime * effect_speed);
            pivot.localScale = newScale;
            pivot.rotation = Quaternion.Lerp(pivot.rotation, Quaternion.Euler(new Vector3(0, 0, -10)), Time.deltaTime * effect_speed);

            Debug.Log(newScale.x);
            if (newScale.x <= 0.64f)
            {
                starteffect = false;
            }
        }
        else if (!starteffect)
        {
            Vector3 newScale = pivot.localScale;
            newScale.x = Mathf.Lerp(newScale.x, 1f, Time.deltaTime * effect_speed);
            newScale.y = Mathf.Lerp(newScale.y, 1f, Time.deltaTime * effect_speed);
            pivot.localScale = newScale;
            pivot.rotation = Quaternion.Lerp(pivot.rotation, Quaternion.Euler(new Vector3(0, 0, 0)), Time.deltaTime * effect_speed);
        }


        if (colorchange)
        {
            time -= Time.deltaTime;
        }
        if (time < 0)
        {
            colorchange = false;
            colorIndex = 0;
        }
        spriteRenderer.color = Color.Lerp(spriteRenderer.color, myColors[colorIndex], lerpTime * Time.deltaTime);





        //spriteRenderer.color = Color.white;
    }

}
