using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using System.IO;


public class GameManager : MonoBehaviour
{
    public bool tutoDone;
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

    int packageSent;
    int objective;

    public static GameManager Instance { get; private set; }
    void Awake()
    {
        Instance = this;
        lockPlayer = false;
    }

    void Update()
    {
        
    }

    public void SetUpStartValue()
    {
        tutoDone = false;
        levelUnlock = 0;
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
        SetUpStartValue();
        string path = Application.persistentDataPath + "/data" + file + ".save";
        if (File.Exists(path))
        {
            SaveData data = SaveSysteme.LoadData(file);
            tutoDone = data.tutoDone;
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

    public void SpacecraftDeliver(int qty)
    {
        packageSent += qty;
    }
    public void EndLevel()
    {
        if(levelUnlock > 0)
            if(packageSent > highScoreList[SceneManager.GetActiveScene().buildIndex - 3])
                highScoreList[SceneManager.GetActiveScene().buildIndex - 3] = packageSent;
        if (!(packageSent < objective) && levelUnlock == highScoreList[SceneManager.GetActiveScene().buildIndex - 2])
            levelUnlock++;
        packageSent = 0;
    }
}
