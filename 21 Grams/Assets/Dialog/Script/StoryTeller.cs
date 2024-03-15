using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class StoryTeller : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public Image storyImageDisplay;
    public Image fadePanel;
    public List<Sprite> storyImages;
    public List<string> storyTexts;
    public string nextSceneName;
    public float typingSpeed = 0.05f;
    public float fadeDuration = 1f;

    private int storyIndex = 0;
    private bool isTyping = false;

    void Start()
    {
        if (storyImages.Count > 0 && storyIndex < storyImages.Count)
        {
            storyImageDisplay.sprite = storyImages[storyIndex];
        }
        StartCoroutine(TypeStoryText());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isTyping)
        {
            if (storyIndex < storyTexts.Count - 1)
            {
                storyIndex++;
                StartCoroutine(ChangeStory());
            }
            else
            {
                StartCoroutine(FadeOutAndSwitchScene());
            }
        }
    }

    IEnumerator TypeStoryText()
    {
        if (storyIndex < storyTexts.Count)
        {
            textDisplay.text = "";
            foreach (char letter in storyTexts[storyIndex].ToCharArray())
            {
                textDisplay.text += letter;
                isTyping = true;
                yield return new WaitForSeconds(typingSpeed);
            }
            isTyping = false;
        }
    }

    IEnumerator ChangeStory()
    {
        yield return Fade(1); // Fade to black
        if (storyIndex < storyImages.Count)
        {
            storyImageDisplay.sprite = storyImages[storyIndex];
        }
        StartCoroutine(TypeStoryText());
        yield return Fade(0); // Fade back in
    }

    IEnumerator FadeOutAndSwitchScene()
    {
        yield return Fade(1);
        SceneManager.LoadScene(nextSceneName);
    }

    IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = fadePanel.color.a;
        float timer = 0;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, timer / fadeDuration);
            fadePanel.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        fadePanel.color = new Color(0, 0, 0, targetAlpha);
    }
}
