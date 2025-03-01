using UnityEngine;

public class Gate : MonoBehaviour
{
    public bool isNearGate = false; // Track if the player is near the gate
    private BoxCollider2D gateCollider;
    private SpriteRenderer spriteRenderer;

    public PlayerController player; 

    void Start()
    {
        gateCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Optional: Hide gate on opening
    }

    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player reached the gate! Press Space to open.");
            isNearGate = true;
        }
    }

    public void OpenGate()
    {
        Debug.Log("Gate Opened!");
        gateCollider.enabled = false; // Disable the collider so player can pass
        spriteRenderer.enabled = false; // Optional: Hide gate sprite
        isNearGate = false;
    }
}
