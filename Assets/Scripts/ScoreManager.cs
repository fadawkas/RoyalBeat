using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public AudioSource hitSFX;
    public AudioSource missSFX;
    public TMPro.TextMeshPro scoreText;
    public static int healthScore;
    public GameObject deathPrefab; // Reference to the prefab to instantiate

    void Start()
    {
        Instance = this;
        healthScore = PlayerPrefs.GetInt("HealthScore", 30);
        Debug.Log($"Initialized health score: {healthScore}");
    }

    public static void Hit()
    {
        Instance.hitSFX.Play();
    }

    public static void Miss()
    {
        // Prevent health from going below 0
        healthScore = Mathf.Max(healthScore - 1, 0);
        Instance.missSFX.Play();

        // Only call Death() and instantiate the prefab when health reaches exactly 0
        if (healthScore == 0)
        {
            AnimManagerPlayer.Death(); // Trigger the death animation when health is 0
            SongManager.StopSong();

            // Instantiate the death prefab at the center of the screen with z = -8f
            if (Instance.deathPrefab != null)
            {
                Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 3.2f, Camera.main.nearClipPlane);
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenCenter);
                worldPosition.z = -8f; // Set z position to -8f

                // Instantiate the prefab without modifying its size
                Instantiate(Instance.deathPrefab, worldPosition, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning("Death prefab is not assigned in the inspector!");
            }
        }
    }

    private void Update()
    {
        // Update the displayed score
        scoreText.text = healthScore.ToString();
    }
}
