using TMPro;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;


public class PlayerController : MonoBehaviour
{

@@ -21,13 +23,13 @@ public class PlayerController : MonoBehaviour

private float horizontalInput;

    // for jumping
    [Header("Jumping")]

[SerializeField] private LayerMask groundLayer;

[SerializeField] private LayerMask wallLayer;

    // for dashing
    [Header("Dashing")]
private bool canDash = true;
private bool isDashing;

@@ -39,19 +41,43 @@ public class PlayerController : MonoBehaviour

[SerializeField] private TrailRenderer tr;

    [Header("Animation")]

    public Sprite spriteLeft;
    public Sprite spriteLeftAlt;
    public Sprite spriteRight;
    public Sprite spriteRightAlt;

    public SpriteRenderer playerSpriteRenderer;

    private float animationTimer = 0f; // Timer to control animation switching
    public float animationSwitchTime = 0.1f; // Time between sprite changes
    private bool useAlternateSprite = false; // Track whether to use the alternate sprite

    [Header("For others")]

    public float fallLimit = -5;


// Called each time an instance is loaded
private void Awake()
{
ratRigidBody = GetComponent<Rigidbody2D>();
circleCollider = GetComponent<CircleCollider2D>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
}


// Update is called once per frame
void Update()
{

        // AnimatePlayerSprite();

        // if (transform.position.y < fallLimit) {
        //     ReloadScene();
        // }

if (isDashing)
{
return;
@@ -174,4 +200,41 @@ private bool onWall()
RaycastHit2D raycastHit = Physics2D.BoxCast(circleCollider.bounds.center, circleCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
return raycastHit.collider != null;
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
        if (ratRigidBody.linearVelocity.x > 0) // Moving right
        {
            playerSpriteRenderer.sprite = useAlternateSprite ? spriteRightAlt : spriteRight;
            playerSpriteRenderer.flipX = false;
        }
        else if (ratRigidBody.linearVelocity.x < 0) // Moving left
        {
            playerSpriteRenderer.sprite = useAlternateSprite ? spriteLeftAlt : spriteLeft;
            playerSpriteRenderer.flipX = true;
        }
    }


    // private void ReloadScene(){
    //     SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    // }

    // private void OnCollisionEnter2D(Collision2D collision){
    //     if (collision.gameObject.CompareTag("Water")){
    //         ReloadScene();
    //     }
    // }

}