using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeObject : MonoBehaviour
{
    public float fadeDuration = 1.0f;
    public float visibleDurtaion = 1.0f;
    public GameObject warningSign;
    private bool fadeIn = false;
    private float fadeTimer = 0.0f;
    private float visibleTimer = 0.0f;

    // Update is called once per frame
    void Update()
    {
        visibleTimer += Time.deltaTime;
        
        if (visibleTimer >= visibleDurtaion) {

            fadeTimer += Time.deltaTime;
            if (fadeTimer >= fadeDuration)
            {
                if (fadeIn) visibleTimer = 0.0f;
                fadeIn = !fadeIn; // Toggle fade direction
                fadeTimer = 0.0f; // Reset the timer
            }

            float alpha = fadeIn ? Mathf.Lerp(0f, 1f, fadeTimer / fadeDuration) : Mathf.Lerp(1f, 0f, fadeTimer / fadeDuration);
            SetAlpha(alpha);
        }
    }

    private void SetAlpha(float alpha)
    {
        SpriteRenderer[] renderers = warningSign.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer renderer in renderers)
        {
            Color color = renderer.color;
            color.a = alpha;
            renderer.color = color;
        }
    }
}
