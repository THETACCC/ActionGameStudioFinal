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
            player.Play();
        }
    }
}
