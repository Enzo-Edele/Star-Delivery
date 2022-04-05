using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int file;
    public bool tutoDone;
    public int levelUnlock;
    public int totalBoxes;
    public int[] highScoreList = new int[GameManager.Instance.highScoreList.Count];
    public int[] boxScoreList = new int[GameManager.Instance.boxScoreList.Count];


    public SaveData(GameManager gameData)
    {
        file = gameData.file;
        tutoDone = gameData.tutoDone;
        levelUnlock = gameData.levelUnlock;
        totalBoxes = gameData.totalBoxes;
        for (int i = 0; i < gameData.highScoreList.Count; i++)
        {
            highScoreList[i] = gameData.highScoreList[i];
        }
        for (int i = 0; i < gameData.boxScoreList.Count; i++)
        {
            boxScoreList[i] = gameData.boxScoreList[i];
        }
    }
}
