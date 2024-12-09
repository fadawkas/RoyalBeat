using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public AudioSource audioSource; // Reference to your AudioSource
    public Slider slider;           // Reference to the Slider
    public GameObject winPrefab;    // Reference to the prefab to spawn

    private bool deathTriggered = false; // To ensure the Death method is called only once
    public KeyCode skipKey = KeyCode.Tab; // First skip key (Tab)
    public KeyCode skipKey2 = KeyCode.Return; // Second skip key (Enter)

    private bool audioStopped = false; // Flag to check if audio has been stopped manually

    void Start()
    {
        if (audioSource != null && slider != null)
        {
            // Set the slider's max value to the duration of the audio
            slider.maxValue = audioSource.clip.length;
            slider.value = audioSource.clip.length; // Set to full duration for countdown timer
        }
        else
        {
            Debug.LogWarning("AudioSource or Slider is not assigned.");
        }
    }

    void Update()
    {
        if (audioSource != null && slider != null)
        {
            // If audio is playing, update the slider value based on the remaining audio time
            if (!audioStopped)
            {
                slider.value = audioSource.clip.length - audioSource.time;

                // Check if the slider value reaches 0 and trigger the death animation and prefab spawn
                if (slider.value <= 0 && !deathTriggered)
                {
                    deathTriggered = true; // Prevent multiple calls
                    TriggerDeath(); // Trigger the death animation and spawn prefab
                }
            }
            else
            {
                // Ensure the slider is at 0 when the audio is stopped
                slider.value = 0;
                if (!deathTriggered)
                {
                    deathTriggered = true;
                    TriggerDeath();
                }
            }
        }

        // Check if both keys are being pressed (Tab and Enter)
        if (Input.GetKey(skipKey) && Input.GetKey(skipKey2))
        {
            if (audioSource != null)
            {
                // Stop the audio immediately and set slider to 0
                audioSource.Stop();
                audioStopped = true; // Mark the audio as stopped

                // Trigger the death animation and spawn prefab
                if (!deathTriggered)
                {
                    deathTriggered = true;
                    TriggerDeath();
                }
            }
        }
    }

    // Method to trigger the death animation and spawn the prefab
    private void TriggerDeath()
    {
        AnimManagerOrc.Death(); // Call the Death animation
        Debug.Log("Death animation triggered.");

        // Instantiate the win prefab at the center of the screen with z = -8f
        if (winPrefab != null)
        {
            Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 3.2f, Camera.main.nearClipPlane);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenCenter);
            worldPosition.z = -8f; // Set z position to -8f

            Instantiate(winPrefab, worldPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Win prefab is not assigned.");
        }
    }
}
