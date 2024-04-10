using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningSign : MonoBehaviour
{
    private ArrowSpawner arrowSpawner;
    private int index;
    private GameManager gameManager;
    private bool isInRange;
    private int signMiniGame;
    private int signLevel;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        if (isInRange)
        {
            // TODO: get signLevel into scenes
            gameManager.LoadMiniGame(signMiniGame, signLevel);
            arrowSpawner.RemoveArrow(index);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        isInRange = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isInRange = false;
    }

    public void SetMinigame(int miniGame)
    {
        signMiniGame = miniGame;
    }

    public void SetLevel(int level)
    {
        signLevel = level;
    }

    public void SetArrowSpawner(ArrowSpawner arSpawner)
    {
        arrowSpawner = arSpawner;
    }

    public void SetIndex(int idx)
    {
        index = idx;
    }
}
