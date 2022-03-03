using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchPad : MonoBehaviour
{
    public List<Spacecraft> spacecraft;
    public Text destiantion;
    public Text packages;
    public Text estimatedTime;
    public Text spacecraftNumber;
    public Button launch;
    public Image spacecraftVisu;

    public int screen = 0;

    public Button leftArrow;
    public Button rightArrow;

    void Update()
    {
        destiantion.text =      "Destination : " + spacecraft[screen].spacecraftDestination;
        packages.text =         "Packages : " + spacecraft[screen].packages + " / " + spacecraft[screen].maximumCharge;
        estimatedTime.text =    "Estimated Time : " + spacecraft[screen].estimatedTime + "s";
        spacecraftNumber.text = "Spacecraft n°" + (screen + 1);

        if (spacecraft.Count != screen + 1) rightArrow.gameObject.SetActive(true);        
        else                                rightArrow.gameObject.SetActive(false);
        
        if (screen != 0)                    leftArrow.gameObject.SetActive(true);
        else                                leftArrow.gameObject.SetActive(false);
    }

    public void LeftArrow()
    {
        screen -= 1;
    }
    
    public void RightArrow()
    {
        screen += 1;
    }
}
