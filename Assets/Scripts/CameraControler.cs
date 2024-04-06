using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform playerTransform;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    void FixedUpdate()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 playerPosition = playerTransform.position;

        // Calculate a position that is in between the player and the mouse
        Vector3 desiredPosition = (playerPosition + mousePosition) / 2 + offset;

        // Calculate the distance between the desired position and the player
        float distance = Vector3.Distance(desiredPosition, playerPosition);

        // Clamp the distance to ensure that the camera doesn't move too far away from the player
        float maxDistance = 10f; // Set this to the maximum distance you want
        if (distance > maxDistance)
        {
            Vector3 direction = (desiredPosition - playerPosition).normalized;
            desiredPosition = playerPosition + direction * maxDistance;
        }

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
    }
}
