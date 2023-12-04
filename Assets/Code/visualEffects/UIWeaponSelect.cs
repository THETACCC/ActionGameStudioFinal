using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWeaponSelect : MonoBehaviour
{
    //get the RectTransform and the TMPRO

    private RectTransform m_transform;

    //get the points on weapon manager
    public GameObject weaponmanager;
    public Weaponmanage manage;
    private SpriteRenderer dummy_renderer;

    //effects
    private bool starteffect;
    private float speed = 20f;

    //get the weapon
    public int theweapon;
    public switch_weapon weaponswitch;

    //timer
    private float time = 0f;
    private float the_time = 0.1f;
    private bool countdown = false;

    private void Awake()
    {
        //manage = GetComponent<Weaponmanage>();
        m_transform = gameObject.GetComponent<RectTransform>();


    }
    private void Update()
    {
        if (weaponswitch == null || manage == null)
        {
            return;
        }



        if ((weaponswitch.selectedWeapon == theweapon))
        {
            Vector3 newScale = m_transform.localScale;
            newScale.x = Mathf.Lerp(newScale.x, 0.62f, Time.deltaTime * speed);
            newScale.y = Mathf.Lerp(newScale.y, 0.62f, Time.deltaTime * speed);
            m_transform.localScale = newScale;
        }
        else
        {
            Vector3 newScale = m_transform.localScale;
            newScale.x = Mathf.Lerp(newScale.x, 0.53f, Time.deltaTime * speed);
            newScale.y = Mathf.Lerp(newScale.y, 0.53f, Time.deltaTime * speed);
            m_transform.localScale = newScale;
        }




    }

}
