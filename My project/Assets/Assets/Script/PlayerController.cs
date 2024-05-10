using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f; // 角色移动速度
    public float jumpForce = 10.0f; // 跳跃力
    private Rigidbody2D myRigidbody;
    private bool isGrounded; // 是否接触地面
    public Transform groundCheck; // 地面检测点
    public float groundCheckRadius; // 地面检测半径
    public LayerMask groundLayer; // 地面层

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 移动
        float move = Input.GetAxis("Horizontal");
        myRigidbody.velocity = new Vector2(move * speed, myRigidbody.velocity.y);

        // 地面检测
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // 跳跃
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            myRigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    // 在编辑器中显示地面检测区域
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
