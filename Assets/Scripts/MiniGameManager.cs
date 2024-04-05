using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    public GameObject pipeHolder;
    public GameObject[] pipes;

    int totalPipes = 0;
    // Start is called before the first frame update
    void Start()
    {
        totalPipes = pipeHolder.transform.childCount;

        pipes = new GameObject[totalPipes];

        for (int i = 0; i < pipes.Length; i++)
        {
            pipes[i] = pipeHolder.transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
