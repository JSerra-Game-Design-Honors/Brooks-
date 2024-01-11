using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement; // 用于场景管理
using System.Collections;
using System.Collections.Generic;

public class StoryTeller : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;       // 用于显示文本的组件
    public Image storyImageDisplay;           // 用于显示故事图像的组件
    public Image fadePanel;                   // 用于渐变效果的面板
    public List<Sprite> storyImages;          // 存储故事图像的列表
    public List<string> storyTexts;           // 存储故事文本的列表
    public string nextSceneName;              // 跳转到的下一个场景的名称
    public float typingSpeed = 0.05f;         // 打字机效果的速度
    public float fadeDuration = 1f;           // 渐变效果的持续时间

    private int storyIndex = 0;
    private bool isTyping = false;

    void Start()
    {
        storyImageDisplay.sprite = storyImages[storyIndex]; // 初始化第一张图片
        StartCoroutine(ShowStory());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isTyping)
        {
            if (storyIndex < storyTexts.Count - 1)
            {
                StartCoroutine(FadeToNextStory());
            }
            else
            {
                // 故事结束，开始跳转到下一个场景
                StartCoroutine(FadeAndSwitchScene());
            }
        }
    }

    IEnumerator ShowStory()
    {
        foreach (char letter in storyTexts[storyIndex].ToCharArray())
        {
            textDisplay.text += letter;
            isTyping = true;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
    }

    IEnumerator FadeToNextStory()
    {
        yield return StartCoroutine(FadeImage(fadePanel, fadeDuration, new Color(0, 0, 0, 1)));
        storyIndex++;
        textDisplay.text = "";
        storyImageDisplay.sprite = storyImages[storyIndex];
        StartCoroutine(ShowStory());
        yield return StartCoroutine(FadeImage(fadePanel, fadeDuration, new Color(0, 0, 0, 0)));
    }

    IEnumerator FadeAndSwitchScene()
    {
        // 开始黑屏渐变
        yield return StartCoroutine(FadeImage(fadePanel, fadeDuration, new Color(0, 0, 0, 1)));
        // 场景切换
        SceneManager.LoadScene(nextSceneName);
    }

    IEnumerator FadeImage(Image image, float duration, Color targetColor)
    {
        Color startColor = image.color;
        for (float t = 0; t <= 1; t += Time.deltaTime / duration)
        {
            image.color = Color.Lerp(startColor, targetColor, t);
            yield return null;
        }
        image.color = targetColor;
    }
}
