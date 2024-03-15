using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // 角色的Transform
    public float smoothing = 5f; // 摄像机跟随的平滑度
    Vector3 offset; // 摄像机与角色之间的偏移量

    void Start()
    {
        offset = transform.position - target.position;
    }

    void FixedUpdate()
    {
        Vector3 targetCamPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}