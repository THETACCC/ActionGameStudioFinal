using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float defDistanceRay = 100;
    public Transform laserFirePoint;
    public LineRenderer m_lineRenderer;
    Transform m_transform;


    private GameObject player;
    private Rigidbody2D rb;
    private player_controller playerControll;

    public PlayerHealthUI playerHealthUI;
    public GameObject PlayerUI;


    private void Start()
    {
        if (m_lineRenderer == null)
        {
            m_lineRenderer = GetComponent<LineRenderer>();
        }
        PlayerUI = GameObject.FindGameObjectWithTag("UI");
        if (PlayerUI != null)
        {
            playerHealthUI = PlayerUI.GetComponent<PlayerHealthUI>();
        }
        rb = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerControll = player.GetComponent<player_controller>();
        m_transform = GetComponent<Transform>();
    }
    private void Update()
    {
         ShootLaser();
    }

    void ShootLaser()
    {
        Vector2 direction = laserFirePoint.right;
        RaycastHit2D hit = Physics2D.Raycast(laserFirePoint.position, direction);

        if (hit.collider != null)
        {
            // If the ray hits a collider, draw the laser up to the hit point
            Draw2DRay(laserFirePoint.position, hit.point);

            if (hit.collider.CompareTag("Player"))
            {
                // Code to execute when the laser hits the player
                if (playerControll.isinvisible == false)
                {
                    playerHealthUI.health -= 10;
                    bool isCriticalHit = true;
                    DamagePopup.Create(gameObject.transform.position, 10, isCriticalHit);

                    playerControll.isinvisible = true;
                }

            }
        }
        else
        {
            // If the ray doesn't hit anything, draw the laser to its default length
            Draw2DRay(laserFirePoint.position, (Vector2)laserFirePoint.position + direction * defDistanceRay);
        }



    }
    void Draw2DRay(Vector2 startPos, Vector2 endPos)
    {
        m_lineRenderer.SetPosition(0,startPos);
        m_lineRenderer.SetPosition(1,endPos);
    }

}
