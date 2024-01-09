using UnityEngine;

public class GlowEffect : MonoBehaviour
{
    private Material material;

    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    void Update()
    {
        // 根据需要调整光照效果
        material.SetColor("_GlowColor", new Color(1, 1, 0, 1)); // 示例：设置发光颜色为黄色
        material.SetFloat("_Glow", 5); // 示例：设置发光强度
    }
}
