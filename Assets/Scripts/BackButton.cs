using UnityEngine;

public class BackButton : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Color normalColor = Color.white;   // Default color
    public Color hoverColor = Color.gray;     // Color when hovered
    public Color pressedColor = Color.black;  // Color when pressed

    // Reference to the prefab you want to destroy
    [SerializeField] private GameObject prefabToDestroy;

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

        // Destroy the prefab if it is not null
        if (prefabToDestroy != null)
        {
            Destroy(prefabToDestroy);
            Debug.Log($"Destroyed prefab: {prefabToDestroy.name}");
        }
    }

    private void OnMouseUp()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = hoverColor; // Return to hover color after release
        }
    }
}
