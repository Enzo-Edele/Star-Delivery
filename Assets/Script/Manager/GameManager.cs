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
    public int boxesObjective;
    public int totalBoxes;
    public List<int> highScoreList = new List<int>();
    public List<int> boxScoreList = new List<int>();

    public bool lockPlayer;
    public bool gameIsPause;

    public int percentageValid;
    public int percentageBomb;
    public int percentageFragile;
    public int percentageSus;

    public float mouseSensitivity;

    public List<GameObject> stickers;
    public List<string> stickersName;
    public Dictionary<string, GameObject> dictionnaryStickers = new Dictionary<string, GameObject>();

    public List<string> validDestination;
    public List<string> validDestinationLevel;
    public List<string> invalidDestination;
    public List<string> invalidDestinationLevel;

    [SerializeField] int packageSent;
    [SerializeField] int objective;
    public int lives;

    public List<Spacecraft> spacecraft;

    //chack value
    public int valid;
    public int invalid;
    public int fragile;
    public int sus;
    public int normal;
    public int bomb;
    public int total;

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

    private void Start()
    {
        for (int i = 0; i < stickers.Count; i++)
        {
            dictionnaryStickers.Add(stickersName[i], stickers[i]);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown("p") && gameState == GameStates.InGame) {
            UIManager.Instance.ActivatePauseMenu();
            ChangeGameState(GameStates.Pause);
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            packageSent = objective;
            levelUnlock = 7;
            EndLevel();
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
                //Debug.Log("InMenu");
                break;
            case GameStates.InGame:
                UnpauseGame();
                Cursor.lockState = CursorLockMode.Locked;
                UIManager.Instance.ActivateLives();
                //Debug.Log("InGame");
                break;
            case GameStates.Pause:
                PauseGame();
                Cursor.lockState = CursorLockMode.Confined;
                //Debug.Log("Pause");
                break;
            case GameStates.GameOver:
                UnpauseGame();
                LockPlayer();
                Cursor.lockState = CursorLockMode.Locked;
                //Debug.Log("GameOver");
                break;
        }
    }

    public void SetUpStartValue(int file)
    {
        this.file = file;
        tutoDone = false;
        levelUnlock = 1;
        for (int i = 0; i < 7; i++)
        {
            highScoreList.Add(0);
            boxScoreList.Add(0);
        }

    }

    public void Save(int file)
    {
        SaveSysteme.Save(this, file);
        //Debug.Log("save to file : " + file);
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
            totalBoxes = data.totalBoxes;
            for (int i = 0; i < data.highScoreList.Length; i++)
            {
                highScoreList[i] = (data.highScoreList[i]);
            }
            for (int i = 0; i < data.boxScoreList.Length; i++)
            {
                boxScoreList[i] = (data.boxScoreList[i]);
            }
            //Debug.Log("load file : "+ file);
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

    public void StartLevel(int percentageBomb, int percentageValid, int percentageFragile, int percentageSus, int objective)
    {
        this.percentageBomb = percentageBomb;
        this.percentageValid = percentageValid;
        this.percentageFragile = percentageFragile;
        this.percentageSus = percentageSus;
        this.objective = objective;
        this.lives = 3;
        UIManager.Instance.ActivateLives();
        Cursor.lockState = CursorLockMode.Locked;

        invalidDestinationLevel.Clear();
        validDestinationLevel.Clear();
        List<string> memory = new List<string>();
        for (int i = 0; i < spacecraft.Count; i++)
        {
            int rnd = Random.Range(0, validDestination.Count);
            spacecraft[i].spacecraftDestination = validDestination[rnd];
            validDestinationLevel.Add(validDestination[rnd]);
            memory.Add(validDestination[rnd]);
            validDestination.RemoveAt(rnd);
        }

        for (int i = 0; i < invalidDestination.Count; i++)
            invalidDestinationLevel.Add(invalidDestination[i]);
        for (int i = 0; i < validDestination.Count; i++)
            invalidDestinationLevel.Add(validDestination[i]);

        for (int i = 0; i < spacecraft.Count; i++)
            validDestination.Add(memory[i]);
    }
    public void ChangeLife(int damage)
    {
        lives += damage;
        if(lives == 0)
        {
            packageSent = 0;
            EndLevel();
        }
        UIManager.Instance.UpdateLives();
    }
    public void SpacecraftDeliver(int qty)
    {
        packageSent += qty;
    }
    void UpdateTotalBoxes()
    {
        for (int i = 0; i < boxScoreList.Count; i++)
            totalBoxes += boxScoreList[i];
    }
    public void EndLevel()
    {
        bool success = false;
        if (levelUnlock > 0)
        {
            if (SceneManager.GetActiveScene().buildIndex - 3 >= 0)
            { //attention ce if permet de mettre un cheat pour changer levelUnlock pendant tuto et permet actualisation des scores
                if (packageSent > boxScoreList[SceneManager.GetActiveScene().buildIndex - 3]) { 
                    boxScoreList[SceneManager.GetActiveScene().buildIndex - 3] = packageSent;
                    UpdateTotalBoxes();
                    Debug.Log("set highscore to : " + packageSent);
                }
            }
        }
        if (!(packageSent < objective))
        {
            success = true;
            Debug.Log("win");
            if (levelUnlock == SceneManager.GetActiveScene().buildIndex - 2 && !(levelUnlock == SceneManager.GetSceneByName("Lvl7").buildIndex - 2)) {
                levelUnlock++;
                Debug.Log("Unlock level : " + levelUnlock);
            }
        }
        packageSent = 0;

        spacecraft.Clear();
        UIManager.Instance.ActivateEndLevel(success);

        if (!(packageSent < objective))
        {
            if (levelUnlock == SceneManager.GetSceneByName("Lvl7").buildIndex - 2 && boxesObjective < totalBoxes)
            {
                Debug.Log("credit, vous êtes employé du mois vous avez battu glados intensité 5, go get a life now.");
                UIManager.Instance.DeactivateEndLevel();
                UIManager.Instance.ActivateEndGame();
            }
        }

        UIManager.Instance.DeactivateLives();

        Save(file);

        ResetAverageCheck();
    }

    //test function
    public void AverageCheck()
    {
        if(total % 10 == 0)
            Debug.Log("valid : " + valid + " invalid : " + invalid + " fragile : " + fragile + " sus : " + sus + " normal : " + normal + " bomb : " + bomb + " total : " + total);
    }
    public void ResetAverageCheck()
    {
        valid = 0;
        invalid = 0;
        fragile = 0;
        sus = 0;
        normal = 0;
        bomb = 0;
        total = 0;
    }
}
