using UnityEditor.Callbacks;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    private Transform currentPoint;
    public float speed;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentPoint = pointB.transform;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 point = currentPoint.position - transform.position;
        if (currentPoint == pointB.transform) {
            rb.linearVelocity = new Vector2(speed,0);
            spriteRenderer.flipX = false;
        } else {
            rb.linearVelocity = new Vector2(-speed,0);
            spriteRenderer.flipX = true;
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform) {
            currentPoint = pointA.transform;
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform) {
            currentPoint = pointB.transform;
            
        }
    }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.CompareTag("Player"))
    //     {
    //         // Kill the player
    //         PlayerController player = other.GetComponent<PlayerController>();
    //         if (player != null)
    //         {
    //             player.ReloadScene();
    //         }
    //     }
    // }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            // Check if the player is above the enemy
            if (transform.position.y < (other.transform.position.y-0.5)) {
                // Kill the enemy
                Die();
                // EnemyController enemy = GetComponent<EnemyController>();
                // if (enemy != null) {
                //     enemy.Die();
                // }
                // Bounce the player up
                // rb.linearVelocity = new Vector2(rb.linearVelocity.x, bounceForce);
            } else {
                // Player dies if they touch the enemy from the side
                PlayerController player = other.GetComponent<PlayerController>();
                if (player != null) {
                    player.ReloadScene();
                }
            }
        }
    }

    public void Die()
    {
        Destroy(gameObject); // Destroy the enemy
    }
}