using UnityEngine;
using TMPro;

public class StartMiniGame : MonoBehaviour {
    public GameObject miniGame;
    public GameObject tutorialPanel;
    public TextMeshProUGUI timerText;

    void Update()
    {
        // when enter is pressed, close the tutorial panel
        if (Input.GetKeyDown(KeyCode.Return))
        {
            tutorialPanel.SetActive(false);
            miniGame.SetActive(true);
            timerText.gameObject.SetActive(true);
        }
    }


}