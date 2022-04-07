using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class XRayMachine : MonoBehaviour
{
    public Material XRay;
    public TMP_Text X;
    public TMP_Text V;

    private void Awake()
    {
        XRay = GetComponent<Renderer>().material;
    }

    private void OnTriggerEnter(Collider other)
    {
        Box box = other.gameObject.GetComponent<Box>();

        if (box.isSus == true)
        {
            X.text = "X";
            V.text = "";
        }            
        else
        {
            X.text = "";
            V.text = "V";
        }
    }
}
