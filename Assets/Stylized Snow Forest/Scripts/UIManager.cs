using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Text collectibleCounterText;
    public Text timerText;
    public Text gameOverScoreText;
    public Button restartButton; // Reference to the Restart button

    void Start()
    {
        // Hide the Game Over screen initially
        gameOverScoreText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);

        // Add a listener to the restart button
        restartButton.onClick.AddListener(RestartGame);
    }

    public void UpdateCollectibleCounter(string collectibleName, int count)
    {
        collectibleCounterText.text = $"{collectibleName}: {count}";
    }

    public void UpdateTimer(float timeRemaining)
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = $"Time: {minutes:00}:{seconds:00}";
    }

    public void DisplayGameOver(int score)
    {
        // Show the Game Over screen, score, and restart button
        gameOverScoreText.text = $"Score: {score}";
        gameOverScoreText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);

        // Hide the counter and timer when the game is over
        collectibleCounterText.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);
    }

    // Method to restart the game
    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reloads the current scene
    }
}
