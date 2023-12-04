using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RocketAmmo : MonoBehaviour
{
    public Text Cooldowntext;
    public Image CooldownBar;
    public rocket_launcher shotgun;
    float cooldown, ready = 3;
    float lerpSpeed;

    private void Start()
    {
        cooldown = 3;
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
