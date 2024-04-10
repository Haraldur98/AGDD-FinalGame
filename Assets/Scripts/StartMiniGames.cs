using UnityEngine;
using TMPro;

public class StartMiniGame : MonoBehaviour {
    public GameObject[] miniGame;
    public GameObject tutorialPanel;
    public TextMeshProUGUI timerText;
    public int currentLevel = 0;

    private bool isActive = false;

    // void Start() {
    //     miniGame = new GameObject[4];
    // }

    void Update()
    {
        // when enter is pressed, close the tutorial panel
        if (Input.GetKeyDown(KeyCode.Return))
        {
            timerText.gameObject.SetActive(true);
        }

        if (isActive)
        {
            miniGame[currentLevel].SetActive(true);
        }
        else {
            miniGame[currentLevel].SetActive(false); 
        }
    }

    public void StartGame (Vector3 pos, int level) 
    {
        miniGame[level].transform.position = pos;
        currentLevel = level;
        isActive = true;
    }

    public void SetIsActive (bool active)
    {
        isActive = active;
    }


}