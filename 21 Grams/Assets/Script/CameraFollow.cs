using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // 玩家或其他目标的Transform
    public float smoothSpeed = 0.125f; // 平滑移动的速度
    public Vector3 offset; // 相对于玩家的偏移量

    void FixedUpdate()
    {
        Vector3 desiredPosition = new Vector3(target.position.x + offset.x, target.position.y + offset.y, transform.position.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
