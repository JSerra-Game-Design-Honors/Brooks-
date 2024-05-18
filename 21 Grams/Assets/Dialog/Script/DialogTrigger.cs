using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public GameObject dialogObject; // 对话对象

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ShowDialog();
        }
    }

    void ShowDialog()
    {
        if (dialogObject != null)
        {
            dialogObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HideDialog();
        }
    }

    void HideDialog()
    {
        if (dialogObject != null)
        {
            dialogObject.SetActive(false);
        }
    }
}
