using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    
    private Rigidbody2D ratRigidBody;

    private CircleCollider2D circleCollider;

    [SerializeField] private float speed;

    [SerializeField] private float jumpForce;

    [SerializeField] private float dashDistance;

    private float wallJumpCoolDown;

    private float horizontalInput;

    // for dashing
    private bool isDashing;
    private float doubleTapTime;
    KeyCode lastKeyCode;


    // Called each time an instance is loaded
    private void Awake()
    {
        ratRigidBody = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

   
    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        //ratRigidBody.linearVelocity = new Vector2(horizontalInput * speed, ratRigidBody.linearVelocity.y);

        // if(Input.GetKey(KeyCode.Space) && isGrounded())
        // {
        //     ratRigidBody.linearVelocity = new Vector2(ratRigidBody.linearVelocity.x, speed);
        // }

        if (wallJumpCoolDown > 0.2f)
        {
            

            ratRigidBody.linearVelocity = new Vector2(horizontalInput * speed, ratRigidBody.linearVelocity.y);

            if (onWall() && !isGrounded())
            {
                ratRigidBody.gravityScale = 0;
                ratRigidBody.linearVelocity = Vector2.zero;
            }
            else
                ratRigidBody.gravityScale = 20;

                if(Input.GetKey(KeyCode.Space) && isGrounded())
                {
                    Jump();
                }

        }
        else
            wallJumpCoolDown += Time.deltaTime;


        // Dashing left
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (doubleTapTime > Time.time && lastKeyCode == KeyCode.A)
            {
                StartCoroutine(Dash(-1f));
            }
            else
            {
                doubleTapTime = Time.time + 0.5f;
            }
            lastKeyCode = KeyCode.A;
        }

        // Dashing right
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (doubleTapTime > Time.time && lastKeyCode == KeyCode.D)
            {
                StartCoroutine(Dash(1f));
            }
            else
            {
                doubleTapTime = Time.time + 0.5f;
            }
            lastKeyCode = KeyCode.D;
        }


        // Debugging Statements
        print(onWall());
    }

    private void Jump()
    {
        if (isGrounded())
        {
            ratRigidBody.linearVelocity = new Vector2(ratRigidBody.linearVelocity.x, jumpForce);
        }
        else if(onWall() && !isGrounded())
        {
            if (horizontalInput == 0)
            {
                ratRigidBody.linearVelocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                ratRigidBody.linearVelocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
            }
            wallJumpCoolDown = 0;
            
        }
        
    }

    IEnumerator Dash (float direction)
    {
        isDashing = true;
        ratRigidBody.linearVelocity = new Vector2(ratRigidBody.linearVelocity.x, 0f);
        ratRigidBody.AddForce(new Vector2(dashDistance * direction, 0f), ForceMode2D.Impulse);
        float gravity = ratRigidBody.gravityScale;
        ratRigidBody.gravityScale = 0;
        yield return new WaitForSeconds(0.4f);
        

        // End dashing
        isDashing = false;
        ratRigidBody.gravityScale = gravity;
    }

    private bool isGrounded()
    {
        // can add layer mask if need be
        RaycastHit2D raycastHit = Physics2D.BoxCast(circleCollider.bounds.center, circleCollider.bounds.size, 0, Vector2.down, 0.1f);
        return raycastHit.collider != null;

    }

    // Currently not detecting correctly
    private bool onWall()
    {
        // can add layer mask if need be
         RaycastHit2D raycastHit = Physics2D.BoxCast(circleCollider.bounds.center, circleCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f);
         return raycastHit.collider != null;
    }
}
