using UnityEngine;

public class GemPickUp : MonoBehaviour
{
    [SerializeField] private int value; // Value of the gem to be added to the player's score
    private bool hasTriggered = false; // Ensure the gem is picked up only once
    private GemManager gemManager;   // Reference to the gemManager instance

    private void Start()
    {
        // Assign the instance of gemManager
        gemManager = GemManager.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collider belongs to the player and the gem hasn't been picked up yet
        if (collision.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true; // Prevent further triggering
            gemManager.ChangeGems(value); // Add the gem's value to the total Gems
            Destroy(gameObject); // Remove the gem from the scene
        }
    }
}
