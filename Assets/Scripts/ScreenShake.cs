using System;
using System.Collections;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public bool isShaking = false;
    public float duration = 0.2f;
    public AnimationCurve shakeCurve;

    void Update()
    {
        if (isShaking)
        {
            isShaking = false;
            StartCoroutine(Shake());
        }
    }
    public IEnumerator Shake()
    {
        Debug.Log("Shake");
        Vector3 originalPosition = transform.position;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float strength = shakeCurve.Evaluate(elapsed / duration);
            transform.position = originalPosition + UnityEngine.Random.insideUnitSphere * strength;
            yield return null;
        }

        transform.position = originalPosition;
    }
}
