using UnityEngine;
using UnityEngine.SceneManagement; // Required for SceneManager

public class RetryButton : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Color normalColor = Color.white;   // Default color
    public Color hoverColor = Color.gray;     // Color when hovered
    public Color pressedColor = Color.black;  // Color when pressed

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

        Debug.Log("Reloading the current scene.");
        // Delay the scene reload slightly to allow color change to show
        Invoke("ReloadScene", 0.1f);
    }

    private void OnMouseUp()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = hoverColor; // Return to hover color after release
        }
    }

    private void ReloadScene()
    {
        // Reload the current active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
