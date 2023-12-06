using SKCell;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SKtest : MonoBehaviour
{
    [SerializeField] SKDialoguePlayer player;

    // Start is called before the first frame update
    void Start()
    {
        player.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
