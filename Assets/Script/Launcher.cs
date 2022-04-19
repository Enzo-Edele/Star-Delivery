using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Launcher : MonoBehaviour
{
    public Spacecraft spacecraft;

    public TMP_Text destinationTxt;
    public TMP_Text packagesTxt;
    public TMP_Text estimatedTimeTxt;
    public TMP_Text timeTxt;

    public GameObject launchPanel;
    public GameObject timePanel;

    void Update()
    {
        destinationTxt.text = "Destination : " + spacecraft.spacecraftDestination;
        packagesTxt.text = "Packages : " + spacecraft.packages + " / " + spacecraft.maximumCharge;
        estimatedTimeTxt.text = "Estimated Time : " + spacecraft.estimatedTime + " s";

        if (spacecraft.delivered)
        {
            timeTxt.text = "Time : " + Mathf.Round(spacecraft.estimatedTime + (spacecraft.deliveredTime - Time.time)) + " s";
        }
        else
        {
            spacecraft.deliveredTime = Time.time;
        }

        if (spacecraft.packages > spacecraft.maximumCharge)
        {
            if (spacecraft.packages == (spacecraft.overload + spacecraft.maximumCharge))
            {
                packagesTxt.color = new Color(1, 0, 0, 1);
            }
            else
            {
                packagesTxt.color = new Color(1, 0.85f, 0, 1);
            }
        }
        else
        {
            packagesTxt.color = new Color(1, 1, 1, 1);
        }

        launchPanel.SetActive(!spacecraft.delivered);
        timePanel.SetActive(spacecraft.delivered);
    }

    public void Launch()
    {
        spacecraft.launchCo = StartCoroutine(spacecraft.LaunchCoroutine());
    }
}
