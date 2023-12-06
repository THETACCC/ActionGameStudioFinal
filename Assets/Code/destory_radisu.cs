using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class destory_radisu : MonoBehaviour
{

    public float timeLeft = 1f;

    private GameObject dummy;
    //Player Ref
    private player_controller playerControll;
    public GameObject player;
    public PlayerHealthUI playerHealthUI;
    public GameObject PlayerUI;

    private void Start()
    {
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
        timeLeft -= Time.deltaTime;


        if (timeLeft <= 0)
        {
            Destroy(gameObject);
        }
    }

    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!playerControll.isinvisible)
            {
                playerHealthUI.health -= 10;
                bool isCriticalHit = true;
                DamagePopup.Create(collision.gameObject.transform.position, 10, isCriticalHit);
                playerControll.isinvisible = true;
            }
        }
        if (collision.gameObject.tag == "Boss")
        {
            Destroy(this.gameObject);
        }
    }
    
}