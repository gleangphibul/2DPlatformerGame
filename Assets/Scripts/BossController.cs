using UnityEditor.Callbacks;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    private Transform currentPoint;
    public float speed;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    public PlayerController player;
    public bool isNearBoss = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentPoint = pointB.transform;
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
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

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player"))
        {
            isNearBoss = true;
            Debug.Log("Near boss");
        }
    }
    
    public void Die()
    {
        Debug.Log("Boss die");
        Destroy(gameObject); // Destroy the enemy
    }
}
