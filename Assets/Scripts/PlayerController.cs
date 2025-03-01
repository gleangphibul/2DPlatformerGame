using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;

    private PolygonCollider2D polygonCollider;

    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    private float wallJumpCoolDown;
    private float horizontalInput;

    private bool canDoubleJump;
    
    [Header("Jumping")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    [Header("Dashing")]
    private bool canDash = true;
    private bool isDashing;
    [SerializeField] private float dashingPower = 5f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    [Header("Move Animation")]
    public Sprite spriteLeft;
    public Sprite spriteLeftAlt;
    public Sprite spriteRight;
    public Sprite spriteRightAlt;
    private SpriteRenderer playerSpriteRenderer;

    private float animationTimer = 0f;
    public float animationSwitchTime = 0.1f;
    private bool useAlternateSprite = false;

    [Header("Attack Animation")]
    public Sprite attackSprite1;
    public Sprite attackSprite2;
    public Sprite attackSprite3;
    public Sprite attackSprite4;
    private bool isAttacking = false;
    private float attackTimer = 0f;
    private int attackFrame = 0;


    [Header("Reload Scene")]
    public float fallLimit = -5;

    [Header("Attack")]
    public float attackRange = 1f;
    public LayerMask breakableLayer;

    private AudioSource attackAudioSource;

    [Header ("Open Gate")]
    public bool hasKey = false;
    public Gate gate; 

    [Header ("Sword")]
    public bool hasSword = false;

    private void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        polygonCollider = GetComponent<PolygonCollider2D>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        attackAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Ensure the player respawns if they fall off
        if (transform.position.y < fallLimit)
        {
            ReloadScene();
        }

        Move();

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
         {
             StartCoroutine(Dash());
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isAttacking)
        {
            StartAttack();
        }

        if (hasKey && (gate.isNearGate == true) && Input.GetKeyDown(KeyCode.Space))
        {
            gate.OpenGate();
        }
    }

    private void Move()
    {
        if (isDashing) return; // Stop overriding velocity if dashing

        horizontalInput = Input.GetAxis("Horizontal");

        if (wallJumpCoolDown > 0.2f)
        {
            playerRigidBody.linearVelocity = new Vector2(horizontalInput * speed, playerRigidBody.linearVelocity.y);

            if (onWall() && !isGrounded())
            {
                playerRigidBody.gravityScale = 0;
                playerRigidBody.linearVelocity = Vector2.zero;
            }
            else
            {
                playerRigidBody.gravityScale = 10;
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Jump();
            }
        }
        else
        {
            wallJumpCoolDown += Time.deltaTime;
        }

        AnimatePlayerSprite();
    }


    private void Jump()
    {
        if (isGrounded())
        {
            playerRigidBody.linearVelocity = new Vector2(playerRigidBody.linearVelocity.x, jumpForce);
            canDoubleJump = true; // Reset double jump when touching the ground
        }
        else if (canDoubleJump)
        {
            playerRigidBody.linearVelocity = new Vector2(playerRigidBody.linearVelocity.x, jumpForce);
            canDoubleJump = false;
        }
        else if (onWall() && !isGrounded())
        {
            if (horizontalInput == 0)
            {
                playerRigidBody.linearVelocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                playerRigidBody.linearVelocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
            }
            wallJumpCoolDown = 0;
        }
    }

    private IEnumerator Dash()
    {
         canDash = false;
         isDashing = true;
         float originalGravity = playerRigidBody.gravityScale;
         playerRigidBody.gravityScale = 0; // Disable gravity during dash

         float dashDirection = (horizontalInput != 0) ? Mathf.Sign(horizontalInput) : Mathf.Sign(transform.localScale.x);
         playerRigidBody.linearVelocity = new Vector2(dashDirection * dashingPower, 0); // Keep Y velocity constant

     
         yield return new WaitForSeconds(dashingTime);
         

         isDashing = false;
         playerRigidBody.gravityScale = originalGravity; // Restore gravity
         yield return new WaitForSeconds(dashingCooldown);
         canDash = true;
    }


    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(polygonCollider.bounds.center, polygonCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(polygonCollider.bounds.center, polygonCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    private void AnimatePlayerSprite()
    {
        animationTimer += Time.deltaTime;

        if (isAttacking)
        {
            attackTimer += Time.deltaTime;

            if (attackTimer >= animationSwitchTime)
            {
                attackTimer = 0f;
                attackFrame++;

                switch (attackFrame)
                {
                    case 1:
                        playerSpriteRenderer.sprite = attackSprite1;
                        break;
                    case 2:
                        playerSpriteRenderer.sprite = attackSprite2;
                        break;
                    case 3:
                        playerSpriteRenderer.sprite = attackSprite3;
                        break;
                    case 4:
                        playerSpriteRenderer.sprite = attackSprite4;
                        Attack(); // Call attack function when last frame is reached
                        break;
                    default:
                        isAttacking = false; // End attack animation
                        break;
                }
            }
            return; // Prevent movement animation while attacking
        }

        // Movement animation
        if (animationTimer >= animationSwitchTime)
        {
            animationTimer = 0f;
            useAlternateSprite = !useAlternateSprite;
        }

        if (playerRigidBody.linearVelocity.x > 0)
        {
            playerSpriteRenderer.sprite = useAlternateSprite ? spriteRightAlt : spriteRight;
            playerSpriteRenderer.flipX = false;
        }
        else if (playerRigidBody.linearVelocity.x < 0)
        {
            playerSpriteRenderer.sprite = useAlternateSprite ? spriteLeftAlt : spriteLeft;
            playerSpriteRenderer.flipX = true;
        }
    }



    private void StartAttack()
    {
        if (isAttacking) return; // Prevent spamming attacks

        isAttacking = true;
        attackTimer = 0f;
        attackFrame = 0;
    }

    private void Attack()
    {

        attackAudioSource.Play();

        // Determine attack direction based on the player's scale
        Vector2 attackDirection = playerSpriteRenderer.flipX ? Vector2.left : Vector2.right;

        // Perform the raycast in the correct direction
        RaycastHit2D hit = Physics2D.Raycast(transform.position, attackDirection, attackRange, breakableLayer);

        Debug.DrawRay(transform.position, attackDirection * attackRange, Color.red, 1f); // Debugging line

        if (hit.collider != null)
        {
            GameObject hitObject = hit.collider.gameObject;
            if (hasSword && hitObject.CompareTag("FinalBoss")) {
                // Deal damage to the Final Boss
                EnemyController boss = hitObject.GetComponent<EnemyController>();
                boss.Die();
            } else if (hitObject.CompareTag("Enemy")) {
                EnemyController enemy = hitObject.GetComponent<EnemyController>();
                enemy.Die();
            } else {
                Box breakable = hit.collider.GetComponent<Box>();
                if (breakable != null) {
                    breakable.TakeHit();
                }
            }
        }
    }


    public void AddKey()
    {
        hasKey = true;
        Debug.Log("Key collected!");
    }

    public void RemoveKey()
    {
        hasKey = false;
        Debug.Log("Key used!");
    }

    public void AddSword()
    {
        hasSword = true;
        Debug.Log("Sword collected!");
    }

    public void RemoveSword()
    {
        hasSword = false;
        Debug.Log("Sword used!");
    }

    public void ReloadScene()
    {
        hasKey = false;
        hasSword = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}