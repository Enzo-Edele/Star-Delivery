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
    [SerializeField] Button endLevelNextLevelButton;

    [SerializeField] GameObject tablet;

    [SerializeField] GameObject pauseMenu;

    [SerializeField] GameObject grabDropIcon;
    [SerializeField] Text grabDropText;

    [SerializeField] GameObject boxInfo;
    [SerializeField] Text destinationText;
    [SerializeField] Text companyText;
    [SerializeField] Text contentText;

    [SerializeField] TouchPad pad;
    #endregion
    public static UIManager Instance { get; private set; }
    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown("e") && tablet.gameObject.activeSelf == false)
        {
            ActivateTablet();
        }
        else if (Input.GetKeyDown("e") || Input.GetKeyDown(KeyCode.Escape))
        {
            DeactivateTablet();
        }
        if (Input.GetKeyDown("p") && tablet.gameObject.activeSelf == false)
        {
            ActivatePauseMenu();
        }
    }

    public void ActivateTablet()
    {
        Cursor.lockState = CursorLockMode.Confined;
        GameManager.Instance.LockPlayer();
        tablet.gameObject.SetActive(true);
    }
    public void DeactivateTablet()
    {
        Cursor.lockState = CursorLockMode.Locked;
        GameManager.Instance.UnlockPlayer();
        tablet.gameObject.SetActive(false);
    }
    public void ActivateMainMenu()
    {
        Cursor.lockState = CursorLockMode.Confined;
        mainMenu.SetActive(true);
    }
    public void DeactivateMainMenu()
    {
        if (mainMenu != null)
            mainMenu.SetActive(false);
    }
    public void ActivateNewGameMenu()
    {
        Cursor.lockState = CursorLockMode.Confined;
        saveMenu.SetActive(true);
        saveText.text = "New Game";
        newGameButton.SetActive(true);
    }
    public void ActivateSaveMenu()
    {
        Cursor.lockState = CursorLockMode.Confined;
        CheckSaveExist();
        saveMenu.SetActive(true);
        saveText.text = "Load";
        loadButton.SetActive(true);
    }
    public void DeactivateNewGameSaveMenu()
    {
        if (saveMenu != null)
            saveMenu.SetActive(false);
        saveText.text = "";
        if (newGameButton != null)
            newGameButton.SetActive(false);
        if (loadButton != null)
            loadButton.SetActive(false);
    }
    public void ActivateLevelMenu()
    {
        Cursor.lockState = CursorLockMode.Confined;
        levelMenu.SetActive(true);
        ExitCampain();
        CheckCampainProgress();
    }
    public void DeactivateLevelMenu()
    {
        Cursor.lockState = CursorLockMode.Locked;
        if (levelMenu != null)
            levelMenu.SetActive(false);
    }
    public void ActivatePauseMenu()
    {
        pauseMenu.SetActive(true);
        GameManager.Instance.LockPlayer();
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void DeactivatePauseMenu()
    {
        Cursor.lockState = CursorLockMode.Locked;
        if (pauseMenu != null)
            pauseMenu.SetActive(false);
    }
    public void ActivateIconGrab()
    {
        grabDropIcon.SetActive(true);
        grabDropText.text = "Grab";
    }
    public void ActivateIconDrop()
    {
        grabDropIcon.SetActive(true);
        grabDropText.text = "Drop";
    }
    public void DeactivateIconGrab()
    {
        if (grabDropIcon != null)
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
        if (boxInfo != null)
            boxInfo.SetActive(false);
    }
    public void ActivateEndLevel(bool success)
    {
        DeactivateTablet();
        DeactivatePauseMenu();
        pad.EndLevel();
        endLevel.SetActive(true);
        GameManager.Instance.LockPlayer();
        Cursor.lockState = CursorLockMode.Confined;
        endLevelNextLevelButton.interactable = false;
        if(success)
            endLevelNextLevelButton.interactable = true;
    }
    public void DeactivateEndLevel()
    {
        GameManager.Instance.UnlockPlayer();
        Cursor.lockState = CursorLockMode.Locked;
        endLevelNextLevelButton.interactable = true;
        if (endLevel != null)
            endLevel.SetActive(false);
    }
    //variante success and fail

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
            highScores[i].text = GameManager.Instance.highScoreList[i].ToString() + " Packages";
        }
    }
    void ExitCampain()
    {
        for (int i = 0; i < LevelButtons.Count; i++)
        {
            LevelButtons[i].interactable = false;
            highScores[i].text = GameManager.Instance.highScoreList[i].ToString();
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
    }
    public void ButtonLevelMenu(int file)//load menu -> level select menu of file X OR tuto if tuto not completed
    {
        GameManager.Instance.Load(file);
        DeactivateNewGameSaveMenu();
        if (GameManager.Instance.levelUnlock > 0)
            ActivateLevelMenu();
        else
            SceneManager.LoadScene("Tuto");
    }
    public void ButtonSelectLevel(string level)//level select menu -> level X
    {
        DeactivateLevelMenu();
        SceneManager.LoadScene(level);
    }
    public void ButtonResume()//pause menu -> level
    {
        DeactivatePauseMenu();
        GameManager.Instance.UnlockPlayer();
    }
    public void ButtonNextLevel()//level _> level + 1
    {
        DeactivateEndLevel();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);//condition pour last level
        GameManager.Instance.UnlockPlayer(); //David Goodenough
    }
    public void ButtonSelectLevelResume()//pause menu/end level menu -> select level menu
    {
        DeactivatePauseMenu();
        DeactivateEndLevel();
        ActivateLevelMenu();
    }
    public void ButtonMainMenuResume()//pause menu/end level menu -> main menu
    {
        DeactivatePauseMenu();
        DeactivateEndLevel();
        ActivateMainMenu();
        SceneManager.LoadScene("Main");
    }
    public void ButtonQuit()//exit app
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
