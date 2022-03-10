using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public CinemachineVirtualCamera cam;

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
    }

    void Update()
    {
        
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
            SaveData data = SaveSysteme.LoadData(file);/*
            level = data.level;
            for (int i = 0; i < data.highScoreList.Length; i++)
            {
                HighScoreList[i] = (data.highScoreList[i]);
            }*/
        }
    }
}
