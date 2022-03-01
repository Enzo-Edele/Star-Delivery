using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crusher : MonoBehaviour
{
    public void RecieveBox(GameObject box)
    {
        if(box.GetComponent<Box>().isArmed)
            Debug.Log("boom");
        else
            Destroy(box);
    }
}
