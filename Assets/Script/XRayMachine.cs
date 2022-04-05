using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRayMachine : MonoBehaviour
{
    public Material XRay;

    private void Awake()
    {
        XRay = GetComponent<Renderer>().material;
    }

    private void OnTriggerEnter(Collider other)
    {
        Box box = other.gameObject.GetComponent<Box>();

        if (box.isSus == true)  XRay.color = Color.red;
        else                    XRay.color = Color.green;
    }
}
