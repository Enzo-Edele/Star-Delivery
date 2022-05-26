using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAll : MonoBehaviour
{
    [SerializeField] int timeStop;
    float timerStop;
    [SerializeField] int timeUse;
    float timerUse;

    [SerializeField] List<ConvoyerBelt> belts;
    [SerializeField] List<BoxSpawner> spawner;
    [SerializeField] List<BeltDoor> door;

    [SerializeField] bool tuto;

    [SerializeField] TutoBoxSpawner spawnerTuto;

    private void Update()
    {
        if (timerStop > 0)
            timerStop -= Time.deltaTime;
        else if (timerStop != 0)
        {
            timerStop = 0;
            for (int i = 0; i < belts.Count; i++)
                belts[i].isOn = true;
            for (int i = 0; i < spawner.Count; i++)
                spawner[i].isOn = true;
            for (int i = 0; i < door.Count; i++)
                door[i].isOn = true;
        }
        if (timerUse > 0)
            timerUse -= Time.deltaTime;
        else if (timerUse != 0)
        {
            timerUse = 0;
            Vector3 pos = gameObject.transform.localPosition;
            pos.y += 0.1f;
            gameObject.transform.localPosition = pos;
        }
    }

    public void Push()
    {
        if (tuto)
            StartTuto();
        else
            StopBelt();
    }

    void StopBelt()
    {
        if (timerUse == 0)
        {
            for (int i = 0; i < belts.Count; i++)
                belts[i].isOn = false;
            for (int i = 0; i < spawner.Count; i++)
                spawner[i].isOn = false;
            for (int i = 0; i < door.Count; i++)
                door[i].isOn = false;
            timerStop = timeStop;
            timerUse = timeUse;
            Vector3 pos = gameObject.transform.localPosition;
            pos.y -= 0.1f;
            gameObject.transform.localPosition = pos;
            SoundManager.Instance.Play("StopBelt");
        }
    }

    void StartTuto()
    {
        spawnerTuto.StartLevel();
        Vector3 pos = gameObject.transform.localPosition;
        pos.y -= 0.08f;
        gameObject.transform.localPosition = pos;
    }
}
