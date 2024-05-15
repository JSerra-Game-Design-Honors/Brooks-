using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float dashForce = 10f;
    public float dashDuration = 0.5f;
    public LayerMask groundLayer;

    private bool isJumping = false;
    private bool isDashing = false;
    private bool canDoubleJump = true;
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 dashDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isDashing)
        {
            Move();
        }

        Jump();
        if (isDashing)
        {
            Dash();
        }
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
        if (Input.GetButtonDown("Jump"))
        {
            if (!isJumping)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                isJumping = true;
            }
            else if (canDoubleJump && !isDashing)
            {
                StartCoroutine(EnterDashState());
            }
        }
    }

    IEnumerator EnterDashState()
    {
        isDashing = true;
        rb.velocity = Vector2.zero;  // 停滞
        yield return new WaitForSeconds(0.2f);  // 停滞时间
        Vector2 inputDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        dashDirection = inputDirection == Vector2.zero ? Vector2.right : inputDirection;  // 如果没有输入方向，则默认为向右

        rb.AddForce(dashDirection * dashForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        canDoubleJump = false;  // 禁止再次使用二段跳
    }

    void Dash()
    {
        rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, Time.deltaTime * 2);  // 逐渐减速
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            isJumping = false;
            canDoubleJump = true;  // 重置二段跳
        }
    }
}
