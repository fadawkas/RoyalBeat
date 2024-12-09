using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // Required for scene management

public class CutSceneController : MonoBehaviour
{
    public TextMeshPro textMeshPro;  // Reference to the TextMeshPro 3D object
    public GameObject nextButton;   // Reference to the Next button GameObject (Sprite with BoxCollider2D)
    public GameObject[] environments; // Array of environment prefabs
    public string[] cutsceneText;   // Array of texts for the cutscene

    private SpriteRenderer nextButtonRenderer; // SpriteRenderer for the button
    private Color originalColor;               // Original color of the button
    public Color hoverColor = Color.gray;      // Color when hovering
    public Color pressedColor = Color.red;     // Color when pressed

    private GameObject currentEnvironment;  // Tracks the currently active environment
    private int currentLine = 0;            // Tracks the current line in the cutscene
    [SerializeField] private string sceneName;

    void Start()
    {
        // Initialize button's original color
        nextButtonRenderer = nextButton.GetComponent<SpriteRenderer>();
        if (nextButtonRenderer != null)
        {
            originalColor = nextButtonRenderer.color;
        }
        else
        {
            Debug.LogError("Next button does not have a SpriteRenderer!");
        }

        // Initialize cutscene by updating the first line and environment
        nextButton.SetActive(true);
        if (cutsceneText.Length > 0 && environments.Length > 0)
        {
            UpdateCutscene();
        }
        else
        {
            Debug.LogError("Cutscene text or environments array is empty!");
        }
    }

    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);

        // Check for mouse hover
        if (hitCollider != null && hitCollider.gameObject == nextButton)
        {
            nextButtonRenderer.color = hoverColor;

            // Check for mouse click
            if (Input.GetMouseButtonDown(0))
            {
                nextButtonRenderer.color = pressedColor; // Change color on press
                OnNextButtonClicked();                  // Proceed to next cutscene part
            }
        }
        else
        {
            nextButtonRenderer.color = originalColor; // Revert color when not hovering
        }
    }

    void OnNextButtonClicked()
    {
        currentLine++;  // Move to the next line in the cutscene

        if (currentLine < cutsceneText.Length && currentLine < environments.Length)
        {
            UpdateCutscene();  // Update the text and environment
        }
        else
        {
            EndCutscene();  // End the cutscene when all lines are completed
        }
    }

    void UpdateCutscene()
    {
        // Update the TextMeshPro text
        textMeshPro.text = cutsceneText[currentLine];

        // Update the environment prefab
        if (currentEnvironment != null)
        {
            Destroy(currentEnvironment);  // Remove the current environment
        }

        currentEnvironment = Instantiate(environments[currentLine]);  // Spawn the next environment prefab
    }

    void EndCutscene()
    {
        // Hide the text and button, clean up the current environment
        textMeshPro.gameObject.SetActive(false);
        nextButton.SetActive(false);

        if (currentEnvironment != null)
        {
            Destroy(currentEnvironment);
        }

        Debug.Log("Cutscene ended! Transitioning to Overworld scene...");

        // Change to the "Overworld" scene
        SceneManager.LoadScene(sceneName);
    }
}
