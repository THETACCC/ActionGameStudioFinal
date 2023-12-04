using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class handgunammo : MonoBehaviour
{
    public Text Cooldowntext;
    public Image CooldownBar;
    public hand_gun handgun;
    float cooldown, ready = 60;
    float lerpSpeed;

    private void Start()
    {
        cooldown = handgun.ammo;
    }

    private void Update()
    {
        lerpSpeed = 3f * Time.deltaTime;
        cooldown = handgun.ammo;
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

