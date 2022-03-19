using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectionTable : MonoBehaviour
{
    GameObject box;

    public void RecieveBox(GameObject newBox)
    {
        box = newBox;
        BoxInspector.Instance.DisplayInspector(box);
    }

    public void RetrieveBox()
    {
        box = null;
    }
}
