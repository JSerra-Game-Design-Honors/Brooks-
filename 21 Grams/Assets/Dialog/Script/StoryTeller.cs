using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement; // ���ڳ�������
using System.Collections;
using System.Collections.Generic;

public class StoryTeller : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;       // ������ʾ�ı������
    public Image storyImageDisplay;           // ������ʾ����ͼ������
    public Image fadePanel;                   // ���ڽ���Ч�������
    public List<Sprite> storyImages;          // �洢����ͼ����б�
    public List<string> storyTexts;           // �洢�����ı����б�
    public string nextSceneName;              // ��ת������һ������������
    public float typingSpeed = 0.05f;         // ���ֻ�Ч�����ٶ�
    public float fadeDuration = 1f;           // ����Ч���ĳ���ʱ��

    private int storyIndex = 0;
    private bool isTyping = false;

    void Start()
    {
        storyImageDisplay.sprite = storyImages[storyIndex]; // ��ʼ����һ��ͼƬ
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
                // ���½�������ʼ��ת����һ������
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
        // ��ʼ��������
        yield return StartCoroutine(FadeImage(fadePanel, fadeDuration, new Color(0, 0, 0, 1)));
        // �����л�
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
