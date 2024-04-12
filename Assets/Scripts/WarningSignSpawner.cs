using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class WarningSignSpawner : MonoBehaviour
{
    public GameObject warningSignPrefab;
    private GameObject[] warningSigns = new GameObject[7];
    private readonly List<Vector3> posForMiniGameOne = new();
    private readonly List<Vector3> posForMiniGameTwo = new();
    // add if beil on beiling on minigame 3
    private List<Vector2> posForMiniGameThree = new();
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        // To add a new warningSign place a location for the minigame of choice
        posForMiniGameOne.Add(new Vector3(24.87f, -12.2f));
        posForMiniGameOne.Add(new Vector3(-27.08905f, -19.72915f));
        posForMiniGameOne.Add(new Vector3(31.47059f, -18.41f));
        posForMiniGameOne.Add(new Vector3(-16.37041f, -27.67455f));

        posForMiniGameTwo.Add(new Vector3(-10.34f, -11.09f));
        posForMiniGameTwo.Add(new Vector3(47f, -38.3f));
        posForMiniGameTwo.Add(new Vector3(-36.72654f, -44.2f));

        for (int i = 0; i < posForMiniGameOne.Count; i++)
        {
            GameObject signObject = Instantiate(warningSignPrefab, posForMiniGameOne[i], quaternion.identity);
            WarningSign warningSign = signObject.GetComponent<WarningSign>();
            warningSign.SetMinigame(1);
            warningSign.SetLevel(i);

        }

        for (int i = 0; i < posForMiniGameTwo.Count; i++)
        {
            GameObject signObject = Instantiate(warningSignPrefab, posForMiniGameTwo[i], quaternion.identity);
            WarningSign warningSign = signObject.GetComponent<WarningSign>();
            warningSign.SetMinigame(2);
            warningSign.SetLevel(i);
            // add signObject in warningSigns
            warningSigns[i] = signObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
    
    }
}
