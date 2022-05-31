using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSensor : MonoBehaviour
{
    [SerializeField]Door door;
    [SerializeField] bool tuto;
    bool isOpen;
    void Start()
    {
        isOpen = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(tuto)
            door.Close();
        else if(!isOpen)
        {
            door.Open();
            isOpen = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(isOpen && !tuto)
        {
            door.Close();
            isOpen = false;
        }
    }
}
