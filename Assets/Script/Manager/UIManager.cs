using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    #region Declaration
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject saveMenu;
    [SerializeField] Text saveText;
    [SerializeField] GameObject newGameButton;
    [SerializeField] GameObject loadButton;
    [SerializeField] List<Button> loadButtons;
    [SerializeField] GameObject levelMenu;
    [SerializeField] List<Button> LevelButtons;
    [SerializeField] List<Text> highScoresBox;
    [SerializeField] List<Text> highScores;
    [SerializeField] List<Vector3> starRequirement;
    [SerializeField] List<Image> starLvl;
    [SerializeField] List<Sprite> starImage;
    [SerializeField] GameObject endLevel;
    [SerializeField] Text endLevelScoreText;
    [SerializeField] GameObject endLevelNextButton;
    [SerializeField] Text endLevelScoreRecap;
    [SerializeField] GameObject endLevelButtons;
    [SerializeField] GameObject retryButton;
    [SerializeField] GameObject endLevelNextLevelButton;
    [SerializeField] GameObject endLevelButtonLevel;
    [SerializeField] GameObject endLevelButtonMain;
    [SerializeField] GameObject endGameButton;

    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject optionMenu;

    [SerializeField] GameObject grabDropIcon;
    [SerializeField] Text grabDropText;
    [SerializeField] GameObject diffuseIcon;
    [SerializeField] Text diffuseText;
    [SerializeField] GameObject walkIcon;
    [SerializeField] Image shiftImage;
    [SerializeField] GameObject fragileWarning;
    [SerializeField] GameObject chrono;
    [SerializeField] TMP_Text chronometerText;
    [SerializeField] GameObject score;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] GameObject LivesDisplay;
    [SerializeField] List<Image> lives;
    [SerializeField] Sprite live, emptylive;
    float timerWarn;
    [SerializeField] int timeWarn;

    bool wasPaused;
    #endregion
    public static UIManager Instance { get; private set; }
    void Awake()
    {
        Instance = this;
        timerWarn = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
            shiftImage.color = Color.blue;
        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
            shiftImage.color = Color.cyan;
        if (timerWarn > 0)
            timerWarn -= Time.deltaTime;
        else if (timerWarn < 0)
        {
            timerWarn = 0;
            DeactivateFragileWarning();
        }
    }

    public void ActivateMainMenu() //main menu
    {
        mainMenu.SetActive(true);
    }
    public void DeactivateMainMenu()
    {
        if (mainMenu != null)
            mainMenu.SetActive(false);
    }
    public void ActivateNewGameMenu() //menu new game
    {
        saveMenu.SetActive(true);
        saveText.text = "New Game";
        newGameButton.SetActive(true);
    }
    public void ActivateSaveMenu() //load menu
    {
        CheckSaveExist();
        saveMenu.SetActive(true);
        saveText.text = "Load";
        loadButton.SetActive(true);
    }
    public void DeactivateNewGameSaveMenu()
    {
        saveMenu.SetActive(false);
        saveText.text = "";
        if (newGameButton != null)
            newGameButton.SetActive(false);
        if (loadButton != null)
            loadButton.SetActive(false);
    }
    public void ActivateLevelMenu() //slect level menu
    {
        levelMenu.SetActive(true);
        ExitCampain();
        CheckCampainProgress();
    }
    public void DeactivateLevelMenu()
    {
        levelMenu.SetActive(false);
    }
    public void ActivatePauseMenu() //pause menu
    {
        pauseMenu.SetActive(true);
    }
    public void DeactivatePauseMenu()
    {
        pauseMenu.SetActive(false);
    }
    public void ActivateOptionMenu() //pause menu
    {
        optionMenu.SetActive(true);
    }
    public void DeactivateOptionMenu()
    {
        optionMenu.SetActive(false);
    }
    public void ActivateIconGrab() //icon grab possible
    {
        grabDropIcon.SetActive(true);
        grabDropText.text = "Grab";
    }
    public void ActivateIconDrop() //icon drop possible
    {
        grabDropIcon.SetActive(true);
        grabDropText.text = "Drop";
    }
    public void ActivateIconPress() //icon press lvl Button possible
    {
        grabDropIcon.SetActive(true);
        grabDropText.text = "Press";
    }
    public void ActivateIconPush() //icon push Bomb Boutton possible
    {
        grabDropIcon.SetActive(true);
        grabDropText.text = "Push";
    }
    public void DeactivateIconGrab()
    {
        grabDropIcon.SetActive(false);
    }
    public void ActivateIconDiffuse() //icon diffues possible
    {
        diffuseIcon.SetActive(true);
        diffuseText.text = "Diffuse";
        GameManager.Instance.canDiffuse = true;
    }
    public void ActivateIconReturn()
    {
        diffuseIcon.SetActive(true);
        diffuseText.text = "return";
    }
    public void DeactivateIconDiffuse()
    {
        diffuseIcon.SetActive(false);
        GameManager.Instance.canDiffuse = false;
    }
    public void ActivateFragileWarning()
    {
        timerWarn = timeWarn;
        fragileWarning.SetActive(true);
    }
    public void DeactivateFragileWarning()
    {
        fragileWarning.SetActive(false);
    }
    public void ActivateIconWalk()
    {
        walkIcon.SetActive(true);
    }
    public void DeactivateIconWalk()
    {
        walkIcon.SetActive(false);
    }
    public void DactivateIcons()
    {
        grabDropIcon.SetActive(false);
        diffuseIcon.SetActive(false);
        walkIcon.SetActive(false);
    }
    public void ActivateLives() //activate livesDisplay
    {
        LivesDisplay.SetActive(true);
        UpdateLives();
    }
    public void DeactivateLives()
    {
        if (LivesDisplay != null)
            LivesDisplay.SetActive(false);
    }
    public void ActivateChrono() //activate chrono
    {
        chrono.SetActive(true);
    }
    public void DeactivateChrono()
    {
        if (chrono != null)
            chrono.SetActive(false);
    }
    public void Chrono()
    {
        chronometerText.text = "" + GameManager.Instance.minutes + " : " + GameManager.Instance.seconds.ToString("00");
    }
    public void ActivateScore() //activate score
    {
        score.SetActive(true);
        scoreText.text = "Score : " + (0 + GameManager.Instance.score);
    }
    public void DeactivateScore()
    {
        if (score != null)
            score.SetActive(false);
    }
    public void UpadateScore()
    {
        scoreText.text = "Score : " + GameManager.Instance.score;
    }

    public void ActivateEndLevel(bool success) //end level menu
    {
        DeactivatePauseMenu();
        DeactivateOptionMenu();
        endLevel.SetActive(true);
        endLevelScoreText.text = "Score : " + GameManager.Instance.score;
        endLevelNextButton.SetActive(true);
        //text recap
        endLevelButtonLevel.SetActive(true);
        endLevelButtonMain.SetActive(true);
        if (success)
            endLevelNextLevelButton.SetActive(true);
        else
            retryButton.SetActive(true);
        endLevelButtons.SetActive(false);
        GameManager.Instance.ChangeGameState(GameManager.GameStates.Pause);
    }
    public void ActivateEndGame() //end level menu for success level 7
    {
        DeactivatePauseMenu();
        DeactivateOptionMenu();
        endLevel.SetActive(true);
        endLevelNextButton.SetActive(true);
        //text recap
        endGameButton.SetActive(true);
        endLevelButtons.SetActive(false);
        GameManager.Instance.ChangeGameState(GameManager.GameStates.Pause);
    }
    public void DeactivateEndLevel()
    {
        endLevel.SetActive(false);
        retryButton.SetActive(false);
        endLevelNextLevelButton.SetActive(false);
        endLevelButtonLevel.SetActive(false);
        endLevelButtonMain.SetActive(false);
        endGameButton.SetActive(false);
    }

    public void UpdateLives()
    {
        for(int i = 0; i < 3; i++)
        {
            if (GameManager.Instance.lives > i)
                lives[i].sprite = live;
            else
                lives[i].sprite = emptylive;
        }
    }

    public void CheckSaveExist() //make button for load interractible if the file already exists
    {
        for (int i = 0; i < loadButtons.Count; i++)
        {
            loadButtons[i].interactable = false;
            string path = Application.persistentDataPath + "/data" + (i + 1) + ".save";
            if (File.Exists(path))
                loadButtons[i].interactable = true;
        }
    }
    public void CheckCampainProgress()
    {
        for (int i = 0; i < LevelButtons.Count; i++)
        {
            if (i < GameManager.Instance.levelUnlock)
                LevelButtons[i].interactable = true;
            highScoresBox[i].text = GameManager.Instance.boxScoreList[i].ToString() + " Packages";
            highScores[i].text = GameManager.Instance.highScoreList[i].ToString() + " Points";
            //if statement to activate star
            if (GameManager.Instance.highScoreList[i] > starRequirement[i].z)
                starLvl[i].sprite = starImage[3];
            else if (GameManager.Instance.highScoreList[i] > starRequirement[i].y)
                starLvl[i].sprite = starImage[2];
            else if (GameManager.Instance.highScoreList[i] > starRequirement[i].x)
                starLvl[i].sprite = starImage[1];
            else
                starLvl[i].sprite = starImage[0];
        }
    }
    void ExitCampain()
    {
        for (int i = 0; i < LevelButtons.Count; i++)
        {
            LevelButtons[i].interactable = false;
            highScoresBox[i].text = GameManager.Instance.boxScoreList[i].ToString();
        }
    }

    public void ButtonNewGameMenu()//main menu -> new game menu
    {
        DeactivateMainMenu();
        ActivateNewGameMenu();
        SoundManager.Instance.Play("Button");
    }
    public void ButtonSaveMenu()//main menu/end level -> load menu
    {
        DeactivateMainMenu();
        DeactivateEndLevel();
        ActivateSaveMenu();
        SoundManager.Instance.Play("Button");
    }
    public void ButtonStartNewGame(int file)//new game menu -> Tuto + create save to file X
    {
        DeactivateNewGameSaveMenu();
        GameManager.Instance.SetUpStartValue(file);
        GameManager.Instance.Save(file);
        ActivateLevelMenu();
        SoundManager.Instance.Play("Button");
    }
    public void ButtonLevelMenu(int file)//load menu -> level select menu of file X OR tuto if tuto not completed
    {
        GameManager.Instance.Load(file);
        DeactivateNewGameSaveMenu();
        ActivateLevelMenu();
        SoundManager.Instance.Play("Button");
    }
    public void ButtonSelectLevel(string level)//level select menu -> level X
    {
        DeactivateLevelMenu();
        SceneManager.LoadScene(level);
        SoundManager.Instance.Play("Button");
        GameManager.Instance.ChangeGameState(GameManager.GameStates.InGame);
    }
    public void ButtonOption(bool paused)//pause menu/main menu -> option
    {
        DeactivateMainMenu();
        DeactivatePauseMenu();
        ActivateOptionMenu();
        wasPaused = paused;
        SoundManager.Instance.Play("Button");
    }
    public void ButtonExitOption()//pause menu/main menu -> option
    {
        DeactivateOptionMenu();
        if (wasPaused)
            ActivatePauseMenu();
        else
            ActivateMainMenu();
        SoundManager.Instance.Play("Button");
    }
    public void ButtonResume()//pause menu -> level
    {
        DeactivatePauseMenu();
        SoundManager.Instance.Play("Button");
        GameManager.Instance.ChangeGameState(GameManager.GameStates.InGame);
    }
    public void ButtonNext()
    {
        endLevelButtons.SetActive(true);
        endLevelNextButton.SetActive(false);
    }
    public void ButtonRetry()
    {
        DeactivateEndLevel();
        SoundManager.Instance.Play("Button");
        GameManager.Instance.ChangeGameState(GameManager.GameStates.InGame);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ButtonNextLevel()//level -> level + 1
    {
        DeactivateEndLevel();
        SoundManager.Instance.Play("Button");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        GameManager.Instance.ChangeGameState(GameManager.GameStates.InGame);
    }
    public void ButtonSelectLevelResume()//pause menu/end level menu -> select level menu
    {
        /*if(pauseMenu.activeSelf == true)
            GameManager.Instance.EndLevel();*/
        DeactivatePauseMenu();
        DeactivateEndLevel();
        ActivateLevelMenu();
        SoundManager.Instance.Play("Button");
        GameManager.Instance.ChangeGameState(GameManager.GameStates.InMenu);
        SceneManager.LoadScene("Main");
    }
    public void ButtonMainMenuResume()//pause menu/end level menu -> main menu
    {
        /*if(pauseMenu.activeSelf == true)
            GameManager.Instance.EndLevel();*/
        DeactivatePauseMenu();
        DeactivateEndLevel();
        ActivateMainMenu();
        SoundManager.Instance.Play("Button");
        GameManager.Instance.ChangeGameState(GameManager.GameStates.InMenu);
        SceneManager.LoadScene("Main");
    }
    public void ButtonEndGame()
    {
        DeactivateEndLevel();
        SoundManager.Instance.Play("Button");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ButtonQuit()//exit app
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void SliderMooseSensitivity(float sensitivity)
    {
        GameManager.Instance.mouseSensitivity = sensitivity;
    }
}
