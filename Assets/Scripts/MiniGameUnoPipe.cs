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

    private void OnMouseDown()
    {
        transform.Rotate(new Vector3(0, 0, 90));
    }

    private void Update()
    {
        if (needsUpdate)
        {
            ChangePipe();
        }
    }

    private void ChangePipe()
    {
        if (isStartPipe) return;

        GameObject prefab = hasWater ? (isStraightPipe ? StraightWaterPrefab : KneePipeWaterPrefab) :
                                                         (isStraightPipe ? StraightPipePrefab : KneePipeFrepfab);
        MiniGameUnoPipe newPipe = Instantiate(prefab, transform.position, transform.rotation, transform.parent).GetComponent<MiniGameUnoPipe>(); ;

        if (isEndPipe)
            newPipe.isEndPipe = true;
        Destroy(gameObject);
    }
}
