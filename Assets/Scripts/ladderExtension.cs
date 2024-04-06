using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ladderExtension : MonoBehaviour
{
    public GameObject ladder; // The ladder object
    public float scaleSpeed = 0.5f; // The speed of scaling

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll > 0f)
        {
            // Scale the ladder up
            Vector3 scale = ladder.transform.localScale;
            scale.y += scaleSpeed;
            ladder.transform.localScale = scale;

            // Move the ladder up by half of the increase in heaaight
            Vector3 position = ladder.transform.position;
            position.y += scaleSpeed / 2;
            ladder.transform.position = position;
        }
        else if (scroll < 0f)
        {
            // Scale the ladder down
            Vector3 scale = ladder.transform.localScale;
            scale.y -= scaleSpeed;
            ladder.transform.localScale = scale;

            // Move the ladder down by half of the decrease in height
            Vector3 position = ladder.transform.position;
            position.y -= scaleSpeed / 2;
            ladder.transform.position = position;
        }
    }
}