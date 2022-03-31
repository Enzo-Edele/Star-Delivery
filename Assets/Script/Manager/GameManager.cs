using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using System.IO;


public class GameManager : MonoBehaviour
{
    public int file;
    public bool tutoDone;
    public int levelUnlock;
    public List<int> highScoreList = new List<int>();
    public List<int> boxScoreList = new List<int>();

    public bool lockPlayer;
    public bool gameIsPause;

    public int percentageBomb;
    public int percentageValid;

    public float mouseSensitivity;

    public List<string> validDestination;
    public List<string> validDestinationLevel;
    public List<string> invalidDestination;
    public List<string> invalidDestinationLevel;

    [SerializeField] int packageSent;
    [SerializeField] int objective;

    public enum GameStates
    {
        InMenu,
        InGame,
        Pause,
        GameOver,
    }
    private static GameStates gameState;
    public static GameStates GameState;

    public static GameManager Instance { get; private set; }
    void Awake()
    {
        Instance = this;
        ChangeGameState(GameStates.InMenu);
    }

    private void Update()
    {
        if (Input.GetKeyDown("p") && gameState == GameStates.InGame) {
            UIManager.Instance.ActivatePauseMenu();
            ChangeGameState(GameStates.Pause);
        }
    }

    public void ChangeGameState(GameStates currentState)
    {
        gameState = currentState;
        GameState = gameState;
        switch (gameState)
        {
            case GameStates.InMenu:
                UnpauseGame();
                LockPlayer();
                Cursor.lockState = CursorLockMode.Confined;
                Debug.Log("InMenu");
                break;
            case GameStates.InGame:
                UnpauseGame();
                Cursor.lockState = CursorLockMode.Locked;
                Debug.Log("InGame");
                break;
            case GameStates.Pause:
                PauseGame();
                Cursor.lockState = CursorLockMode.Confined;
                Debug.Log("Pause");
                break;
            case GameStates.GameOver:
                UnpauseGame();
                LockPlayer();
                Cursor.lockState = CursorLockMode.Locked;
                Debug.Log("GameOver");
                break;
        }
    }

    public void SetUpStartValue(int file)
    {
        this.file = file;
        tutoDone = false;
        levelUnlock = 0;
        for (int i = 0; i < 7; i++)
        {
            highScoreList.Add(0);
            boxScoreList.Add(0);
        }

    }

    public void Save(int file)
    {
        SaveSysteme.Save(this, file);
        Debug.Log("save to file : " + file);
    }

    public void Load(int file)
    {
        SetUpStartValue(0);
        string path = Application.persistentDataPath + "/data" + file + ".save";
        if (File.Exists(path))
        {
            SaveData data = SaveSysteme.LoadData(file);
            this.file = data.file;
            tutoDone = data.tutoDone;
            levelUnlock = data.levelUnlock;
            for (int i = 0; i < data.highScoreList.Length; i++)
            {
                highScoreList[i] = (data.highScoreList[i]);
            }
            for (int i = 0; i < data.boxScoreList.Length; i++)
            {
                boxScoreList[i] = (data.boxScoreList[i]);
            }
            Debug.Log("load file : "+ file);
        }
    }

    public void LockPlayer()
    {
        lockPlayer = true;
    }
    public void UnlockPlayer()
    {
        lockPlayer = false;
    }
    public void PauseGame()
    {
        lockPlayer = true;
        gameIsPause = true;
    }
    public void UnpauseGame()
    {
        lockPlayer = false;
        gameIsPause = false;
    }

    public void StartLevel(int percentageBomb, int percentageValid, int objective)
    {
        this.percentageBomb = percentageBomb;
        this.percentageValid = percentageValid;
        this.objective = objective;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void SpacecraftDeliver(int qty)
    {
        packageSent += qty;
    }
    public void EndLevel()
    {
        bool success = false;
        if (levelUnlock > 0)
        {
            if (packageSent > highScoreList[SceneManager.GetActiveScene().buildIndex - 3]) {
                highScoreList[SceneManager.GetActiveScene().buildIndex - 3] = packageSent;
                Debug.Log("set highscore to : " + packageSent);
            }
        }
        if (!(packageSent < objective) && levelUnlock == SceneManager.GetActiveScene().buildIndex - 2)
        {
            levelUnlock++;
            success = true;
            Debug.Log("win");
        }
        else if (packageSent < objective)
            Debug.Log("fail");
        packageSent = 0;
        UIManager.Instance.ActivateEndLevel(success);
        Save(file);
    }
}
