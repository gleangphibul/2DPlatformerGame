using UnityEngine;

public class LadderMovement : MonoBehaviour
{
    private float vertical;
    private float speed = 5f;
    private bool isLadder;
    private bool isClimbing;

    [SerializeField] private Rigidbody2D playerRigidbody;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
            isClimbing = false;
        }
    }

    private void FixedUpdate()
    {
        if (isClimbing)
        {
            playerRigidbody.gravityScale = 0f;
            playerRigidbody.linearVelocity = new Vector2(playerRigidbody.linearVelocity.x, vertical * speed);
        }
        else
        {
            playerRigidbody.gravityScale = 1f;
        }
    }

    void Update()
    {
        vertical = Input.GetAxis("Vertical");
        if (isLadder && Mathf.Abs(vertical) > 0f)
        {
            isClimbing = true;
        }
    }
}
