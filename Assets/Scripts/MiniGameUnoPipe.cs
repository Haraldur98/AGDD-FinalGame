using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MiniGameUnoPipe : MonoBehaviour
{
    public bool hasWater = false;
    public bool isStartPipe = false;
    public bool isEndPipe = false;
    public bool isStraightPipe = false;
    public GameObject StraightWaterPrefab;
    public GameObject StraightPipePrefab;
    public GameObject KneePipeWaterPrefab;
    public GameObject KneePipeFrepfab;
    public bool needsUpdate = false;

    public float rotationSpeed = 2.0f; // Speed of rotation

    private bool isRotating = false;
    public float targetAngle = 0;

    private void OnMouseDown()
    {
        if (!isRotating)
        {
            targetAngle -= 90; // Assuming you want to rotate 90 degrees each click
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
    }

    private void Update()
    {
        if (needsUpdate && !isRotating)
        {
            ChangePipe();
        }
    }

    private void ChangePipe()
    {
        if (isStartPipe) return;

        GameObject prefab = hasWater ? (isStraightPipe ? StraightWaterPrefab : KneePipeWaterPrefab) :
                                                         (isStraightPipe ? StraightPipePrefab : KneePipeFrepfab);
        MiniGameUnoPipe newPipe = Instantiate(prefab, transform.position, transform.rotation, transform.parent).GetComponent<MiniGameUnoPipe>(); 

        if (isEndPipe)
            newPipe.isEndPipe = true;
        Destroy(gameObject);
    }
}
