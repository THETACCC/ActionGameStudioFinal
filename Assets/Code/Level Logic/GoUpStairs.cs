using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoUpStairs : MonoBehaviour
{
    public Elevator elevator;
    public player_controller controller;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            elevator.startmoving= true;
            controller.talking();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            elevator.startmoving = true;
            controller.Stoptalking();
        }
    }

}
