using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spacecraft : MonoBehaviour
{
    public int packages = 0;

    private void OnCollisionEnter(Collision collision)
    {         
        packages++;
        Destroy(collision.gameObject);
    }
}
