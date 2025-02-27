using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA;  // Start point of the platform
    public Transform pointB;  // End point of the platform
    public float speed = 3f;  // Speed of the platform's movement
    private bool movingToB = true;  // Flag to check direction

    // For 2D physics interactions (adjust for 3D if needed)
    private void Update()
    {
        // Move the platform between point A and B
        if (movingToB)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointB.position, speed * Time.deltaTime);
            
            // If the platform reaches point B, switch direction
            if (transform.position == pointB.position)
            {
                movingToB = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, pointA.position, speed * Time.deltaTime);

            // If the platform reaches point A, switch direction
            if (transform.position == pointA.position)
            {
                movingToB = true;
            }
        }
    }

    // Detect player on the platform and move with it
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Make the player move with the platform
            collision.transform.parent = transform;  // Make the player a child of the platform
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Detach player from platform when they leave
            collision.transform.parent = null;
        }
    }
}
