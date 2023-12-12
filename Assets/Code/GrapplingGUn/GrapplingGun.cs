using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    [Header("Scripts Ref:")]
    public GrapplingRope grappleRope;
    public player_controller controller;
    [Header("Layers Settings:")]
    [SerializeField] private bool grappleToAll = false;
    [SerializeField] private int grappableLayerNumber = 6;

    [Header("Main Camera:")]
    public Camera m_camera;

    [Header("Transform Ref:")]
    public Transform gunHolder;
    public Transform gunPivot;
    public Transform firePoint;
    public Transform raycastPoint;
    [Header("Physics Ref:")]
    public SpringJoint2D m_springJoint2D;
    public Rigidbody2D m_rigidbody;

    [Header("Rotation:")]
    [SerializeField] private bool rotateOverTime = true;
    [Range(0, 60)][SerializeField] private float rotationSpeed = 4;

    [Header("Distance:")]
    [SerializeField] private bool hasMaxDistance = false;
    [SerializeField] private float maxDistnace = 20;

    private enum LaunchType
    {
        Transform_Launch,
        Physics_Launch
    }

    [Header("Launching:")]
    [SerializeField] private bool launchToPoint = true;
    [SerializeField] private LaunchType launchType = LaunchType.Physics_Launch;
    [SerializeField] private float launchSpeed = 1;

    [Header("No Launch To Point")]
    [SerializeField] private bool autoConfigureDistance = false;
    [SerializeField] private float targetDistance = 3;
    [SerializeField] private float targetFrequncy = 1;

    [HideInInspector] public Vector2 grapplePoint;
    [HideInInspector] public Vector2 grappleDistanceVector;


    //Modifications
    public bool Grapenabled = false;
    public bool isgrappling = false;
    private Vector2 storedVelocity;
    public float angle;

    //Cooldowns
    public float cooldowntimer = 2f;
    public bool startcounting = false;

    //ref
    public Mycursor cursor;


    //sound ref
    public AudioSource audioSource;
    public AudioClip soundClip;
    public AudioClip ReadySound;
    private bool playreadysound = true;
    private void Start()
    {
        cooldowntimer = 3f;
        grappleRope.enabled = false;
        m_springJoint2D.enabled = false;

    }

    private void Update()
    {
        if (!startcounting)
        {
            cooldowntimer += Time.deltaTime;
        }
        if (cooldowntimer >= 3f)
        {
            if (playreadysound == false) 
            {
                audioSource.PlayOneShot(ReadySound, 0.2f);
                playreadysound = true;
            }

        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && cooldowntimer >= 3f)
        {
            SetGrapplePoint();
            audioSource.PlayOneShot(soundClip);
            Grapenabled = true;
            storedVelocity = m_rigidbody.velocity;
        }
        else if (Input.GetKey(KeyCode.Mouse1) && Grapenabled == true)
        {

            if (grappleRope.enabled)
            {
                RotateGun(grapplePoint, false);
            }
            else
            {
                Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
                RotateGun(mousePos, true);
            }

            if (launchToPoint && grappleRope.isGrappling)
            {
                if (launchType == LaunchType.Transform_Launch)
                {
                    Vector2 firePointDistnace = firePoint.position - gunHolder.localPosition;
                    Vector2 targetPos = grapplePoint - firePointDistnace;
                    gunHolder.position = Vector2.Lerp(gunHolder.position, targetPos, Time.deltaTime * launchSpeed / 2);

                    if ((angle < 30) && (angle > 0))
                    {
                        m_rigidbody.velocity = new Vector2(Mathf.Abs(storedVelocity.x) * 1.4f + 40f, 5f);
                    }
                    else if ((angle < 60) && (angle > 29))
                    {
                        m_rigidbody.velocity = new Vector2(Mathf.Abs(storedVelocity.x) * 1.2f + 40f, 15f);
                    }
                    else if ((angle < 90) && (angle > 59))
                    {
                        m_rigidbody.velocity = new Vector2(Mathf.Abs(storedVelocity.x) * 1.1f + 40f, 30f);
                    }
                    else if ((angle < 120) && (angle > 89))
                    {
                        m_rigidbody.velocity = new Vector2(-Mathf.Abs(storedVelocity.x) * 1.1f - 40f, 30f);
                    }
                    else if ((angle < 150) && (angle > 119))
                    {
                        m_rigidbody.velocity = new Vector2(-Mathf.Abs(storedVelocity.x) * 1.2f - 40f, 15f);
                    }
                    else if ((angle < 180) && (angle > 149))
                    {
                        m_rigidbody.velocity = new Vector2(-Mathf.Abs(storedVelocity.x) * 1.4f - 40f, 5f);
                    }
                    else if ((angle < 0) && (angle > -30))
                    {
                        m_rigidbody.velocity = new Vector2(Mathf.Abs(storedVelocity.x) * 1.4f + 40f, 5f);
                    }
                    else if ((angle < -29) && (angle > -60))
                    {
                        m_rigidbody.velocity = new Vector2(Mathf.Abs(storedVelocity.x) * 1.5f + 40f, 5f);
                    }
                    else if ((angle < -59) && (angle > -90))
                    {
                        m_rigidbody.velocity = new Vector2(Mathf.Abs(storedVelocity.x) * 1.1f - 40f, 5f);
                    }
                    else if ((angle < -89) && (angle > -120))
                    {
                        m_rigidbody.velocity = new Vector2(-Mathf.Abs(storedVelocity.x) * 1.1f - 40f, 5f);
                    }
                    else if ((angle < -119) && (angle > -150))
                    {
                        m_rigidbody.velocity = new Vector2(-Mathf.Abs(storedVelocity.x) * 1.2f - 40f, 5f);
                    }
                    else if ((angle < -149) && (angle > -180))
                    {
                        m_rigidbody.velocity = new Vector2(-Mathf.Abs(storedVelocity.x) * 1.4f + 40f, 5f);
                    }
                    //m_rigidbody.velocity += storedVelocity;

                }
            }
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1) || Grapenabled == false)
        {
            startcounting = false;
            grappleRope.enabled = false;
            m_springJoint2D.enabled = false;
            m_rigidbody.gravityScale = 23f;
        }
        else
        {
            Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
            RotateGun(mousePos, true);
        }
    }

    void RotateGun(Vector3 lookPoint, bool allowRotationOverTime)
    {
        Vector3 distanceVector = lookPoint - gunPivot.position;




        angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;



        if (rotateOverTime && allowRotationOverTime)
        {
            gunPivot.rotation = Quaternion.Lerp(gunPivot.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * rotationSpeed);
        }
        else
        {
            gunPivot.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    void SetGrapplePoint()
    {

        Vector2 distanceVector = m_camera.ScreenToWorldPoint(Input.mousePosition) - gunPivot.position;
        Debug.DrawRay(raycastPoint.position, distanceVector.normalized * maxDistnace, Color.red, 2f);
        if (Physics2D.Raycast(raycastPoint.position, distanceVector.normalized))
        {
            // Create a layer mask that ignores the player's layer
            int layerMask = 7 << LayerMask.NameToLayer("Player");
            layerMask = ~layerMask; // Invert the mask to ignore the player's layer

            RaycastHit2D _hit = Physics2D.Raycast(raycastPoint.position, distanceVector.normalized);
            Debug.Log(_hit);
            if (_hit.transform.gameObject.layer == 6)
            {
                Debug.Log("OK");
                if (Vector2.Distance(_hit.point, raycastPoint.position) <= maxDistnace)
                {
                    grapplePoint = _hit.point;
                    grappleDistanceVector = grapplePoint - (Vector2)gunPivot.position;
                    grappleRope.enabled = true;
                    startcounting = true;
                    cooldowntimer = 0f;
                    playreadysound = false;
                    Debug.Log("FOUND");
                }
            }
        }
    }

    public void Grapple()
    {
        m_springJoint2D.autoConfigureDistance = false;
        if (!launchToPoint && !autoConfigureDistance)
        {
            m_springJoint2D.distance = targetDistance;
            m_springJoint2D.frequency = targetFrequncy;
        }
        if (!launchToPoint)
        {
            if (autoConfigureDistance)
            {
                m_springJoint2D.autoConfigureDistance = true;
                m_springJoint2D.frequency = 0;
            }

            m_springJoint2D.connectedAnchor = grapplePoint;
            m_springJoint2D.enabled = true;
        }
        else
        {
            switch (launchType)
            {
                case LaunchType.Physics_Launch:
                    m_springJoint2D.connectedAnchor = grapplePoint;

                    Vector2 distanceVector = firePoint.position - gunHolder.position;

                    //m_springJoint2D.distance = distanceVector.magnitude;
                    m_springJoint2D.distance = 0.005f;
                    m_springJoint2D.frequency = launchSpeed;
                    m_springJoint2D.enabled = true;
                    break;
                case LaunchType.Transform_Launch:
                    m_rigidbody.gravityScale = 0;
                    //m_rigidbody.velocity = Vector2.zero;
                    break;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (firePoint != null && hasMaxDistance)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(firePoint.position, maxDistnace);
        }
    }

}
