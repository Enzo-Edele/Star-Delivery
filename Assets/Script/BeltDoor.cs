using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltDoor : MonoBehaviour
{
    [SerializeField] GameObject door;
    public Vector3 amplitude;
    Vector3 pos;
    [SerializeField] float timeOpen;
    float timerOpen = 0;

    private void Update()
    {
        if (timerOpen > 0)
            timerOpen -= Time.deltaTime;
        else if(timerOpen < 0)
        {
            timerOpen = 0;
            Close();
        }    
    }

    void Open()
    {
        timerOpen = timeOpen;
        pos = door.transform.position;
        pos.y += amplitude.y;
        door.transform.position = pos;
        Debug.Log("open");
    }
    void Close()
    {
        pos = door.transform.position;
        pos.y -= amplitude.y;
        door.transform.position = pos;
        Debug.Log("close");
    }
    private void OnTriggerEnter(Collider other)
    {
        Open();
        Debug.Log("collision");
    }
}
