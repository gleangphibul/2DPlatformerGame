using System.Collections;
using TMPro;
using UnityEditor.Callbacks;
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

    // for jumping

    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private LayerMask wallLayer;

    // for dashing
    private bool canDash = true;
    private bool isDashing;

    [SerializeField] private float dashingPower = 24f;

    private float dashingTime = 0.2f;

    private float dashingCooldown = 1f;

    [SerializeField] private TrailRenderer tr;


    // Called each time an instance is loaded
    private void Awake()
    {
        ratRigidBody = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

   
    // Update is called once per frame
    void Update()
    {

        if (isDashing)
        {
            return;
        }

        horizontalInput = Input.GetAxis("Horizontal");
        //ratRigidBody.linearVelocity = new Vector2(horizontalInput * speed, ratRigidBody.linearVelocity.y);

        // if(Input.GetKey(KeyCode.Space) && isGrounded())
        // {
        //     ratRigidBody.linearVelocity = new Vector2(ratRigidBody.linearVelocity.x, speed);
        // }

        // Wall Jump Logic
        if (wallJumpCoolDown > 0.2f)
        {

            ratRigidBody.linearVelocity = new Vector2(horizontalInput * speed, ratRigidBody.linearVelocity.y);

            if (onWall() && !isGrounded())
            {
                ratRigidBody.gravityScale = 0;
                ratRigidBody.linearVelocity = Vector2.zero;
            }
            else
                ratRigidBody.gravityScale = 10;

            if(Input.GetKey(KeyCode.Space))
            {
                Jump();
            }

        }
        else
            wallJumpCoolDown += Time.deltaTime;


        // Dashing logic
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
        


        // Debugging Statements
        //print(onWall());
        //print(isGrounded());
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        ratRigidBody.linearVelocity = new Vector2(horizontalInput * speed, ratRigidBody.linearVelocity.y);
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

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = ratRigidBody.gravityScale;
        ratRigidBody.gravityScale = 0f;
        if (ratRigidBody.linearVelocityX < 0)
        {
            ratRigidBody.linearVelocity = new Vector2(-transform.localScale.x * dashingPower, 0f);
        }
        else
        {
            ratRigidBody.linearVelocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        }
    
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        ratRigidBody.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    private bool isGrounded()
    {
        // can add layer mask if need be
        RaycastHit2D raycastHit = Physics2D.BoxCast(circleCollider.bounds.center, circleCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        //Debug.Log(raycastHit.collider.gameObject);
        return raycastHit.collider != null;

    }

    // Currently not detecting correctly
    private bool onWall()
    {
        // can add layer mask if need be
         RaycastHit2D raycastHit = Physics2D.BoxCast(circleCollider.bounds.center, circleCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
         return raycastHit.collider != null;
    }
}
