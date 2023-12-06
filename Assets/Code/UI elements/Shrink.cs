using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shrink : MonoBehaviour
{
    public Image imageToBlink; // Assign this in the inspector
    public float blinkDuration = 1.0f; // Duration of one full blink cycle (fade out and fade in)
    public float timeBetweenBlinks = 2.0f; // Time between blink cycles

    private void Start()
    {
        if (imageToBlink != null)
        {
            StartCoroutine(BlinkImage());
        }
    }

    private IEnumerator BlinkImage()
    {
        while (true)
        {
            // Fade out
            yield return StartCoroutine(FadeImage(0f, blinkDuration / 2));
            // Wait
            yield return new WaitForSeconds(timeBetweenBlinks);
            // Fade in
            yield return StartCoroutine(FadeImage(1f, blinkDuration / 2));
            // Wait
            yield return new WaitForSeconds(timeBetweenBlinks);
        }
    }

    private IEnumerator FadeImage(float targetAlpha, float duration)
    {
        float startAlpha = imageToBlink.color.a;
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            imageToBlink.color = new Color(imageToBlink.color.r, imageToBlink.color.g, imageToBlink.color.b, alpha);
            yield return null;
        }

        imageToBlink.color = new Color(imageToBlink.color.r, imageToBlink.color.g, imageToBlink.color.b, targetAlpha);
    }
}
