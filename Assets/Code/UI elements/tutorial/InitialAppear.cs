using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitialAppear : MonoBehaviour
{


    public Image targetImage;
    public float fadeDuration = 1.0f; // Duration for the fade effect

    // Start is called before the first frame update
    void Start()
    {
        if (targetImage != null)
        {
            targetImage.color = new Color(targetImage.color.r, targetImage.color.g, targetImage.color.b, 0);
            StartCoroutine(FadeIn());
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

}
