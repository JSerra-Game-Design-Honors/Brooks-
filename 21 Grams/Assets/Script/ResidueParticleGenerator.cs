using UnityEngine;

public class ResidueParticleGenerator : MonoBehaviour
{
    public ParticleSystem residueParticleSystem; // 将残留粒子系统分配给此字段

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground")) // 检测到角色经过地面
        {
            // 在碰撞点生成残留粒子
            Vector2 collisionPoint = collision.ClosestPoint(transform.position);
            residueParticleSystem.transform.position = collisionPoint;
            residueParticleSystem.Emit(1); // 发射一个粒子
        }
    }
}
