using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoxSpawner : MonoBehaviour
{
    [SerializeField]GameObject boxPrefab;
    GameObject box;

    [SerializeField] int objective;
    public bool isOn;
    [SerializeField] float timeMin, timeMax;
    [SerializeField] int percentageBomb, percentageGood, percentageFragile, percentageSus;
    float timerBoxes;

    [SerializeField] float timeLevel;
    float timerLevel;
    bool endLevel;

    [SerializeField] bool isSecond;
    [SerializeField] int timerDial;

    void Start()
    {
        StartLevel();
    }

    void Update()
    {
        if(timerBoxes > 0 && !GameManager.Instance.gameIsPause && isOn)
            timerBoxes -= Time.deltaTime;
        else if(!GameManager.Instance.gameIsPause && isOn)
        {
            timerBoxes = Random.Range(timeMin, timeMax);
            box = Instantiate(boxPrefab, transform.position + (transform.forward * 0.5f) + (Vector3.up * 0.1f), Quaternion.identity);
            if (isSecond)
                box.GetComponent<Box>().NotSus();
            SoundManager.Instance.Play("SpawnBox");
        }
        if (timerLevel > 0 && !GameManager.Instance.gameIsPause)
            timerLevel -= Time.deltaTime;
        else if (!endLevel && !GameManager.Instance.gameIsPause && !isSecond)
        {
            GameManager.Instance.EndLevel();
            endLevel = true;
        }

        GameManager.Instance.TimeLevel(timerLevel);
    }

    void StartLevel()
    {
        isOn = true;
        timerBoxes = timeMin;
        timerLevel = timeLevel;
        endLevel = false;
        GameManager.Instance.StartLevel(percentageBomb, percentageGood, percentageFragile, percentageSus, objective, 3);
        UIManager.Instance.ActivateDialogue(TextManager.Instance.LvlEntryDial[SceneManager.GetActiveScene().buildIndex - 3], timerDial);
    }
}
