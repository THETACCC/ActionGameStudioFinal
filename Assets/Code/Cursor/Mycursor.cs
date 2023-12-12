using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class Mycursor : MonoBehaviour
{
    [Header("Transform Ref:")]
    public Transform gunHolder;
    public Transform gunPivot;
    public Transform firePoint;

    // Animations
    public Animator animator;

    private Camera mainCamera;
    public bool cangrapple;

    private const float MaxGrappleDistance = 270f; // Max distance for grappling
    private LayerMask layerMask; // LayerMask for the raycast
    private float followSpeed = 5f;
    void Start()
    {
        Cursor.visible = false;
        mainCamera = Camera.main; // Cache the main camera
        layerMask = 1 << 6; // Set the layer mask to layer 6
    }

    void Update()
    {
        
        //Vector3 mousePosition = Input.mousePosition; // Get the mouse position in screen space
        //Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition); // Convert to world space

        // For a 2D game, we ignore the z-axis of the worldPosition

    }

    void LateUpdate()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = mainCamera.nearClipPlane;
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);

        transform.position = Vector3.Lerp(transform.position, new Vector3(worldPosition.x, worldPosition.y, transform.position.z), followSpeed * Time.deltaTime);

        transform.position = new Vector3(worldPosition.x, worldPosition.y, transform.position.z);


        Vector2 direction = (Vector2)(worldPosition - firePoint.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, direction, MaxGrappleDistance, layerMask);

        if (hit.collider != null)
        {
            float distance = Vector2.Distance(hit.point, firePoint.position);
            //Debug.Log($"Hit: {hit.collider.name}, Distance: {distance}");
            cangrapple = distance <= MaxGrappleDistance;
        }
        else
        {
            //Debug.Log("No hit");
            cangrapple = false;
        }

        animator.SetBool("Grapple", cangrapple);

    }

}
