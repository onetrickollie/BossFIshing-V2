using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    private bool canFish = false;
    private bool isFishing = false; // Prevent multiple fishing attempts
    private bool fishBiting = false; // Track if the fish is currently biting

    [SerializeField]
    private List<FishData> fishList = new List<FishData>(); // List of fish data (ScriptableObjects)
    [SerializeField]
    private InventoryManager inventoryManager;
    [SerializeField]
    private AudioSource mainAudioSource; // Reference to an AudioSource for playing general sounds
    [SerializeField]
    private AudioSource fishBiteAudioSource; // Separate AudioSource for fish bite sound
    [SerializeField]
    private AudioClip fishBiteSound; // Sound when fish bites
    [SerializeField]
    private GameObject exclamationMark; // Reference to the exclamation mark GameObject
    [SerializeField]
    private Animator animator; // Reference to the Animator component
    [SerializeField]
    private TMP_Text InGameMessage; // Text field for displaying messages
    [SerializeField]
    private FishingMinigame fishingMinigame; // Reference to the minigame script

    private CatchMessageUI catchMessageUI;

    private bool isMinigameRunning = false; // New flag to indicate if the minigame is active

    private void Start()
    {
        catchMessageUI = FindObjectOfType<CatchMessageUI>();
        if (inventoryManager == null)
        {
            Debug.LogWarning("No InventoryManager found in the scene. Make sure to add one to the scene.");
        }

        if (exclamationMark != null)
        {
            exclamationMark.SetActive(false); // Ensure the exclamation mark is hidden initially
        }

        if (mainAudioSource == null)
        {
            Debug.LogWarning("No AudioSource assigned for general sound playback.");
        }
        if (fishBiteAudioSource == null)
        {
            Debug.LogWarning("No AudioSource assigned for fish bite sound playback.");
        }

        // Hook up the minigame completion events
        if (fishingMinigame != null)
        {
            fishingMinigame.OnMinigameWin += CatchFish; // Call CatchFish if player wins
            fishingMinigame.OnMinigameLose += MissedFish; // Call MissedFish if player loses
        }
    }

    private void Update()
    {
        if (canFish && Input.GetMouseButtonDown(0) && !isFishing && !isMinigameRunning)
        {
            StartCoroutine(FishingRoutine()); // Start the fishing coroutine
        }

        if (fishBiting && Input.GetMouseButtonDown(0))
        {
            fishBiting = false; // Stop the biting state
            if (exclamationMark != null)
            {
                exclamationMark.SetActive(false);
            }

            // Start the fishing minigame if the player successfully clicks within the biting window
            if (fishingMinigame != null)
            {
                isMinigameRunning = true; // Set the flag to indicate the minigame is running
                fishingMinigame.StartMinigame(); // Show the progress bar and start minigame logic
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            canFish = true;
            InGameMessage.text = "You have entered the fishing zone";
            Debug.Log("Entered fishing area.");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            canFish = false;
            InGameMessage.text = "You have left the fishing zone";
            Debug.Log("Exited fishing area.");
        }
    }

    private IEnumerator FishingRoutine()
    {
        isFishing = true;
        Debug.Log("Fishing routine started.");
        animator.SetInteger("RodIndex", GameManager.Instance.GetEquippedRodIndex());
        Debug.Log($"Set RodIndex in Animator: {GameManager.Instance.GetEquippedRodIndex()}");

        // Play fishing animation
        if (animator != null)
        {
            animator.SetInteger("RodIndex", GameManager.Instance.GetEquippedRodIndex()); // Update RodIndex first
            animator.ResetTrigger("StartFishing");
            animator.SetTrigger("StartFishing");
            Debug.Log("Fishing animation triggered with updated RodIndex.");
        }
        else
        {
            Debug.LogWarning("No Animator component found on the player.");
        }

        // Wait for a random duration before a fish is on the hook
        float waitTime = UnityEngine.Random.Range(3f, 5f);
        Debug.Log($"Waiting for {waitTime} seconds for a fish to bite...");
        yield return new WaitForSeconds(waitTime);

        // Play fish bite sound and display exclamation mark
        if (fishBiteAudioSource != null && fishBiteSound != null)
        {
            fishBiteAudioSource.clip = fishBiteSound;
            fishBiteAudioSource.Play();
            StartCoroutine(StopAudioAfterDelay(fishBiteAudioSource, 1f));
        }

        if (exclamationMark != null)
        {
            exclamationMark.SetActive(true);
        }

        // Set state for the fish biting and give player a brief window to respond
        fishBiting = true;

        // Give the player a window of time to respond
        float responseWindow = 1f;
        yield return new WaitForSeconds(responseWindow);

        // If player didn't react in time, it's a miss
        if (fishBiting)
        {
            fishBiting = false;
            if (exclamationMark != null)
            {
                exclamationMark.SetActive(false);
            }
            InGameMessage.text = "Missed the fish!";
            Debug.Log("Player missed the fish.");

            // End the fishing routine
            isFishing = false;
            yield break;
        }

        // If player responded, start the minigame
        if (fishBiting == false && isFishing)
        {
            fishingMinigame.StartMinigame();
        }

        // End fishing routine
        isFishing = false;
    }

    private void CatchFish()
    {
        isMinigameRunning = false;
        fishBiting = false;
        isFishing = false;
        if (exclamationMark != null)
        {
            exclamationMark.SetActive(false);
        }

        int randomIndex = UnityEngine.Random.Range(0, fishList.Count);
        FishData caughtFish = fishList[randomIndex];

        // Display the catch message
        if (catchMessageUI != null)
        {
            catchMessageUI.DisplayCatchMessage(caughtFish.fishName);
        }

        InGameMessage.text = "You caught a fish!";
        Debug.Log($"Caught fish: {caughtFish.fishName}");

        // Create an Item and add to inventory
        if (inventoryManager != null)
        {
            int fishPrice = caughtFish.price;
            Item fishItem = new Item(caughtFish.fishName, 1, caughtFish.fishSprite, caughtFish.fishDescription, fishPrice, true);
            inventoryManager.AddItem(fishItem);
        }
    }

    private void MissedFish()
    {
        isMinigameRunning = false;
        fishBiting = false;
        isFishing = false;
        if (exclamationMark != null)
        {
            exclamationMark.SetActive(false);
        }

        InGameMessage.text = "Missed the fish!";
        Debug.Log("Player missed the fish during the minigame.");
    }

    private IEnumerator StopAudioAfterDelay(AudioSource source, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (source.isPlaying)
        {
            source.Stop();
        }
    }
}
