using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public float targetHeight = 5.0f; // Height to which the elevator will move
    public float speed = 2.0f; // Speed of the elevator movement
    private bool isMoving = false;
    private Vector3 startPosition;
    private Vector3 endPosition;
    public bool startmoving = false;
    void Start()
    {
        startPosition = transform.position;
        endPosition = new Vector3(transform.position.x, transform.position.y + targetHeight, transform.position.z);
    }

    void Update()
    {
        // Trigger to start the elevator, replace this with your own condition
        if (startmoving)
        {
            StartCoroutine(MoveElevator());
        }
    }

    IEnumerator MoveElevator()
    {
        isMoving = true;
        float journey = 0f;

        while (journey <= 1f)
        {
            journey += Time.deltaTime * speed / targetHeight;
            transform.position = Vector3.Lerp(startPosition, endPosition, journey);
            yield return null;
        }
        startmoving = false;
        isMoving = false;
    }
}
