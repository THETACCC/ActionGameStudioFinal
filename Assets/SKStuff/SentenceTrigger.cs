using SKCell;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentenceTrigger : MonoBehaviour
{

    [SerializeField] SKDialoguePlayer player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player_controller controll = collision.gameObject.GetComponent<player_controller>();
            Rigidbody2D playerRB = collision.gameObject.GetComponent<Rigidbody2D>();
            controll.current_speed_left = 0f;
            controll.current_speed_right = 0f;
            playerRB.velocity = new Vector2(0f, playerRB.velocity.y);
            player.Play();
        }
    }
}
