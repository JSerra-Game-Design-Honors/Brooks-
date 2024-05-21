using UnityEngine;

namespace MyGameNamespace
{
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
                DontDestroyOnLoad(gameObject); // 确保 CoinManager 在场景切换时不被销毁
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            // 确保实例已被正确初始化
            if (dialogTrigger == null)
            {
                Debug.LogError("DialogTrigger is not assigned in the CoinManager.");
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
}
