using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Crusher : MonoBehaviour
{
    public void RecieveBox(GameObject box)
    {
        box.GetComponent<Box>().Crusher();
    }
}
