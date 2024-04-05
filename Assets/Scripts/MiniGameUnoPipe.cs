using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MiniGameUnoPipe : MonoBehaviour
{
    // public float[] correcRotation;

    public bool hasWater = false;
    public bool isStartPipe;
    public bool isStraightPipe;
    public GameObject StraightWaterPrefab;
    public GameObject StraightPipePrefab;
    public GameObject KneePipeWaterPrefab;
    public GameObject KneePipeFrepfab;

    float[] rotations = { 0, 90, 180, 270 };
    public bool needsUpdate = false;

    // [SerializeField]
    // bool isPlaced = false;

    private void Start()
    {
        // if (!isStartPipe)
        // {
        //     int randIdx = Random.Range(0, rotations.Length);
        //     transform.eulerAngles = new Vector3(0, 0, rotations[randIdx]);

        // for (int i = 0; i < correcRotation.Length; i++)
        // {
        //     if (transform.eulerAngles.z == correcRotation[i])
        //     {
        //         isPlaced = true;
        //     }
        // }
        // }
    }

    private void OnMouseDown()
    {
        transform.Rotate(new Vector3(0, 0, 90));
        // for (int i = 0; i < correcRotation.Length; i++)
        // {
        //     if (transform.eulerAngles.z == correcRotation[i] && isPlaced == false)
        //     {
        //         isPlaced = true;
        //         break;
        //     }
        //     else if (isPlaced)
        //     {
        //         isPlaced = false;
        //     }
        // }
    }

    private void Update()
    {
        if (isStartPipe)
        {
            return;
        }

        if (needsUpdate)
        {
            GameObject prefab = hasWater ? (isStraightPipe ? StraightWaterPrefab : KneePipeWaterPrefab) :
                                                         (isStraightPipe ? StraightPipePrefab : KneePipeFrepfab);
            Instantiate(prefab, transform.position, transform.rotation, transform.parent);
            Destroy(gameObject);
            needsUpdate = false;
        }
    }
}
