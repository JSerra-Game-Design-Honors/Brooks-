using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Dialog : MonoBehaviour
{
    [System.Serializable]
    public struct SpriteMapping
    {
        public string name;
        public Sprite sprite;
    }

    public TextAsset dialogDataFile;
    public SpriteRenderer spriteLeft;
    public SpriteRenderer spriteRight;
    public TMP_Text nameText;
    public TMP_Text dialogText;
    public List<SpriteMapping> sprites = new List<SpriteMapping>();
    private Dictionary<string, Sprite> imageDic = new Dictionary<string, Sprite>();
    public int dialogIndex;
    private string[] dialogRows;
    public Button next;
    public GameObject optionButton;
    public Transform buttonGroup;
    public string nextScene;

    public GameObject dialogUI; // 引用整个对话UI的根对象

    private void Awake()
    {
        PopulateImageDictionary();
    }

    private void PopulateImageDictionary()
    {
        foreach (var spriteMapping in sprites)
        {
            imageDic[spriteMapping.name] = spriteMapping.sprite;
        }
    }

    void Start()
    {
        spriteLeft.enabled = false;
        spriteRight.enabled = false;
        ReadText(dialogDataFile);
        ShowDialogRow();
    }

    public void UpdateText(string _name, string _text)
    {
        nameText.text = _name;
        dialogText.text = _text;
    }

    public void UpdateImage(string _name, string _position)
    {
        if (imageDic.TryGetValue(_name, out Sprite sprite))
        {
            if (_position == "Left")
            {
                spriteLeft.sprite = sprite;
                spriteLeft.enabled = true;
            }
            else if (_position == "Right")
            {
                spriteRight.sprite = sprite;
                spriteRight.enabled = true;
            }
        }
        else
        {
            Debug.LogError($"Sprite not found for {_name}");
        }
    }

    public void ReadText(TextAsset _textAsset)
    {
        dialogRows = _textAsset.text.Split('\n');
    }

    public void ShowDialogRow()
    {
        if (dialogIndex >= dialogRows.Length)
        {
            Debug.Log("No more dialogs to show.");
            EndDialogue();
            return;
        }

        Debug.Log("Showing dialog row: " + dialogIndex);

        for (int i = 0; i < dialogRows.Length; i++)
        {
            string[] cells = dialogRows[i].Split(',');

            if (cells.Length < 6)
            {
                Debug.LogError("Insufficient data in dialog row: " + dialogRows[i]);
                continue;
            }

            if (cells[0] == "#" && int.Parse(cells[1]) == dialogIndex)
            {
                HandleStandardDialog(cells);
                break;
            }
            else if (cells[0] == "&" && int.Parse(cells[1]) == dialogIndex)
            {
                next.gameObject.SetActive(false);
                GenerateOption(i);
                break; // 确保不掉到标准对话处理程序中
            }
            else if (cells[0] == "End" && int.Parse(cells[1]) == dialogIndex)
            {
                Debug.Log("The End");
                EndDialogue();
                break;
            }
        }
    }

    private void HandleStandardDialog(string[] cells)
    {
        UpdateText(cells[2], cells[4]);
        UpdateImage(cells[2], cells[3]);
        dialogIndex = int.Parse(cells[5]);
        next.gameObject.SetActive(true);
    }

    public void OnClickNext()
    {
        ShowDialogRow();
    }

    public void GenerateOption(int _index)
    {
        string[] cells = dialogRows[_index].Split(',');

        if (cells[0] == "&")
        {
            GameObject button = Instantiate(optionButton, buttonGroup);
            button.GetComponentInChildren<TMP_Text>().text = cells[4];
            int targetDialogId = int.Parse(cells[5]);
            button.GetComponent<Button>().onClick.AddListener(() => OnOptionClick(targetDialogId));
            GenerateOption(_index + 1); // 递归调用以处理可能的连续选项
        }
    }

    public void OnOptionClick(int _id)
    {
        dialogIndex = _id; // 更新对话索引以反映所选选项
        ShowDialogRow(); // 显示新的对话内容
        // 清理之前的选项按钮
        for (int i = 0; i < buttonGroup.childCount; i++)
        {
            Destroy(buttonGroup.GetChild(i).gameObject);
        }
    }

    private void ResetDialogueUI()
    {
        spriteLeft.enabled = false;
        spriteRight.enabled = false;
        nameText.text = "";
        dialogText.text = "";
        // 清理所有选项按钮
        foreach (Transform child in buttonGroup)
        {
            Destroy(child.gameObject);
        }
        next.gameObject.SetActive(true);
    }

    private void EndDialogue()
    {
        Debug.Log("The dialogue has ended.");
        ResetDialogueUI(); // 调用重置UI的方法
        dialogUI.SetActive(false); // 禁用对话系统的根对象
        if (!string.IsNullOrEmpty(nextScene))
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
