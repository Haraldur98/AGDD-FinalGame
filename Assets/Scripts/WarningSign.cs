using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WarningSign : MonoBehaviour
{
    public GameObject gameInfoPanelPrefab; // Assign the prefab in the Inspector
    private GameObject gameInfoPanelInstance;
    private CanvasGroup infoPanelCanvasGroup;
    private GameManager gameManager;
    private bool isInRange;
    private int signMiniGame;
    private int signLevel;
    private string gameName;
    public int potentialCash;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Manage the visibility and alpha of the info panel based on player proximity
        if (gameInfoPanelInstance != null)
        {
            float targetAlpha = isInRange ? 1 : 0;
            infoPanelCanvasGroup.alpha = Mathf.MoveTowards(infoPanelCanvasGroup.alpha, targetAlpha, Time.deltaTime * 3);
        }
    }

    private void OnMouseDown()
    {
        if (isInRange)
        {
            string sceneString = signMiniGame == 1 ? "MiniGameUNO" : (signMiniGame == 2 ? "minigame2" : "SKRAAH USA USA USA");
            // TODO: get signLevel into scenes
            gameManager.LoadMiniGame(sceneString, signLevel);
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        isInRange = true;
        if (gameInfoPanelInstance == null)
        {
            
            Debug.Log("Creating game info panel");
            // Instantiate and set up the game info panel
            gameInfoPanelInstance = Instantiate(gameInfoPanelPrefab, transform.position, Quaternion.identity);
            // gameInfoPanelInstance.transform.SetParent(transform, false); // Optional: Set parent to keep the UI clean
            infoPanelCanvasGroup = gameInfoPanelInstance.GetComponent<CanvasGroup>();
            // Update the text elements
            UpdateInfoText();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isInRange = false;
        // Destroy the game info panel when the player leaves the trigger area
        if (gameInfoPanelInstance != null)
            Destroy(gameInfoPanelInstance);
    }

    private void AddName() 
    {
        if (signMiniGame == 1) {
            gameInfoPanelInstance.transform.Find("MiniGameName").GetComponent<TextMeshPro>().text = "Flow Pipe";
        } else {
            gameInfoPanelInstance.transform.Find("MiniGameName").GetComponent<TextMeshPro>().text = "Fix Pipe";
        }
    }

    private void UpdateInfoText()
    {
        Debug.Log(gameInfoPanelInstance);
        // Assuming each text component's tag or name is set to identify them
        gameInfoPanelInstance.transform.Find("MiniGameLevel").GetComponent<TextMeshPro>().text = "Level: " + signLevel.ToString();
        gameInfoPanelInstance.transform.Find("MiniGameCash").GetComponent<TextMeshPro>().text = "$" + potentialCash.ToString();
        AddName();
    }

    private void OnDestroy()
    {
        // Ensure to clean up the instantiated panel
        if (gameInfoPanelInstance != null)
            Destroy(gameInfoPanelInstance);
    }

    public void SetMinigame(int miniGame)
    {
        signMiniGame = miniGame;
    }

    public void SetLevel(int level)
    {
        signLevel = level;
    }
}
