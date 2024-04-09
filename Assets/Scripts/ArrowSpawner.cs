using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    public GameObject arrowPrefab;
    private GameObject[] warningSigns;
    private ArrowController[] arrows;
    private
    void Start()
    {
        warningSigns = GameObject.FindGameObjectsWithTag("WarningSign");
        arrows = new ArrowController[warningSigns.Length];
        SpawnArrows();
    }


    void Update()
    {
        if (warningSigns == null || arrows == null)
            return;

        if (warningSigns.Length == 0 || arrows.Length == 0)
            return;

        for (int i = 0; i < warningSigns.Length; i++)
        {
            if (!arrows[i].CheckIfOffCamera())
                arrows[i].SetActive(false);
            else
                arrows[i].SetActive(true);
        }
    }

    void SpawnArrows()
    {
        for (int i = 0; i < warningSigns.Length; i++)
        {
            GameObject arrowGameObject = Instantiate(arrowPrefab);
            ArrowController arrow = arrowGameObject.GetComponent<ArrowController>();
            arrow.SetArrowObject(arrowGameObject);
            arrow.SetWarningSign(warningSigns[i]);
            arrows[i] = arrow;
        }
    }
}
