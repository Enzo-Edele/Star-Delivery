using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData : MonoBehaviour
{
    public int day;
    //public int[] highScoreList = new int[GameManager.Instance.HighScoreList.Count];

    public SaveData(GameManager gameData)
    {/*
        level = gameData.level;
        for (int i = 0; i < gameData.HighScoreList.Count; i++)
        {
            highScoreList[i] = gameData.HighScoreList[i];
        }*/
    }
}
