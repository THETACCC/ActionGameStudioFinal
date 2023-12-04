using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shotgunammo : MonoBehaviour
{
    public Text Cooldowntext;
    public Image CooldownBar;
    public shot_gun shotgun;
    float cooldown, ready = 5;
    float lerpSpeed;

    private void Start()
    {
        cooldown = 5;
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

