using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spacecraft : MonoBehaviour
{
    public int packages = 0;
    public int maximumCharge = 20;
    public float estimatedTime = 20;
    public string spacecraftDestination = "test";

    private void OnCollisionEnter(Collision collision)
    {  
        if (collision.gameObject.GetComponent<Box>().destination != spacecraftDestination)
        {
            Debug.Log("mauviase adresse");
        }
        else
        { 
            packages++;
            Destroy(collision.gameObject);
        }
    }
}
