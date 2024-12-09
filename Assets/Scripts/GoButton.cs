using UnityEngine;
using UnityEngine.SceneManagement;

public class GoButton : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Color normalColor = Color.white;   // Default color
    public Color hoverColor = Color.gray;     // Color when hovered
    public Color pressedColor = Color.black;  // Color when pressed

    private int waypointIndex = 0; // The index to pass to the "Overworld" scene
    private int healthScore = 30;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = normalColor; // Set the initial color
        }
    }

    void Start()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        switch (currentSceneName)
        {
            case "LevelOne": // Level 1
                waypointIndex = 3;
                healthScore = 40;
                PlayerPrefs.SetInt("Level1Completed", 1);  // Mark Level 1 as completed
                break;

            case "LevelTwo": // Level 2
                waypointIndex = 4;
                healthScore = 50;
                PlayerPrefs.SetInt("Level2Completed", 1);  // Mark Level 2 as completed
                break;

            case "LevelThree": // Level 3
                waypointIndex = 4;
                healthScore = 60;
                break;
        }

        // Save any changes to PlayerPrefs
        PlayerPrefs.Save();
    }

    private void OnMouseEnter()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = hoverColor; // Change color when hovered
        }
    }

    private void OnMouseExit()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = normalColor; // Reset color when no longer hovered
        }
    }

    private void OnMouseDown()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = pressedColor; // Change color when pressed
        }

        Debug.Log("Loading 'Overworld' scene with waypoint index: " + waypointIndex);
        Debug.Log("Setting the Player Health Score: " + healthScore);

        // Save the waypoint index and health score
        PlayerPrefs.SetInt("WaypointIndex", waypointIndex);
        PlayerPrefs.SetInt("HealthScore", healthScore);
        PlayerPrefs.Save();

        // Delay the scene transition slightly to allow color change to show
        Invoke("LoadOverworldScene", 0.1f);
    }

    private void OnMouseUp()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = hoverColor; // Return to hover color after release
        }
    }

    private void LoadOverworldScene()
    {
        // Load the "Overworld" scene
        SceneManager.LoadScene("Overworld");
    }
}
