using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    public GameObject pipeHolder;
    public GameObject[] pipes;
    public GameObject endPipeGameobject;
    private MiniGameUnoPipe endPipe;

    int totalPipes = 0;
    float[] rotations = { 0, 90, 180, 270 };
    // Start is called before the first frame update
    void Start()
    {
        totalPipes = pipeHolder.transform.childCount;

        endPipe = endPipeGameobject.GetComponent<MiniGameUnoPipe>();

        pipes = new GameObject[totalPipes];

        for (int i = 0; i < pipes.Length; i++)
        {
            MiniGameUnoPipe pipe = pipeHolder.transform.GetChild(i).GetComponent<MiniGameUnoPipe>();
            int randIdx = Random.Range(0, rotations.Length);
            pipe.transform.eulerAngles = new Vector3(0, 0, rotations[randIdx]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (endPipe.hasWater) 
        {
            Debug.Log("BINGO");
        }

    }
}
