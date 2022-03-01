using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class UIManager : MonoBehaviour
{
    #region Declaration
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject saveMenu;
    [SerializeField] Text saveText;
    [SerializeField] GameObject newGameButton;
    [SerializeField] GameObject loadButton;

    [SerializeField] GameObject pauseMenu;

    [SerializeField] GameObject grabDropIcon;
    [SerializeField] Text grabDropText;
    #endregion
    public static UIManager Instance { get; private set; }
    void Start()
    {
        Instance = this;
    }

    void Update()
    {

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

    public void ButtonNewGame(int file)
    {

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
    public void ButtonQuit()
    {
        Application.Quit();
    }
}
