using UnityEngine;
using MyGameNamespace; // 引用命名空间

public class Coin : MonoBehaviour
{
    public int coinValue = 1; // 金币的值，可以在 Inspector 中修改

    private void Start()
    {
        if (CoinManager.instance != null)
        {
            CoinManager.instance.RegisterCoin();
        }
        else
        {
            Debug.LogError("CoinManager instance is not initialized in Start.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (CoinManager.instance != null)
            {
                CoinManager.instance.CollectCoin();
            }
            else
            {
                Debug.LogError("CoinManager instance is not initialized in OnTriggerEnter2D.");
            }

            if (ScoreManager.instance != null)
            {
                ScoreManager.instance.AddScore(coinValue);
            }
            else
            {
                Debug.LogError("ScoreManager instance is not initialized.");
            }

            Destroy(gameObject);
        }
    }
}
