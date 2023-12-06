using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIndicate : MonoBehaviour
{

    public float radius = 5f; // Radius of the circle
    public Transform centerPoint; // The center point of the circle
    public Vector3 direction;
    public GameObject Boss;
    void Update()
    {

        if(Boss != null)
        {
            Vector3 Bosspos = Boss.transform.position;
            direction = Bosspos - centerPoint.position;
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

}
