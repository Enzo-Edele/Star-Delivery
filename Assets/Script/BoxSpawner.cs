using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    [SerializeField]GameObject box;
    [SerializeField] AudioClip alarm;

    [SerializeField] int objective;
    [SerializeField] float timeMin, timeMax;
    [SerializeField] int percentageBomb, percentageGood;
    float timerBoxes;
    [SerializeField]float timeLevel;
    float timerLevel;

    void Start()
    {
        StartLevel();
    }

    void Update()
    {
        if(timerBoxes > 0)
            timerBoxes -= Time.deltaTime;
        else
        {
            timerBoxes = Random.Range(timeMin, timeMax);
            Instantiate(box, transform.position + (transform.forward * 0.5f) + (Vector3.up * 0.1f), Quaternion.identity);
            SoundManager.Instance.PlaySoundEffect(alarm);
        }/*
        if (timerLevel > 0)
            timerLevel -= Time.deltaTime;
        else
        {
            if (objective == 2)
                GameManager.Instance.levelUnlock++;
            GameManager.Instance.EndLevel();
            UIManager.Instance.ActivateEndLevel();
        }*/
    }

    void StartLevel()
    {
        timerBoxes = timeMin;
        timerLevel = timeLevel;
        GameManager.Instance.percentageBomb = percentageBomb;
        GameManager.Instance.percentageValid = percentageGood;
        TouchPad.Instance.StarLevel();
    }
}
