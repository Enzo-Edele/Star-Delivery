using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

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
    [SerializeField] List<Text> highScores;
    [SerializeField] GameObject endLevel;
    [SerializeField] GameObject retryButton;
    [SerializeField] GameObject endLevelNextLevelButton;
    [SerializeField] GameObject endLevelButtonLevel;
    [SerializeField] GameObject endLevelButtonMain;
    [SerializeField] GameObject endGameButton;

    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject optionMenu;

    [SerializeField] GameObject grabDropIcon;
    [SerializeField] Text grabDropText;

    [SerializeField] GameObject boxInfo;
    [SerializeField] Text destinationText;
    [SerializeField] Text companyText;
    [SerializeField] Text contentText;

    bool wasPaused;
    #endregion
    public static UIManager Instance { get; private set; }
    void Awake()
    {
        Instance = this;
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
    public void DeactivateIconGrab()
    {
        grabDropIcon.SetActive(false);
    }

    public void ActivateBoxInfo(string destination, string company, string content)
    {
        boxInfo.SetActive(true);
        destinationText.text = destination;
        companyText.text = company;
        contentText.text = content;
    }
    public void DeactivateBoxInfo()
    {
        boxInfo.SetActive(false);
    }
    public void ActivateEndLevel(bool success) //end level menu
    {
        DeactivatePauseMenu();
        DeactivateOptionMenu();
        endLevel.SetActive(true);
        endLevelButtonLevel.SetActive(true);
        endLevelButtonMain.SetActive(true);
        if (success)
            endLevelNextLevelButton.SetActive(true);
        else
            retryButton.SetActive(true);
        GameManager.Instance.ChangeGameState(GameManager.GameStates.Pause);
    }
    public void ActivateEndGame()
    {
        DeactivatePauseMenu();
        DeactivateOptionMenu();
        endLevel.SetActive(true);
        endGameButton.SetActive(true);
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

    public void CheckSaveExist()
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
            highScores[i].text = GameManager.Instance.boxScoreList[i].ToString() + " Packages";
        }
    }
    void ExitCampain()
    {
        for (int i = 0; i < LevelButtons.Count; i++)
        {
            LevelButtons[i].interactable = false;
            highScores[i].text = GameManager.Instance.boxScoreList[i].ToString();
        }
    }

    public void ButtonNewGameMenu()//main menu -> new game menu
    {
        DeactivateMainMenu();
        ActivateNewGameMenu();
    }
    public void ButtonSaveMenu()//main menu/end level -> load menu
    {
        DeactivateMainMenu();
        DeactivateEndLevel();
        ActivateSaveMenu();
    }
    public void ButtonStartNewGame(int file)//new game menu -> Tuto + create save to file X
    {
        DeactivateNewGameSaveMenu();
        GameManager.Instance.SetUpStartValue(file);
        GameManager.Instance.Save(file);
        SceneManager.LoadScene("Tuto");
        GameManager.Instance.ChangeGameState(GameManager.GameStates.InGame);
    }
    public void ButtonLevelMenu(int file)//load menu -> level select menu of file X OR tuto if tuto not completed
    {
        GameManager.Instance.Load(file);
        DeactivateNewGameSaveMenu();
        if (GameManager.Instance.levelUnlock > 0)
            ActivateLevelMenu();
        else
        {
            SceneManager.LoadScene("Tuto");
            GameManager.Instance.ChangeGameState(GameManager.GameStates.InGame);
        }
    }
    public void ButtonSelectLevel(string level)//level select menu -> level X
    {
        DeactivateLevelMenu();
        SceneManager.LoadScene(level);
        GameManager.Instance.ChangeGameState(GameManager.GameStates.InGame);
    }
    public void ButtonOption(bool paused)//pause menu/main menu -> option
    {
        DeactivateMainMenu();
        DeactivatePauseMenu();
        ActivateOptionMenu();
        wasPaused = paused;
    }
    public void ButtonExitOption()//pause menu/main menu -> option
    {
        DeactivateOptionMenu();
        if (wasPaused)
            ActivatePauseMenu();
        else
            ActivateMainMenu();
    }
    public void ButtonResume()//pause menu -> level
    {
        DeactivatePauseMenu();
        GameManager.Instance.ChangeGameState(GameManager.GameStates.InGame);
    }
    public void ButtonRetry()
    {
        DeactivateEndLevel();
        GameManager.Instance.ChangeGameState(GameManager.GameStates.InGame);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ButtonNextLevel()//level -> level + 1
    {
        DeactivateEndLevel();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        GameManager.Instance.ChangeGameState(GameManager.GameStates.InGame); //David Goodenough
    }
    public void ButtonSelectLevelResume()//pause menu/end level menu -> select level menu
    {
        if(pauseMenu.activeSelf == true)
            GameManager.Instance.EndLevel();
        DeactivatePauseMenu();
        DeactivateEndLevel();
        ActivateLevelMenu();
        GameManager.Instance.ChangeGameState(GameManager.GameStates.InMenu);
        SceneManager.LoadScene("Main");
    }
    public void ButtonMainMenuResume()//pause menu/end level menu -> main menu
    {
        if (pauseMenu.activeSelf == true)
            GameManager.Instance.EndLevel();
        DeactivatePauseMenu();
        DeactivateEndLevel();
        ActivateMainMenu();
        GameManager.Instance.ChangeGameState(GameManager.GameStates.InMenu);
        SceneManager.LoadScene("Main");
    }
    public void ButtonEndGame()
    {
        DeactivateEndLevel();
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
