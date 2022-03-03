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
    void Start()
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
