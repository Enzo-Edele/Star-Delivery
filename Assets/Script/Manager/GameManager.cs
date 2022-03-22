using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public bool tutorial;
    public int levelUnlock;
    public List<int> highScoreList = new List<int>();
    public List<int> boxScoreList = new List<int>();

    public bool lockPlayer;

    public int percentageBomb;
    public int percentageValid;

    public List<string> validDestination;
    public List<string> validDestinationLevel;
    public List<string> invalidDestination;
    public List<string> invalidDestinationLevel;

    public static GameManager Instance { get; private set; }
    void Awake()
    {
        Instance = this;
        lockPlayer = false;
        Debug.Log("gameManager");
    }

    void Update()
    {
        
    }

    public void SetUpStartValue()
    {
        tutorial = false;
        levelUnlock = 0; //virée tuto et juste check si levelUnlock = 0
        for (int i = 0; i < 7; i++)
        {
            highScoreList.Add(0);
            boxScoreList.Add(0);
        }
    }

    public void Save(int file)
    {
        SaveSysteme.Save(this, file);
    }

    public void Load(int file)
    {
        string path = Application.persistentDataPath + "/data.save";
        if (File.Exists(path))
        {
            SaveData data = SaveSysteme.LoadData(file);
            tutorial = data.tuto;
            levelUnlock = data.levelUnlock;
            for (int i = 0; i < data.highScoreList.Length; i++)
            {
                highScoreList[i] = (data.highScoreList[i]);
            }
            for (int i = 0; i < data.boxScoreList.Length; i++)
            {
                boxScoreList[i] = (data.boxScoreList[i]);
            }
            Debug.Log("load");
        }
    }
}
