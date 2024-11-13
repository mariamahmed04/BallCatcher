using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject welcomeMenu;           // Assign the WelcomeMenu panel here
    public Button startGameButton;           // Assign the StartGame button here
    public PlayerMovement PlayerMovement; // Reference to player controller to manage input

    void Start()
    {
        // Show the welcome menu and disable player input initially
        welcomeMenu.SetActive(true);
        PlayerMovement.enabled = false;

        // Add listener to the start button
        startGameButton.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        // Hide the welcome menu and enable player input
        welcomeMenu.SetActive(false);
        PlayerMovement.enabled = true;

        // Additional game start logic can be added here if needed
    }
}
