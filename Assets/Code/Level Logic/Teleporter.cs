using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform teleportLocation; // The location to teleport the player to

    void OnCollisionEnter2D(Collision2D other)
    {

            other.gameObject.transform.position = teleportLocation.position; // Teleport the player

    }
}
