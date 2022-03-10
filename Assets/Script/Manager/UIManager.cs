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
            tablet.gameObject.SetActive(true);
        }
        else if (Input.GetKeyDown("a"))
        {
            Cursor.lockState = CursorLockMode.Locked;
            tablet.gameObject.SetActive(false);
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
    public void ActivatePauseMenu()
    {
        pauseMenu.SetActive(true);
    }
    public void DeactivatePauseMenu()
    {
        if (pauseMenu != null)
            pauseMenu.SetActive(false);
    }
    public void ActivateEndLevel()
    {
        endLevel.SetActive(true);
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
        //level select
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        DeactivateNewGameSaveMenu();
    }
    public void ButtonLoad(int file)
    {
        string path = Application.persistentDataPath + "/data" + file + ".save";
        if (File.Exists(path))
        {
            SaveData data = SaveSysteme.LoadData(file);/*
            level = data.level;
            for (int i = 0; i < data.highScoreList.Length; i++)
            {
                HighScoreList[i] = (data.highScoreList[i]);
            }*/
            Debug.Log("Load");
        }
    }
    public void ButtonResume()
    {
        DeactivatePauseMenu();
    }
    public void ButtonMainMenuResume()
    {
        DeactivatePauseMenu();
        ActivateMainMenu();
        //faire quitter niveau en cour
    }
    public void ButtonQuit()
    {
        Application.Quit();
    }
}
