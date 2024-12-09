using UnityEngine;
using UnityEngine.SceneManagement; // Required for SceneManager

public class SpriteButton : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Color normalColor = Color.white;   // Default color
    public Color hoverColor = Color.gray;     // Color when hovered
    public Color pressedColor = Color.black;  // Color when pressed

    // The name of the scene to load
    [SerializeField] private string sceneName;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = normalColor; // Set the initial color
        }
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

        if (!string.IsNullOrEmpty(sceneName))
        {
            Debug.Log($"Starting the game by loading scene: {sceneName}");
            // Delay the scene load slightly to allow color change to show
            Invoke("LoadScene", 0.1f);
        }
        else
        {
            Debug.LogError("Scene name is not set!");
        }
    }

    private void OnMouseUp()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = hoverColor; // Return to hover color after release
        }
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
