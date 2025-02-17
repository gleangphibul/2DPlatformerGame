using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    public float speed = 5;
    public float jumpForce = 50;
    private Rigidbody2D playerRigidbody;

    [Header("Animation")]

    public Sprite spriteLeft;
    public Sprite spriteLeftAlt;
    public Sprite spriteRight;
    public Sprite spriteRightAlt;

    public SpriteRenderer playerSpriteRenderer;

    private float animationTimer = 0f; // Timer to control animation switching
    public float animationSwitchTime = 0.1f; // Time between sprite changes
    private bool useAlternateSprite = false; // Track whether to use the alternate sprite

    public float fallLimit = -5;

    private void Move()
    {
        float moveHorizontal = 0f;
        float moveVertical = playerRigidbody.linearVelocity.y; // Keep vertical velocity constant (affected by gravity)

        if (Input.GetKey(KeyCode.LeftArrow)) // Move left
        {
            moveHorizontal = -1f;
        }
        else if (Input.GetKey(KeyCode.RightArrow)) // Move right
        {
            moveHorizontal = 1f;
        }

        // Apply movement based on horizontal input
        playerRigidbody.linearVelocity = new Vector2(moveHorizontal * speed, moveVertical);

        // Handle jumping
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            playerRigidbody.linearVelocity = new Vector2(playerRigidbody.linearVelocity.x, jumpForce); // Apply jump force
        }
    }

    private void AnimatePlayerSprite()
    {
        // Update the animation timer
        animationTimer += Time.deltaTime;

        // Switch sprite every animationSwitchTime seconds
        if (animationTimer >= animationSwitchTime)
        {
            animationTimer = 0f; // Reset timer
            useAlternateSprite = !useAlternateSprite; // Toggle sprite state
        }

        // Determine which sprite to display based on movement direction
        if (playerRigidbody.linearVelocity.x > 0) // Moving right
        {
            playerSpriteRenderer.sprite = useAlternateSprite ? spriteRightAlt : spriteRight;
            playerSpriteRenderer.flipX = false;
        }
        else if (playerRigidbody.linearVelocity.x < 0) // Moving left
        {
            playerSpriteRenderer.sprite = useAlternateSprite ? spriteLeftAlt : spriteLeft;
            playerSpriteRenderer.flipX = true;
        }
    }


    private void ReloadScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.CompareTag("Water")){
            ReloadScene();
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        AnimatePlayerSprite();

        if (transform.position.y < fallLimit) {
            ReloadScene();
        }
    }
}

    