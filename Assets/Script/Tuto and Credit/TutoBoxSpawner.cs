using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoBoxSpawner : MonoBehaviour
{
    [SerializeField] TutoManager tutoManager;

    [SerializeField] GameObject box;
    [SerializeField] GameObject package;

    [SerializeField] int objective;
    [SerializeField] int percentageBomb, percentageGood, percentageFragile, percentageSus;
    float timerBoxes;

    [SerializeField] bool isSecond;

    void Start()
    {
        StartLevel();
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
    }

    public void SpawnBox(bool valid, bool fragile, bool sus, bool bomb)
    {
        package = Instantiate(box, transform.position + (transform.forward * 0.5f) + (Vector3.up * 0.1f), Quaternion.identity);
        package.GetComponent<Box>().ForceValue(valid, fragile, sus, bomb);
    }

    void StartLevel()
    {
        GameManager.Instance.StartLevel(percentageBomb, percentageGood, percentageFragile, percentageSus, objective);
    }
}
