using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOverTime : MonoBehaviour
{
    public float rotationSpeed = 50f; // Rotation speed in degrees per second

    void Update()
    {
        // Rotate around the Z-axis at 'rotationSpeed' degrees per second
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
