using System.Collections.Generic;
using UnityEngine.SceneManagement; // Needed for reloading or managing scenes
using System.Collections;
using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    public GameObject[] collectiblePrefabs;
    public Transform[] spawnPoints;
    public float minSpawnInterval = 1f;
    public float maxSpawnInterval = 5f;
    public int maxCollectibles = 10;

    private int currentCollectibleCount = 0;
    private string firstCollectibleName = null; // Name of the first collectible type
    private int collectibleCounter = 0;

    public UIManager uiManager;
    public GameObject gameOverScreen; // Game Over UI

    private float timer = 60f;        // 1-minute timer
    private bool timerStarted = false;
    private bool alertPlayed = false; // Ensures alert plays only once

    private AudioSource audioSource; // Audio source for sound effects

    public AudioClip collectibleSound;      // Sound for correct collectible
    public AudioClip wrongCollectibleSound; // Sound for wrong collectible
    public AudioClip alertSound;            // Sound for 3-second alert

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(SpawnCollectiblesAtRandomIntervals());
    }

    void Update()
    {
        // Check if the timer has started and is still running
        if (timerStarted && timer > 0)
        {
            // Decrease the timer by the time elapsed since last frame
            timer -= Time.deltaTime;
            uiManager.UpdateTimer(timer); // Update UI timer display

            // Play the 3-second alert if the timer is at 3 seconds and the alert hasn't played yet
            if (timer <= 3f && !alertPlayed)
            {
                PlayAlertSound();
                alertPlayed = true;
            }

            // Check if timer has run out
            if (timer <= 0)
            {
                EndGame();
            }
        }
    }

    IEnumerator SpawnCollectiblesAtRandomIntervals()
    {
        while (true)
        {
            if (currentCollectibleCount < maxCollectibles)
            {
                SpawnCollectible();
                currentCollectibleCount++;
            }

            float interval = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(interval);
        }
    }

    void SpawnCollectible()
    {
        Transform chosenPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject chosenPrefab = collectiblePrefabs[Random.Range(0, collectiblePrefabs.Length)];
        Instantiate(chosenPrefab, chosenPoint.position, Quaternion.identity);
    }

    public void RegisterFirstCollectible(string collectibleName)
    {
        // Start the timer on the first collectible collected
        if (!timerStarted)
        {
            timerStarted = true;
        }

        // Extract the part before the underscore
        string displayName = collectibleName.Split('_')[0];

        // Check if this is the first collectible type
        if (firstCollectibleName == null)
        {
            firstCollectibleName = displayName;
            collectibleCounter = 1;
            PlayCollectibleSound(); // Play sound for the first collectible
        }
        else if (displayName == firstCollectibleName)
        {
            collectibleCounter++;
            PlayCollectibleSound(); // Play sound for correct collectible
        }
        else
        {
            PlayWrongCollectibleSound(); // Play sound for wrong collectible
        }

        // Update the UI with the display name and count
        uiManager.UpdateCollectibleCounter(firstCollectibleName, collectibleCounter);
    }

    public void DecrementCollectibleCount()
    {
        currentCollectibleCount--;
    }

    private void PlayCollectibleSound()
    {
        // Play the correct collectible sound effect
        audioSource.PlayOneShot(collectibleSound);
    }

    private void PlayWrongCollectibleSound()
    {
        // Play the wrong collectible sound effect
        audioSource.PlayOneShot(wrongCollectibleSound);
    }

    private void PlayAlertSound()
    {
        // Play the 3-second alert sound
        audioSource.PlayOneShot(alertSound);
    }

    private void EndGame()
    {
        timerStarted = false;

        // Display the Game Over screen and show the score
        gameOverScreen.SetActive(true);
        uiManager.DisplayGameOver(collectibleCounter);
        
        // Stop all collectible spawning
        StopAllCoroutines();
        
        // Disable player movement
        FindObjectOfType<PlayerMovement>().enabled = false;
    }
}
