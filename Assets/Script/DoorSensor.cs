using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSensor : MonoBehaviour
{
    [SerializeField]Door door;
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        door.Close();
        Debug.Log("sense");
    }
}
