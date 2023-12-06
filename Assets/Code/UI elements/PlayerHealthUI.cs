using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    public Text Cooldowntext;
    public Image CooldownBar;
    public Image[] healthPoints;
    public player_controller controller;
    public float health, maxhealth = 100;
    float lerpSpeed;
    private bool restarted = false;
    private void Start()
    {
        restarted = false;
    }

    private void Update()
    {
        lerpSpeed = 100f;

        if (health > maxhealth)
        {
            health = maxhealth;
        }
        CooldownBarFiller();
        //ColorChanger();
        if(health <= 0)
        {
            if(!restarted)
            {
                SceneController.instance.RestartLevel();
                restarted = true;
            }

        }


    }

    bool DisplayHealthPoint ( float _health, int pointNumber)
    {
        return((pointNumber * 10) >= _health);
    }
    void ColorChanger()
    {
        //Color CooldownColor = Color.Lerp(Color.red, Color.cyan, (health / maxhealth));

        //CooldownBar.color = CooldownColor;

    }


    void CooldownBarFiller()
    {
        CooldownBar.fillAmount = Mathf.Lerp(CooldownBar.fillAmount, (health / maxhealth), lerpSpeed);

        for (int i = 0; i < healthPoints.Length; i++)
        {
            healthPoints[i].enabled = !DisplayHealthPoint(health, i);
        }



        //CooldownBar.fillAmount = (cooldown / ready) * 0.25f;
    }
}
