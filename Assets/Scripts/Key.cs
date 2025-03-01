using UnityEngine;

public class Key : MonoBehaviour
{
    private PlayerController player; // Reference to the player script

    void Start()
    {
        // Find the player object dynamically at runtime if not assigned in the inspector
        if (player == null)
        {
            player = GameObject.FindWithTag("Player")?.GetComponent<PlayerController>();
            if (player == null)
            {
                Debug.LogError("PlayerController not found. Make sure the player object has the 'Player' tag and a PlayerController component.");
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player has collided with the key
        if (collision.gameObject.CompareTag("Player") && player != null)
        {
            // Add the key to the player's collection
            player.AddKey();

            // Destroy the key object after collecting it
            Destroy(gameObject);
        }
    }
}
