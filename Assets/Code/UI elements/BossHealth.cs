using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public Text Cooldowntext;
    public Image CooldownBar;
    public EnemyShoot Boss;
    public float health, maxhealth = 10000;
    float lerpSpeed;

    private void Start()
    {
        
    }

    private void Update()
    {
        health = Boss.Health;
        lerpSpeed = 0.1f;

        if (health > maxhealth)
        {
            health = maxhealth;
        }
        CooldownBarFiller();
        ColorChanger();
    }

    bool DisplayHealthPoint(float _health, int pointNumber)
    {
        return ((pointNumber * 10) >= _health);
    }
    void ColorChanger()
    {
        Color CooldownColor = Color.Lerp(Color.red, Color.green, (health / maxhealth));

        CooldownBar.color = CooldownColor;

    }


    void CooldownBarFiller()
    {
        CooldownBar.fillAmount = Mathf.Lerp(CooldownBar.fillAmount, (health / maxhealth), lerpSpeed);





        //CooldownBar.fillAmount = (cooldown / ready) * 0.25f;
    }
}

