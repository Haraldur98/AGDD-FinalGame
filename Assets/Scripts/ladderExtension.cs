using System.Collections;
using UnityEngine;

public class ladderExtension : MonoBehaviour
{
    public GameObject ladder; // The ladder object
    public float scaleSpeed = 2f; // The speed of scaling
    public float minScale = 1f; // The minimum scale of the ladder
    public float maxScale = 10f; // The maximum scale of the ladder

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll > 0f)
        {
            // Scale the ladder up
            Vector3 scale = ladder.transform.localScale;
            if (scale.y + scaleSpeed <= maxScale)
            {
                StartCoroutine(ScaleLadder(scaleSpeed));
            }
        }
        else if (scroll < 0f)
        {
            // Scale the ladder down
            Vector3 scale = ladder.transform.localScale;
            if (scale.y - scaleSpeed >= minScale)
            {
                StartCoroutine(ScaleLadder(-scaleSpeed));
            }
        }
    }

    IEnumerator ScaleLadder(float amount)
    {
        Vector3 startScale = ladder.transform.localScale;
        Vector3 endScale = new Vector3(startScale.x, startScale.y + amount, startScale.z);

        Vector3 startPosition = ladder.transform.position;
        Vector3 endPosition = new Vector3(startPosition.x, startPosition.y + amount / 2, startPosition.z);

        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * scaleSpeed;
            ladder.transform.localScale = Vector3.Lerp(startScale, endScale, t);
            ladder.transform.position = Vector3.Lerp(startPosition, endPosition, t);
            yield return null;
        }
    }
}