using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteChangeShoot : MonoBehaviour
{
    public Image targetImage; // Assign this in the inspector
    public Sprite newSprite;  // Assign the new sprite in the inspector
    public int MousePress; // Assign the key in the inspector
    public float fadeDuration = 1.0f; // Duration for the fade effect

    private Sprite originalSprite;
    private bool isFading = false;
    private bool startfading = false;
    public bool fadetrigger = false;

    public ShootCheck check;


    void Start()
    {
        if (targetImage != null)
        {
            originalSprite = targetImage.sprite;
            targetImage.color = new Color(targetImage.color.r, targetImage.color.g, targetImage.color.b, 0);
            StartCoroutine(FadeIn());
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(MousePress))
        {
            if (targetImage != null && newSprite != null)
            {
                targetImage.sprite = newSprite;
                if (MousePress == 0) // Left mouse button
                {
                    check.Spacepressed = true;
                }
            }
        }
        else if (Input.GetMouseButtonUp(MousePress))
        {
            if (targetImage != null)
            {
                targetImage.sprite = originalSprite;
            }
        }

        if (fadetrigger && !startfading)
        {
            StartCoroutine(FadeOut());
            startfading = true;
        }


    }

    IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Color transparentColor = new Color(targetImage.color.r, targetImage.color.g, targetImage.color.b, 0);
        targetImage.color = transparentColor;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            targetImage.color = new Color(transparentColor.r, transparentColor.g, transparentColor.b, alpha);
            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        isFading = true;
        float elapsedTime = 0f;
        Color originalColor = targetImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(1.0f - (elapsedTime / fadeDuration));
            targetImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        targetImage.gameObject.SetActive(false);
        targetImage.color = originalColor; // Reset color for next activation
        isFading = false;
    }


}
