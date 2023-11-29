using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cooldown : MonoBehaviour
{
    public Text Cooldowntext;
    public Image CooldownBar;

    float cooldown, ready = 3;

    private void Start()
    {
        cooldown = ready;
    }

    private void Update()
    {
        if (cooldown > ready)
        {
            cooldown = ready;
        }
        CooldownBarFiller();
    }

    void CooldownBarFiller()
    {
        CooldownBar.fillAmount = cooldown / ready;
    }
}
