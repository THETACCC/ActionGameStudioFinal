using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{



    private GameObject player;
    private Rigidbody2D rb;
    private player_controller playerControll;

    public PlayerHealthUI playerHealthUI;
    public GameObject PlayerUI;

    private float destorytime = 0;

    // Start is called before the first frame update
    void Start()
    {

        PlayerUI = GameObject.FindGameObjectWithTag("UI");
        if (PlayerUI != null )
        {
            playerHealthUI = PlayerUI.GetComponent<PlayerHealthUI>();
        }
        rb = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerControll = player.GetComponent<player_controller>();

       // Vector3 direction = player.transform.position - transform.position;
       // rb.velocity =  new Vector2(direction.x, direction.y).normalized * force;

       // float rot = Mathf.Atan2(-direction.y,-direction.x) * Mathf.Rad2Deg;
       // transform.rotation = Quaternion.Euler(0,0,rot);



    }

    private void Update()
    {
        destorytime += Time.deltaTime;
        if(destorytime> 7)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            if(!playerControll.isinvisible)
            {
                playerHealthUI.health -= 10;
                bool isCriticalHit = true;
                DamagePopup.Create(gameObject.transform.position, 10, isCriticalHit);
                Destroy(gameObject);
                playerControll.isinvisible = true;
            }
            else
            {
                Destroy(gameObject);
            }

        }

    }
}
