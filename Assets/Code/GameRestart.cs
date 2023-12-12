using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRestart : MonoBehaviour
{
    public void Restart()
    {
        // Reloads the current scene
        SceneManager.LoadScene(0);
    }
}
