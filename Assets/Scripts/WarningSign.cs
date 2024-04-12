using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
    // Dictionary with key as signLevel and value potential cash
    public static Dictionary<int, int> FixPipeScores = new Dictionary<int, int>
    {
        { 0, 1000 },
        { 1, 3000 },
        { 2, 5000 }
    };
    public static Dictionary<int, int> FlowPipeScores = new Dictionary<int, int>
    {
        { 0, 100 },
        { 1, 1000 },
        { 2, 2000 },
        { 3, 4000 }
    };

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
            gameName = "Flow Pipe";
        } else {
            gameName = "Fix Pipe";
        }
        gameInfoPanelInstance.transform.Find("MiniGameName").GetComponent<TextMeshPro>().text = gameName;
    }
    private void AddCash() 
    {
        if (signMiniGame == 1)  // Assuming 1 is for Flow Pipe
        {
            if (FlowPipeScores.TryGetValue(signLevel, out int flowCash))
            {
                potentialCash = flowCash;
            }
        }
        else if (signMiniGame == 2)  // Assuming 2 is for Fix Pipe
        {
            if (FixPipeScores.TryGetValue(signLevel, out int fixCash))
            {
                potentialCash = fixCash;
            }
        }
        gameInfoPanelInstance.transform.Find("MiniGameCash").GetComponent<TextMeshPro>().text = "<color=green>$</color>:" + potentialCash;
    }

    private void UpdateInfoText()
    {
        Debug.Log(gameInfoPanelInstance);
        // Assuming each text component's tag or name is set to identify them
        gameInfoPanelInstance.transform.Find("MiniGameLevel").GetComponent<TextMeshPro>().text = "Level: " + signLevel.ToString();
        AddCash();
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
