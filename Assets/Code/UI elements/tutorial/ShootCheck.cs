using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCheck : MonoBehaviour
{
    public bool Spacepressed = false;

    public SpriteChangeShoot change1;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Spacepressed)
        {
            change1.fadetrigger = true;

        }
    }
}