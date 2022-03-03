using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltEnd : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Box")
            collision.gameObject.GetComponent<Box>().ReachEndBelt();
    }
}
