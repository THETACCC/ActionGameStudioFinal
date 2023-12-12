using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossKilled : MonoBehaviour
{
    public void gameend()
    {

        Invoke("gotoend", 60f);

    }

    public void gotoend()
    {
        SceneController.instance.NextLevel();
    }
}
