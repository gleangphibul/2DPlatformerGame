using UnityEngine;

public class Key : MonoBehaviour
{
    public PlayerController player; // Reference to the player script

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player has collided with the key
        if (collision.gameObject.CompareTag("Player"))
        {
            // Add the key to the player's collection
            player.AddKey();
            // Destroy the key object after collecting it
            Destroy(gameObject);
        }
    }
}
