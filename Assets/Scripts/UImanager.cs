using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Include the UnityEngine.UI namespace
using TMPro; // Include the TextMeshPro namespace

public class UImanager : MonoBehaviour
{
    public enum ActionState
    {
        None,
        PickUp,
        Climb,
        Fix,
    }

    public Transform playerTransform; // The player's transform
    public GameObject actionIndicator; // The UI element
    public TextMeshProUGUI actionText; // The UI Text
    public Button ladderButton; // New Button variable

    [Header("refrence to scripts to detect if the ladder is near or not")]
    public PickUpRadius pickUpRadius; // Reference to the PickUpRadius script
    public ItemManager itemManager; // Reference to the ItemManager script
    
    private Dictionary<ActionState, string> actionMessages;

    private void Start()
    {
        actionMessages = new Dictionary<ActionState, string>
        {
            { ActionState.None, "" },
            { ActionState.PickUp, "Right click to pick up ladder" },
            { ActionState.Climb, "Press W to climb" },
            { ActionState.Fix, "Left click to fix" },
        };
    }

    private void Update()
    {
        // Position the action indicator above the player
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(playerTransform.position);
        screenPosition.y += 50; // Adjust this value to position the indicator above the player
        screenPosition.x -= 20; // Adjust this value to position the indicator above the player
        actionIndicator.transform.position = screenPosition;

        if ( pickUpRadius.canPickUp == false && itemManager.isLadderPlaced == true)
        {
            // If the ladder is lost, enable the button
            ladderButton.interactable = true;
        }
        else
        {
            // If the ladder is not lost, disable the button
            ladderButton.interactable = false;
        }
    
    }

    public void ShowIndicator(ActionState state)
    {
        // Show or hide the action indicator
        actionIndicator.SetActive(state != ActionState.None);

        // Update the action text
        actionText.text = actionMessages[state];
    }
}