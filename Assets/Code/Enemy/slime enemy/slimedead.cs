using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimedead : MonoBehaviour
{
    public slimeenemymovc slime;


    public void DestroyGameObject()
    {
        slime.DestroyGameObject();
    }
}
