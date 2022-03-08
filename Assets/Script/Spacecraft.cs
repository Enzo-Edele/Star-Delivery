using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spacecraft : MonoBehaviour
{
    public int packages = 0;
    public int maximumCharge = 20;
    public float estimatedTime = 20;
    public string spacecraftDestination = "test";
    public TMP_Text destinationText; //set up tout ca avec une fct

    private void OnCollisionEnter(Collision collision)
    {
        Box box = collision.gameObject.GetComponent<Box>();
        if (box.destination != spacecraftDestination)
        {
            Debug.Log("mauviase adresse");
        }
        else
        { 
            packages++;
            box.Navette();
        }
    }
}
