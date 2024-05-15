using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingMenu;
    public string gameSceneName;
    public string mainMenuSceneName;
    public string dialogueSceneName;

    public static UIManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Handle scene loaded events if needed
    }

    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == mainMenuSceneName)
        {
            GameStateManager.Instance.ChangeGameState(GameStateManager.GameState.MainMenu);
            mainMenu.SetActive(true);
        }
        else
        {
            mainMenu.SetActive(false);
        }
    }

    public void StartGame()
    {
        GameStateManager.Instance.ChangeGameState(GameStateManager.GameState.InGame);
        SceneManager.LoadScene(gameSceneName);
        mainMenu.SetActive(false);
    }

    public void StartDialogue()
    {
        GameStateManager.Instance.ChangeGameState(GameStateManager.GameState.Dialogue);
        SceneManager.LoadScene(dialogueSceneName);
        mainMenu.SetActive(false);
    }

    public void LeaveGame()
    {
        Application.Quit();
    }

    public void ReturnToMainMenu()
    {
        GameStateManager.Instance.ChangeGameState(GameStateManager.GameState.MainMenu);
        SceneManager.LoadScene(mainMenuSceneName);
        mainMenu.SetActive(true);
    }

    public void GoToSetting()
    {
        mainMenu.SetActive(false);
        settingMenu.SetActive(true);
    }

    public void CloseSetting()
    {
        if (GameStateManager.Instance.currentGameState == GameStateManager.GameState.MainMenu)
        {
            mainMenu.SetActive(true);
        }
        settingMenu.SetActive(false);
    }

    public void CloseDialogUI()
    {
        GameObject dialogUI = GameObject.Find("DialogUI");
        if (dialogUI != null)
        {
            dialogUI.SetActive(false);
        }
        else
        {
            Debug.LogError("DialogUI GameObject not found. Make sure it is named correctly in the scene.");
        }
    }

    public void LoadCurrentScene()
    {
        GameStateManager.Instance.ChangeGameState(GameStateManager.GameState.InGame);
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
