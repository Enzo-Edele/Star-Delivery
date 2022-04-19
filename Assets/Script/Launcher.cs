using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Launcher : MonoBehaviour
{
    public Spacecraft spacecraft;

    public TMP_Text destiantion;
    public TMP_Text packages;
    public TMP_Text estimatedTime;
    public TMP_Text time;

    public GameObject launchPanel;
    public GameObject timePanel;

    void Update()
    {
        destiantion.text = "Destination : " + spacecraft.spacecraftDestination;
        packages.text = "Packages : " + spacecraft.packages + " / " + spacecraft.maximumCharge;
        estimatedTime.text = "Estimated Time : " + spacecraft.estimatedTime + " s";

        if (spacecraft.delivered)
        {
            time.text = "Time : " + Mathf.Round(spacecraft.estimatedTime + (spacecraft.deliveredTime - Time.time)) + " s";
        }
        else
        {
            spacecraft.deliveredTime = Time.time;
        }

        if (spacecraft.packages > spacecraft.maximumCharge)
        {
            if (spacecraft.packages == (spacecraft.overload + spacecraft.maximumCharge))
            {
                packages.color = new Color(1, 0, 0, 1);
            }
            else
            {
                packages.color = new Color(1, 0.85f, 0, 1);
            }
        }
        else
        {
            packages.color = new Color(1, 1, 1, 1);
        }

        launchPanel.SetActive(!spacecraft.delivered);
        timePanel.SetActive(spacecraft.delivered);
    }

    public void Launch()
    {
        spacecraft.launchCo = StartCoroutine(spacecraft.LaunchCoroutine());
    }
}
