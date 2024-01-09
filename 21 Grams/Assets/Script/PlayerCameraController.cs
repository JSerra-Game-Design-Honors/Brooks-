using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    public Transform target; // 角色或其他要跟随的目标
    public float smoothSpeed = 0.125f; // 平滑移动速度
    public Vector3 offset; // 相对于目标的偏移量

    public bool bounds; // 是否限制摄像机移动范围
    public Vector2 minCameraPos; // 摄像机允许的最小位置
    public Vector2 maxCameraPos; // 摄像机允许的最大位置

    public Transform[] backgrounds; // 用于视差效果的背景
    private float[] parallaxScales;
    private Vector3 previousCamPos;

    void Start()
    {
        previousCamPos = transform.position;
        parallaxScales = new float[backgrounds.Length];

        for (int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z * -1;
        }
    }

    void FixedUpdate()
    {
        // 摄像机跟随
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        smoothedPosition.z = transform.position.z; // 保持Z轴不变
        transform.position = smoothedPosition;

        if (bounds)
        {
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, minCameraPos.x, maxCameraPos.x),
                Mathf.Clamp(transform.position.y, minCameraPos.y, maxCameraPos.y),
                transform.position.z
            );
        }

        // 视差效果
        for (int i = 0; i < backgrounds.Length; i++)
        {
            float parallax = (previousCamPos.x - transform.position.x) * parallaxScales[i];
            float backgroundTargetPosX = backgrounds[i].position.x + parallax;
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothSpeed);
        }

        previousCamPos = transform.position;
    }
}
