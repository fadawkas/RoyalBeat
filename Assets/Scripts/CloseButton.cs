using UnityEngine;

public class CloseButton : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Color normalColor = Color.white;   // Default color
    public Color hoverColor = Color.gray;     // Color when hovered
    public Color pressedColor = Color.black;  // Color when pressed

    // The prefab or object to destroy
    [SerializeField] private GameObject objectToDestroy;

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

        if (objectToDestroy != null)
        {
            Debug.Log("Destroying object...");
            // Delay the destruction slightly to allow color change to show
            Invoke("DestroyObject", 0.1f);
        }
        else
        {
            Debug.LogError("Object to destroy is not set!");
        }
    }

    private void OnMouseUp()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = hoverColor; // Return to hover color after release
        }
    }

    private void DestroyObject()
    {
        Destroy(objectToDestroy); // Destroy the object
    }
}
