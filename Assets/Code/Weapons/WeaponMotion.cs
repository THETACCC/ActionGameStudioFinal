using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMotion : MonoBehaviour
{

    public float radius = 5f; // Radius of the circle
    public Transform centerPoint; // The center point of the circle
    public Vector3 direction;
    void Update()
    {
        // Get the mouse position in world coordinates
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Assuming you're working in 2D

        // Calculate the angle towards the mouse position
        direction = mousePosition - centerPoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x);

        // Calculate the new position
        float x = centerPoint.position.x + radius * Mathf.Cos(angle);
        float y = centerPoint.position.y + radius * Mathf.Sin(angle);
        Vector3 newPosition = new Vector3(x, y, 0);

        // Update the object's position
        transform.position = newPosition;

        // Align the object to face the mouse position
        transform.up = direction.normalized;
    }

}
