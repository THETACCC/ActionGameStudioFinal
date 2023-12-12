using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashCoolDown : MonoBehaviour
{
    public Text Cooldowntext;
    public Image CooldownBar;
    public player_controller controller;
    float cooldown, ready = 1;
    float lerpSpeed;

    private void Start()
    {
        cooldown = controller.dashingCooldownRef;
    }

    private void Update()
    {
        lerpSpeed =  100f;
        cooldown = controller.dashingCooldownRef;
        if (cooldown > ready)
        {
            cooldown = ready;
        }
        CooldownBarFiller();
        ColorChanger();
    }


    void ColorChanger()
    {
        Color CooldownColor = Color.Lerp(Color.cyan, Color.cyan, (cooldown / ready));

        CooldownBar.color = CooldownColor;

    }


    void CooldownBarFiller()
    {
        CooldownBar.fillAmount = Mathf.Lerp(CooldownBar.fillAmount, (cooldown / ready) * 0.20f, lerpSpeed);
        //CooldownBar.fillAmount = (cooldown / ready) * 0.25f;
    }
}

