using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public bool tuto;
    public int levelUnlock;
    public int[] highScoreList = new int[GameManager.Instance.highScoreList.Count];
    public int[] boxScoreList = new int[GameManager.Instance.boxScoreList.Count];


    public SaveData(GameManager gameData)
    {
        tuto = gameData.tutorial;
        levelUnlock = gameData.levelUnlock;
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
