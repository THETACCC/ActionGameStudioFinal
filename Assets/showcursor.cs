using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showcursor : MonoBehaviour
{

    void Start()
    {
        // Set cursor visible
        Cursor.visible = true;

        // Optionally, unlock the cursor if it was previously locked
        Cursor.lockState = CursorLockMode.None;
    }
}

