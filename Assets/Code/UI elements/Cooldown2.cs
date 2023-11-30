using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cooldown2 : MonoBehaviour
{
    public Text Cooldowntext;
    public Image CooldownBar;
    public GrapplingGun grapplingGun;
    float cooldown, ready = 2;
    float lerpSpeed;

    private void Start()
    {
        cooldown = grapplingGun.cooldowntimer;
    }

    private void Update()
    {
        lerpSpeed = 3f * Time.deltaTime;
        cooldown = grapplingGun.cooldowntimer;
        if (cooldown > ready)
        {
            cooldown = ready;
        }
        CooldownBarFiller();
        ColorChanger();
    }


    void ColorChanger()
    {
        Color CooldownColor = Color.Lerp(Color.red, Color.yellow, (cooldown / ready));

        CooldownBar.color = CooldownColor;

    }


    void CooldownBarFiller()
    {
        CooldownBar.fillAmount = Mathf.Lerp(CooldownBar.fillAmount, (cooldown / ready )* 0.20f, lerpSpeed);
    }
}
