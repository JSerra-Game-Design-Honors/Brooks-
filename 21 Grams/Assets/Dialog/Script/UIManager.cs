using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //public GameObject pauseMenu;
    public GameObject mainMenu;
    //public GameObject endMenu;
    public GameObject settingMenu;
    //public GameObject deploymentMenu;
    //public GameObject characterMenu;
    public string gameSceneName;
    public string mainMenuSceneName;
    public string dialogueSceneName;
    //public string victorySceneName;
    //public GameObject editTeamMenu;

    public static UIManager Instance;
    // Remove gameState reference as we will use singleton

    // private bool isGamePaused; removed as we will use GameStateManager
    private void Awake()
    {
        // Singleton pattern
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

    }
    private void Start()
    {
        // Time.timeScale set in GameStateManager
        //pauseMenu.SetActive(false);
        //deploymentMenu.SetActive(false);
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

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    PauseGame();
        //}
        //End();
    }

    public void StartGame()
    {
        GameStateManager.Instance.ChangeGameState(GameStateManager.GameState.InGame);
        SceneManager.LoadScene(gameSceneName);
        //deploymentMenu.SetActive(true);
        mainMenu.SetActive(false);

    }

    public void StartDialogue()
    {
        GameStateManager.Instance.ChangeGameState(GameStateManager.GameState.Dialogue);
        SceneManager.LoadScene(dialogueSceneName);
        //deploymentMenu.SetActive(true);
        mainMenu.SetActive(false);

    }
    public void LeaveGame()
    {
        Application.Quit();
    }

    //public void PauseGame()
    //{
    //    // Use GameStateManager instead of isGamePaused
    //    if (GameStateManager.Instance.currentGameState == GameStateManager.GameState.InGame)
    //    {
    //        GameStateManager.Instance.ChangeGameState(GameStateManager.GameState.Paused);
    //        pauseMenu.SetActive(true);
    //        //deploymentMenu.SetActive(false);
    //    }
    //}

    //public void DeploymentStart()
    //{
    //    if (GameStateManager.Instance.currentGameState == GameStateManager.GameState.Deployment)
    //    {
    //        GameStateManager.Instance.ChangeGameState(GameStateManager.GameState.InGame);
    //        deploymentMenu.SetActive(false);
    //    }
    //}

    //public void EditTeam()
    //{
    //    if (GameStateManager.Instance.currentGameState == GameStateManager.GameState.Deployment)
    //    {
    //        GameStateManager.Instance.ChangeGameState(GameStateManager.GameState.EditTeam);
    //        deploymentMenu.SetActive(false);
    //        editTeamMenu.SetActive(true);
    //    }
    //}

    //public void FinishEdit()
    //{
    //    if (GameStateManager.Instance.currentGameState == GameStateManager.GameState.EditTeam)
    //    {
    //        GameStateManager.Instance.ChangeGameState(GameStateManager.GameState.Deployment);
    //        deploymentMenu.SetActive(true);
    //        editTeamMenu.SetActive(false);
    //    }
        
    //}

    public void ReturnToMainMenu()
    {
        GameStateManager.Instance.ChangeGameState(GameStateManager.GameState.MainMenu);
        SceneManager.LoadScene(mainMenuSceneName);
        //pauseMenu.SetActive(false);
        //endMenu.SetActive(false);
        //deploymentMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    //public void ResumeGame()
    //{
    //    // Use GameStateManager instead of isGamePaused
    //    if (GameStateManager.Instance.currentGameState == GameStateManager.GameState.Paused)
    //    {
    //        GameStateManager.Instance.ChangeGameState(GameStateManager.GameState.InGame);
    //        pauseMenu.SetActive(false);

    //    }
    //}

    public void GoToSetting()
    {
        mainMenu.SetActive(false);
        //pauseMenu.SetActive(false);
        //deploymentMenu.SetActive(false);
        settingMenu.SetActive(true);
    }

    public void CloseSetting()
    {
        //if (GameStateManager.Instance.currentGameState == GameStateManager.GameState.Paused)
        //{
        //    //pauseMenu.SetActive(true);

        //}
        if (GameStateManager.Instance.currentGameState == GameStateManager.GameState.MainMenu)
        {
            mainMenu.SetActive(true);
        }
        //else if (GameStateManager.Instance.currentGameState == GameStateManager.GameState.Deployment)
        //{
        //    //deploymentMenu.SetActive(true);
        //}
        settingMenu.SetActive(false);
    }
    //public void End()
    //{
    //    Scene currentScene = SceneManager.GetActiveScene();
    //    if (GameStateManager.Instance.currentGameState == GameStateManager.GameState.GameOver && currentScene.name == gameSceneName)
    //    {
    //        endMenu.SetActive(true);
    //        pauseMenu.SetActive(false);
    //        //deploymentMenu.SetActive(false);
    //        mainMenu.SetActive(false);
    //        Time.timeScale = 0;
    //    }

    //}
    public void LoadCurrentScene()
    {
        GameStateManager.Instance.ChangeGameState(GameStateManager.GameState.InGame);
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
