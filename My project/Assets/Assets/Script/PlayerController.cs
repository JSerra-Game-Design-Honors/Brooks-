using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f; // 控制移动速度
    public float jumpForce = 500.0f; // 控制跳跃力度
    private Rigidbody2D myRigidbody;
    private bool isGrounded; // 检查角色是否在地面上
    public Transform groundCheck; // 地面检查点的位置
    public float groundCheckRadius; // 地面检查的半径
    public LayerMask whatIsGround; // 定义哪一层是地面

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 地面检查，确保角色可以跳跃
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        // 角色移动
        float moveHorizontal = Input.GetAxis("Horizontal");
        myRigidbody.velocity = new Vector2(moveHorizontal * speed, myRigidbody.velocity.y);

        // 角色跳跃
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            myRigidbody.AddForce(new Vector2(0, jumpForce));
        }
    }

    // 如果需要，可以添加额外的方法来处理碰撞和触发事件
}