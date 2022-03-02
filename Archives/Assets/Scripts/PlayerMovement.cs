using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private BoxCollider2D coll;

    [SerializeField] private LayerMask jumpableGround;

    private int playerSpeed = 10;
    private int playerJumpPower = 1450;
    private bool facingRight = false;
    private float moveX;
    private int jumpsLeft = 2;

    private bool isDashing = false;
    private float dashDistance = 3.5f;
    KeyCode lastKeyCode;
    float doubleTapTime;
    private float dashCoolDownTime = 2f;
    private float nextDashTime = 0;

    private enum MovementState
    {
        idle, running, jumping, falling, doublejumping
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDashing)
        {
            PlayerMove();
        }
    }

    void PlayerMove()
    {
        // Controls
        moveX = Input.GetAxisRaw("Horizontal");

        if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow)) && (IsGrounded() || jumpsLeft > 0))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(Time.time > nextDashTime)
            {
                if (doubleTapTime > Time.time && lastKeyCode == KeyCode.RightArrow)
                {
                    StartCoroutine(Dash(-1f));
                    nextDashTime = Time.time + dashCoolDownTime;
                }
                else
                {
                    doubleTapTime = Time.time + 0.3f;
                }
            }

            lastKeyCode = KeyCode.RightArrow;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (Time.time > nextDashTime)
            {
                if (doubleTapTime > Time.time && lastKeyCode == KeyCode.LeftArrow)
                {
                    StartCoroutine(Dash(1f));
                    nextDashTime = Time.time + dashCoolDownTime;
                }
                else
                {
                    doubleTapTime = Time.time + 0.3f;
                }
            }
            lastKeyCode = KeyCode.LeftArrow;
        }


        // Direction
        UpdateAnimationState();

        // Physics
        if (isDashing)
        {
            rb.velocity = new Vector2(moveX * playerSpeed * dashDistance, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(moveX * playerSpeed, rb.velocity.y);
        }
        
    }

    IEnumerator Dash (float direction)
    {
        isDashing = true;
        rb.velocity = new Vector2(rb.velocity.x, 0);
        float gravity = rb.gravityScale;
        rb.gravityScale = 0;
        yield return new WaitForSeconds(0.15f);
        isDashing = false;
        rb.gravityScale = gravity;
    }

    void Jump()
    {
        if (jumpsLeft == 2)
        {
            rb.AddForce(Vector2.up * playerJumpPower);
        }
        else if (jumpsLeft == 1)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce((Vector2.up * 15f), ForceMode2D.Impulse);
        }
        jumpsLeft--;
    }

    void UpdateAnimationState()
    {
        MovementState state = MovementState.idle;

        if (moveX < 0.0f)
        {
            if (!facingRight)
            {
                FlipPlayer();
                state = MovementState.running;
            }
            else
            {
                state = MovementState.running;
            }
        }
        else if (moveX > 0.0f)
        {
            if (facingRight)
            {
                FlipPlayer();
                state = MovementState.running;
            }
            else
            {
                state = MovementState.running;
            }
        }
        else
        {
            state = MovementState.idle;
        }

        if(rb.velocity.y > 0.1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -0.1f)
        {
            state = MovementState.falling;
        }

        if (isDashing)
        {
            state = MovementState.jumping;
        }

        anim.SetInteger("MovementState", (int)state);
    }


    void FlipPlayer()
    {
        facingRight = !facingRight;
        Vector2 localScale = gameObject.transform.localScale;

        localScale.x *= -1;
        transform.localScale = localScale;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Player has collided with " + collision.collider.name);
    }

    private bool IsGrounded()
    {
        if (Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround))
        {
            jumpsLeft = 2;
            return true;
        }
        else return false;
  
        //return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }
}
