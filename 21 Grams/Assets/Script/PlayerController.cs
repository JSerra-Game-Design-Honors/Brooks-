using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float wallJumpForce = 10f;
    public float wallCheckDistance = 0.5f;
    public LayerMask wallLayer;
    public LayerMask groundLayer; // 添加地面LayerMask

    private bool isJumping = false;
    private Rigidbody2D rb;
    private Animator animator;

    private static readonly float Angle45InRad = 45f * Mathf.Deg2Rad; // 45度的弧度值

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        Jump();
        WallJump();
        WallSlide();
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);

        animator.SetBool("WalkRight", moveX > 0);
        animator.SetBool("WalkLeft", moveX < 0);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumping = true;
        }
    }

    void WallJump()
    {
        if (IsTouchingWall() && Input.GetButtonDown("Jump"))
        {
            Vector2 direction = new Vector2(Mathf.Cos(Angle45InRad), Mathf.Sin(Angle45InRad));
            Vector2 jumpDirection = (IsTouchingLeftWall() ? direction : new Vector2(-direction.x, direction.y));
            rb.velocity = jumpDirection * wallJumpForce;
            isJumping = false;
        }
    }

    void WallSlide()
    {
        if (IsTouchingWall() && !isJumping && rb.velocity.y < 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -0.5f, float.MaxValue));
        }
    }

    private bool IsTouchingWall()
    {
        return IsTouchingDirection(Vector2.left) || IsTouchingDirection(Vector2.right);
    }

    private bool IsTouchingLeftWall()
    {
        return IsTouchingDirection(Vector2.left);
    }

    private bool IsTouchingRightWall()
    {
        return IsTouchingDirection(Vector2.right);
    }

    private bool IsTouchingDirection(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, wallCheckDistance, wallLayer);
        return hit.collider != null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0) // 使用LayerMask检测地面
        {
            isJumping = false;
        }
    }
}
