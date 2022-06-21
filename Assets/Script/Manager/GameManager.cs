using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using System.IO;


public class GameManager : MonoBehaviour
{
    public int file;
    public float minutes;
    public float seconds;
    public bool tutoDone;
    public bool chronoStart;
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

    public List<Sprite> stickers;
    public List<string> stickersName;
    public Dictionary<string, Sprite> dictionnaryStickers = new Dictionary<string, Sprite>();

    public List<string> validDestination;
    public List<string> validDestinationLevel;
    public List<string> invalidDestination;
    public List<string> invalidDestinationLevel;

    [SerializeField] int packageSent;
    public int score;
    public int[] scoreDtails = new int[8];
    [SerializeField] int objective;
    public int lives;
    public bool canDiffuse;

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
        isDiffusing,
        Pause,
        GameOver
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
            dictionnaryStickers.Add(stickersName[i], stickers[i]); //récup le string via le nom du sprite test avec debug log pour check
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && gameState == GameStates.InGame) 
        {
            UIManager.Instance.ActivatePauseMenu();
            ChangeGameState(GameStates.Pause);
        }
        if (Input.GetKeyDown(KeyCode.L) && gameState == GameStates.InGame)
        {
            InfiniteLives();
        }
        if (Input.GetKeyDown(KeyCode.C))
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
                if (UIManager.Instance != null)
                {
                    UIManager.Instance.DeactivateLives();
                    UIManager.Instance.DeactivateChrono();
                    UIManager.Instance.DeactivateScore();
                    UIManager.Instance.DeactivateFragileWarning();
                    UIManager.Instance.DeactivateDialogue();
                }
                SoundManager.Instance.StopMusic("MusicGame");
                SoundManager.Instance.StopSound("Dradis");
                SoundManager.Instance.PlayMusic("MusicMenu");
                //Debug.Log("InMenu");
                break;
            case GameStates.InGame:
                UnpauseGame();
                Cursor.lockState = CursorLockMode.Locked;
                UIManager.Instance.ActivateLives();
                UIManager.Instance.ActivateChrono();
                UIManager.Instance.ActivateScore();
                SoundManager.Instance.StopMusic("MusicMenu");
                SoundManager.Instance.Play("Dradis");
                SoundManager.Instance.PlayMusic("MusicGame");
                //Debug.Log("InGame");
                break;
            case GameStates.isDiffusing:
                LockPlayer();
                UIManager.Instance.DeactivateLives();
                UIManager.Instance.DeactivateChrono();
                UIManager.Instance.DeactivateScore();
                UIManager.Instance.DeactivateFragileWarning();
                UIManager.Instance.DeactivateDialogue();
                //Debug.Log("isDiffusing");
                break;
            case GameStates.Pause:
                PauseGame();
                Cursor.lockState = CursorLockMode.Confined;
                UIManager.Instance.DactivateIcons();
                UIManager.Instance.DeactivateFragileWarning();
                UIManager.Instance.DeactivateDialogue();
                SoundManager.Instance.PauseMusic("MusicGame");
                SoundManager.Instance.PauseSound("Dradis");
                SoundManager.Instance.PlayMusic("MusicMenu");
                //Debug.Log("Pause");
                break;
            case GameStates.GameOver:
                UnpauseGame();
                LockPlayer();
                Cursor.lockState = CursorLockMode.Locked;
                UIManager.Instance.DactivateIcons();
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

    public void StartLevel(int percentageBomb, int percentageValid, int percentageFragile, int percentageSus, int objective, int lives)
    {
        this.percentageBomb = percentageBomb;
        this.percentageValid = percentageValid;
        this.percentageFragile = percentageFragile;
        this.percentageSus = percentageSus;
        this.objective = objective;
        this.lives = lives;
        chronoStart = true;
        UIManager.Instance.ActivateLives();
        UIManager.Instance.ActivateChrono();
        UIManager.Instance.ActivateScore();
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
        if(lives <= 0)
        {
            packageSent = 0;
            score = 0;
            EndLevel();
        }
        UIManager.Instance.UpdateLives();
    }
    public void UpdateScore(int point, int index)
    {
        score += point;
        if (index >= 0 && index < 8)
        {
            if (index < 5)
                scoreDtails[0] += point;
            else
                scoreDtails[5] += point;
            scoreDtails[index] += point;
        }
        UIManager.Instance.UpadateScore(point);
    }
    public void UpdateScoreDetails(int point, int index)
    {
        if (index >= 0 && index < 8)
        {
            if (index < 5)
                scoreDtails[0] += point;
            else
                scoreDtails[5] += point;
            scoreDtails[index] += point;
        }
    }
    public void SpacecraftDeliver(int qty)
    {
        packageSent += qty;
    }
    void UpdateTotalBoxes()
    {
        totalBoxes = 0;
        for (int i = 0; i < boxScoreList.Count; i++)
            totalBoxes += boxScoreList[i];
    }
    public void ResetValue()
    {
        spacecraft.Clear();
        packageSent = 0;
        score = 0;
        for (int i = 0; i < scoreDtails.Length; i++)
            scoreDtails[i] = 0;
    }
    public void EndLevel()
    {
        for (int i = 0; i < spacecraft.Count; i++)
        {
            spacecraft[i].launchCo = StartCoroutine(spacecraft[i].LaunchCoroutine());
        }
        bool success = false;
        if (levelUnlock > 0)
        {
            if (SceneManager.GetActiveScene().buildIndex == 2)
                if (lives > 0)
                    tutoDone = true;
            if (SceneManager.GetActiveScene().buildIndex - 3 >= 0)
            { //attention ce if permet de mettre un cheat pour changer levelUnlock pendant tuto et permet actualisation des scores
                if (packageSent > boxScoreList[SceneManager.GetActiveScene().buildIndex - 3] && lives > 0) { 
                    boxScoreList[SceneManager.GetActiveScene().buildIndex - 3] = packageSent;
                    UpdateTotalBoxes();
                }
                if(score > highScoreList[SceneManager.GetActiveScene().buildIndex - 3] && lives > 0)
                    highScoreList[SceneManager.GetActiveScene().buildIndex - 3] = score;
            }
        }
        if (!(packageSent < objective) && lives > 0)
        {
            success = true;
            Debug.Log("win");
            if (levelUnlock == SceneManager.GetActiveScene().buildIndex - 2 && !(levelUnlock == SceneManager.GetSceneByName("Lvl7").buildIndex - 2)) {
                levelUnlock++;
                Debug.Log("Unlock level : " + levelUnlock);
            }
        }
        UIManager.Instance.ActivateEndLevel(success);

        ResetValue();
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
        UIManager.Instance.DeactivateChrono();
        UIManager.Instance.DeactivateScore();
        UIManager.Instance.DeactivateIconWalk();
        UIManager.Instance.DeactivateFragileWarning();
        UIManager.Instance.DeactivateDialogue();

        Save(file);

        ResetAverageCheck();
    }

    public void TimeLevel(float timeLevel)
    {
        if (chronoStart)
        {
            minutes = Mathf.Ceil(timeLevel / 60) - 1;
            seconds = Mathf.Ceil(timeLevel - minutes * 60) - 1;
            UIManager.Instance.Chrono();
        }
    }

    //cheat function

    void InfiniteLives()
    {
        lives = 999;
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
