using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoBoxSpawner : MonoBehaviour
{
    [SerializeField] TutoManager tutoManager;

    [SerializeField] GameObject box;
    public GameObject package;

    [SerializeField] int objective;
    public bool isOn;
    [SerializeField] float timeMin, timeMax;
    [SerializeField] int percentageBomb, percentageGood, percentageFragile, percentageSus;
    float timerBoxes;

    [SerializeField] float timeLevel;
    float timerLevel;
    bool endLevel;

    private void Start()
    {
        GameManager.Instance.StartLevel(percentageBomb, percentageGood, percentageFragile, percentageSus, objective);
    }
    private void Update()
    {
        if (package != null)
        {
            if (package.GetComponent<Box>().isBroken)
            {
                tutoManager.ReDoStep(9, 7);
            }
        }
        if(isOn)
        {
            if (timerBoxes > 0 && !GameManager.Instance.gameIsPause && isOn)
                timerBoxes -= Time.deltaTime;
            else if (!GameManager.Instance.gameIsPause && isOn)
            {
                timerBoxes = Random.Range(timeMin, timeMax);
                Instantiate(box, transform.position + (transform.forward * 0.5f) + (Vector3.up * 0.1f), Quaternion.identity);
                SoundManager.Instance.Play("alarm");
            }
            if (timerLevel > 0 && !GameManager.Instance.gameIsPause)
                timerLevel -= Time.deltaTime;
            else if (!endLevel && !GameManager.Instance.gameIsPause)
            {
                Debug.Log("time's up");
                GameManager.Instance.EndLevel();
                endLevel = true;
            }
        }
    }

    public void SpawnBox(bool valid, bool fragile, bool sus, bool bomb, string destination)
    {
        package = Instantiate(box, transform.position + (transform.forward * 0.5f) + (Vector3.up * 0.1f), Quaternion.identity);
        package.GetComponent<Box>().ForceValue(valid, fragile, sus, bomb, destination);
        Debug.Log("spawn");
    }

    public void StartLevel()
    {
        isOn = true;
        timerBoxes = timeMin;
        timerLevel = timeLevel;
        endLevel = false;
        GameManager.Instance.StartLevel(percentageBomb, percentageGood, percentageFragile, percentageSus, objective);
    }
}
