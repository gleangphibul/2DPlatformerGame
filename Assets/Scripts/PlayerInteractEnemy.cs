using UnityEngine;

public class PlayerInteractEnemy : MonoBehaviour
{
    public float bounceForce = 10f;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Enemy"))
        {
            // Check if the player is above the enemy
            if (transform.position.y > other.transform.position.y) {
                // Kill the enemy
                EnemyController enemy = other.GetComponent<EnemyController>();
                if (enemy != null) {
                    enemy.Die();
                }
                // Bounce the player up
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, bounceForce);
            } else {
                // Player dies if they touch the enemy from the side
                Die();
            }
        }
    }

    public void Die()
    {
        Debug.Log("Player Died!");
        // Add death logic here (e.g., restart level)
    }
}
