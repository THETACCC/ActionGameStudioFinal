using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TNTAmmo : MonoBehaviour
{
    public Text Cooldowntext;
    public Image CooldownBar;
    public TNT_launcher shotgun;
    float cooldown, ready = 6;
    float lerpSpeed;

    private void Start()
    {
        cooldown = 6;
    }

    private void Update()
    {
        lerpSpeed = 3f * Time.deltaTime;
        cooldown = shotgun.ammo;
        if (cooldown > ready)
        {
            cooldown = ready;
        }
        CooldownBarFiller();

    }





    void CooldownBarFiller()
    {
        CooldownBar.fillAmount = Mathf.Lerp(CooldownBar.fillAmount, (cooldown / ready) * 0.25f, lerpSpeed);
    }
}

