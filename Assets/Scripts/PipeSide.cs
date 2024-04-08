using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSide : MonoBehaviour
{
    private MiniGameUnoPipe pipe;

    private void Start()
    {
        pipe = transform.parent.GetComponent<MiniGameUnoPipe>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        MiniGameUnoPipe otherPipe = other.transform.parent.GetComponent<MiniGameUnoPipe>();

        if (otherPipe != null && otherPipe.hasWater)
        {
            if (pipe.hasWater) return;
            pipe.hasWater = true;
            pipe.needsUpdate = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        MiniGameUnoPipe otherPipe = other.transform.parent.GetComponent<MiniGameUnoPipe>();
        if (otherPipe != null && otherPipe.hasWater)
        {
            if (!pipe.hasWater) return;
            pipe.hasWater = false;
            pipe.needsUpdate = true;
        }
    }
}
