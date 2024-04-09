using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MiniGameUnoPipe : MonoBehaviour
{
    public bool hasWater = false;
    public bool isStartPipe = false;
    public bool isEndPipe = false;

    public bool needsUpdate = false;
    public GameObject filledPipe;

    public float rotationSpeed = 4.0f; // Speed of rotation

    private bool isRotating = false;
    public float targetAngle = 0;
    public MiniGameUnoPipe connectionOne;
    public MiniGameUnoPipe connectionTwo;

    private void OnMouseDown()
    {
        if (!isRotating)
        {
            targetAngle -= 90; 
            StartCoroutine(RotatePipe());
            
        }
    }

    private IEnumerator RotatePipe()
    {
        isRotating = true;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, 0, -90));
        float time = 0.0f;

        while (time < 1.0f)
        {
            time += Time.deltaTime * rotationSpeed;
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
        }

        isRotating = false;
        FindObjectOfType<MiniGameManager>().PropagateWater();
    }

    public void PropagateWater(bool status)
    {
        if (hasWater != status || isStartPipe)
        {
            hasWater = status;
            ChangePipe();
            // Propagate the water status to connected pipes
            connectionOne?.PropagateWater(status);
            connectionTwo?.PropagateWater(status);
        }
    }

    public void ChangePipe()
    {
        if (isStartPipe) return;
        if (hasWater) {
            filledPipe.SetActive(true);
        } else {
            filledPipe.SetActive(false);
        }
    }
}
