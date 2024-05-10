using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string sceneNameToLoad; // 你想要加载的场景名称

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // 假设玩家对象有一个"Player"的标签
        {
            SceneManager.LoadScene(sceneNameToLoad); // 加载场景
        }
    }
}
