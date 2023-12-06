using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialmovementcheck : MonoBehaviour
{

    public bool Apressed = false;
    public bool Dpressed = false;

    public SpriteChange change1;
    public SpriteChange change2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Apressed || Dpressed)
        {
            change1.fadetrigger = true;
            change2.fadetrigger = true;
        }
    }
}
