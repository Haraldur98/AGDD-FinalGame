using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform playerTransform;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    void FixedUpdate()
    {
        Vector3 desiredPosition = playerTransform.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
