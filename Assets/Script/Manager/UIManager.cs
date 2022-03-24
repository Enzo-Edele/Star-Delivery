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
    [SerializeField] GameObject levelMenu;
    [SerializeField] List<Button> LevelButtons;
    [SerializeField] List<Text> highScores;
    [SerializeField] GameObject endLevel;

    [SerializeField] GameObject tablet;

    [SerializeField] GameObject pauseMenu;

    [SerializeField] GameObject grabDropIcon;
    [SerializeField] Text grabDropText;

    [SerializeField] GameObject boxInfo;
    [SerializeField] Text destinationText;
    [SerializeField] Text companyText;
    [SerializeField] Text contentText;

    #endregion
    public static UIManager Instance { get; private set; }
    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown("a") && tablet.gameObject.activeSelf == false)
        {
            Cursor.lockState = CursorLockMode.Confined;
            GameManager.Instance.lockPlayer = true;
            tablet.gameObject.SetActive(true);
        }
        else if (Input.GetKeyDown("a"))
        {
            Cursor.lockState = CursorLockMode.Locked;
            GameManager.Instance.lockPlayer = false;
            tablet.gameObject.SetActive(false);
        }
        if (Input.GetKeyDown("p") && tablet.gameObject.activeSelf == false)
        {
            ActivatePauseMenu();
        }
    }
    public void ActivateMainMenu()
    {
        mainMenu.SetActive(true);
    }
    public void DeactivateMainMenu()
    {
        if (mainMenu != null)
            mainMenu.SetActive(false);
    }
    public void ActivateNewGameMenu()
    {
        saveMenu.SetActive(true);
        saveText.text = "New Game";
        newGameButton.SetActive(true);
    }
    public void ActivateSaveMenu()
    {
        //if tuto fait
        saveMenu.SetActive(true);
        saveText.text = "Load";
        loadButton.SetActive(true);
        //else on load tuto
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
    public void ActivatePauseMenu()
    {
        pauseMenu.SetActive(true);
        GameManager.Instance.lockPlayer = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void DeactivatePauseMenu()
    {
        if (pauseMenu != null)
            pauseMenu.SetActive(false);
    }
    public void ActivateLevelMenu()
    {
        levelMenu.SetActive(true);
        CheckCampainProgress();
    }
    public void DeactivateLevelMenu()
    {
        if (levelMenu != null)
            levelMenu.SetActive(false);
    }
    public void ActivateEndLevel()
    {
        endLevel.SetActive(true);
        GameManager.Instance.lockPlayer = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void DeactivateEndLevel()
    {
        if (endLevel != null)
            endLevel.SetActive(false);
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

    void CheckCampainProgress()
    {
        //loop des High Score & bouton interractible
    }
    void ExitCampain()
    {
        for (int i = 0; i < LevelButtons.Count; i++)
        {
            if (i < GameManager.Instance.levelUnlock)
                LevelButtons[i].interactable = true;
            highScores[i].text = GameManager.Instance.highScoreList[i].ToString();
        }
    }

    public void ButtonNewGameMenu()
    {
        DeactivateMainMenu();
        ActivateNewGameMenu();
    }
    public void ButtonLoadMenu()
    {
        DeactivateMainMenu();
        DeactivateEndLevel();
        ActivateSaveMenu();
    }
    public void ButtonStartNewGame(int file)
    {
        DeactivateNewGameSaveMenu();
        GameManager.Instance.SetUpStartValue();
        GameManager.Instance.Save(file);
        SceneManager.LoadScene("Tuto");
    }
    public void ButtonLoad(int file)
    {
        GameManager.Instance.Load(file);
        DeactivateNewGameSaveMenu();
        ActivateLevelMenu();
    }
    public void ButtonSelectLevel(string level)
    {
        DeactivateLevelMenu();
        SceneManager.LoadScene(level);
    }
    public void ButtonResume()
    {
        DeactivatePauseMenu();
        GameManager.Instance.lockPlayer = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void ButtonLevelSelect()
    {
        DeactivatePauseMenu();
        ActivateLevelMenu();
    }
    public void ButtonNextLevel()
    {
        DeactivateEndLevel();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ButtonMainMenuResume()
    {
        DeactivatePauseMenu();
        ActivateMainMenu();
        SceneManager.LoadScene("Main");
    }
    public void ButtonQuit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
