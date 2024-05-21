using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;
    public int totalCoins;
    private int collectedCoins;

    public DialogTrigger dialogTrigger; // 引用 DialogTrigger 脚本

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterCoin()
    {
        totalCoins++;
    }

    public void CollectCoin()
    {
        collectedCoins++;
        if (collectedCoins >= totalCoins)
        {
            dialogTrigger.ShowDialog();
        }
    }
}
