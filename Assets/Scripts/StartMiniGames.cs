using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class StartMiniGame : MonoBehaviour
{
    public GameObject tutorial;
    public GameObject levelOne;
    public GameObject levelTwo;
    public GameObject levelThree;
    public GameObject tutorialPanel;
    public TextMeshProUGUI timerText;
    public UnityEvent onMiniGameEnd;

    void Update()
    {
        // when enter is pressed, close the tutorial panel
        if (Input.GetKeyDown(KeyCode.Return))
        {
            timerText.gameObject.SetActive(true);
        }
    }

    public void StartGame(int level)
    {
        DisableMinigames();
        if (level == 0)
        {
            tutorial.SetActive(true);
        }
        else if (level == 1)
        {
            levelOne.SetActive(true);
        }
        else if (level == 2)
        {
            levelTwo.SetActive(true);
        }
        else if (level == 3)
        {
            levelThree.SetActive(true);
        }
    }

    public void DisableMinigames()
    {
        tutorial.SetActive(false);
        levelOne.SetActive(false);
        levelTwo.SetActive(false);
        levelThree.SetActive(false);
    }

}