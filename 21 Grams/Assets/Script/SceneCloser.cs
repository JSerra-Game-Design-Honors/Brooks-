using UnityEngine;

public class SceneCloser : MonoBehaviour
{
    public void QuitGame()
    {
        // 在编辑器中运行时使用这行代码
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

        // 编译到游戏时使用这行代码
        Application.Quit();
    }
}

