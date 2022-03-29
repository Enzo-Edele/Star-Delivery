using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    [SerializeField]GameObject box;

    [SerializeField] int objective;
    [SerializeField] float timeMin, timeMax;
    [SerializeField] int percentageBomb, percentageGood;
    float timerBoxes;
    [SerializeField]float timeLevel;
    float timerLevel;
    bool endLevel;

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
            SoundManager.Instance.Play("alarm");
        }
        if (timerLevel > 0)
            timerLevel -= Time.deltaTime;
        else if (!endLevel)
        {
            GameManager.Instance.EndLevel();
            endLevel = true;
        }
    }

    void StartLevel()
    {
        timerBoxes = timeMin;
        timerLevel = timeLevel;
        endLevel = false;
        GameManager.Instance.StartLevel(percentageBomb, percentageGood, objective);
        TouchPad.Instance.StarLevel();
    }
}
