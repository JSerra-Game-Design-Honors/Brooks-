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
        ShowDiaLogRow();
    }

    public void UpdateText(string _name, string _text)
    {
        nameText.text = _name;
        dialogText.text = _text;
    }

    public void UpdateImage(string _name, string _position)
    {
        Sprite sprite = imageDic[_name];
        if (_position == "Left")
        {
            spriteLeft.sprite = sprite;
            spriteLeft.enabled = true; // 显示SpriteRenderer
        }
        else if (_position == "Right")
        {
            spriteRight.sprite = sprite;
            spriteRight.enabled = true; // 显示SpriteRenderer
        }


    }

    public void ReadText(TextAsset _textAsset)
    {
        dialogRows = _textAsset.text.Split('\n');
    }

    public void ShowDiaLogRow()
    {
        for (int i = 0; i < dialogRows.Length; i++)
        {
            string[] cells = dialogRows[i].Split(',');

            if (cells.Length < 6)
                continue; // Ensure we have enough data

            if (cells[0] == "#" && int.Parse(cells[1]) == dialogIndex)
            {
                HandleStandardDialog(cells);
                break;
            }
            else if (cells[0] == "&" && int.Parse(cells[1]) == dialogIndex)
            {
                next.gameObject.SetActive(false);
                GenerateOption(i);
            }
            else if (cells[0] == "End" && int.Parse(cells[1]) == dialogIndex)
            {
                Debug.Log("The End");
                UIManager uIManager = FindAnyObjectByType<UIManager>();
                if (uIManager != null)
                {
                    uIManager.StartGame();
                }
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
        ShowDiaLogRow();
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
            GenerateOption(_index + 1);
        }
    }

    public void OnOptionClick(int _id)
    {
        dialogIndex = _id;
        ShowDiaLogRow();
        for (int i = 0; i < buttonGroup.childCount; i++)
        {
            Destroy(buttonGroup.GetChild(i).gameObject);
        }
    }
}
