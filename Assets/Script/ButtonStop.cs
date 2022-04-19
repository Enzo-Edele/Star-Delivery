using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonStop : MonoBehaviour
{
    [SerializeField] int timeStop;
    float timerStop;
    [SerializeField] int timeUse;
    float timerUse;

    [SerializeField] List<ConvoyerBelt> belts;
    [SerializeField] BoxSpawner spawner;

    private void Update()
    {
        if (timerStop > 0)
            timerStop -= Time.deltaTime;
        else if(timerStop != 0)
        {
            for (int i = 0; i < belts.Count; i++)
                belts[i].isOn = true;
            spawner.isOn = true;
            timerStop = 0;
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

    public void stopBelt()
    {
        if (timerUse == 0)
        {
            for (int i = 0; i < belts.Count; i++)
                belts[i].isOn = false;
            timerStop = timeStop;
            timerUse = timeUse;
        }
        spawner.isOn = false;
        Vector3 pos = gameObject.transform.localPosition;
        pos.y -= 0.1f;
        gameObject.transform.localPosition = pos;
        SoundManager.Instance.Play("StopBelt");
    }
}
