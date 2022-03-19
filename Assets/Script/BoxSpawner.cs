using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    [SerializeField]GameObject box;
    [SerializeField] AudioClip alarm;

    [SerializeField] float timeMin, timeMax;
    [SerializeField] int percentageBomb, percentageGood;
    float timer;

    void Start()
    {
        StartLevel();
    }

    void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = Random.Range(timeMin, timeMax);
            Instantiate(box, transform.position + (transform.forward * 0.5f) + (Vector3.up * 0.1f), Quaternion.identity);
            SoundManager.Instance.PlaySoundEffect(alarm);
        }
    }

    void StartLevel()
    {
        timer = timeMin;
        GameManager.Instance.percentageBomb = percentageBomb;
        GameManager.Instance.percentageValid = percentageGood;
        TouchPad.Instance.StarLevel();
    }
}
