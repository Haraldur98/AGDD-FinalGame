using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    public GameObject arrowPrefab;
    private GameObject[] warningSigns;
    private List<GameObject> arrowGameObjects;
    private List<ArrowController> arrows;
    private GameManager gameManager;
    private bool arrowsAreActive = false;

    private
    void Start()
    {
        warningSigns = GameObject.FindGameObjectsWithTag("WarningSign");
        arrows = new List<ArrowController>();
        arrowGameObjects = new List<GameObject>();
        gameManager = FindObjectOfType<GameManager>();
        SpawnArrows();
    }


    void Update()
    {
        if (gameManager.isInMiniGAme)
        {
            DisableArrows();
            arrowsAreActive = false;
            return;
        }
        else if (!gameManager.isInMiniGAme && !arrowsAreActive)
        {
            SpawnArrows();
            arrowsAreActive = true;
        }

        if (warningSigns == null || arrows == null)
            return;

        if (warningSigns.Length == 0 || arrows.Count == 0)
            return;

        for (int i = 0; i < arrows.Count; i++)
        {
            if (!arrows[i].CheckIfOffCamera())
                arrows[i].SetActive(false);
            else
                arrows[i].SetActive(true);
        }
    }

    void SpawnArrows()
    {
        Debug.Log(warningSigns.Length);
        for (int i = 0; i < warningSigns.Length; i++)
        {
            if (warningSigns[i] == null) continue;
            GameObject arrowGameObject = Instantiate(arrowPrefab);
            arrowGameObjects.Add(arrowGameObject);
            ArrowController arrow = arrowGameObject.GetComponent<ArrowController>();
            arrow.SetArrowObject(arrowGameObject);
            arrow.SetWarningSign(warningSigns[i]);
            arrows.Add(arrow);
        }
    }


    void DisableArrows()
    {
        for (int i = 0; i < arrows.Count; i++)
        {
            Destroy(arrowGameObjects[i]);
        }
    }

    public void RemoveArrow(int index)
    {
        if (index < 0 || index >= arrowGameObjects.Count)
        {
            Debug.LogError("Index out of range");
            return;
        }

        // Destroy the object at the specified index
        Destroy(arrowGameObjects[index]);

        // Remove the element at the specified index from the lists
        arrowGameObjects.RemoveAt(index);
        arrows.RemoveAt(index);
    }
}
