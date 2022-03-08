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

    void Start()
    {
        StarLevel();
    }

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

    public void Arrow(int direction)
    {
        screen += direction;
    }

    public void StarLevel()
    {
        GameManager.Instance.invalidDestinationLevel.Clear();
        GameManager.Instance.validDestinationLevel.Clear();
        List<string> memory = new List<string>();
        for(int i = 0; i < spacecraft.Count; i++)
        {
            int rnd = Random.Range(0, GameManager.Instance.validDestination.Count);
            spacecraft[i].spacecraftDestination = GameManager.Instance.validDestination[rnd];
            spacecraft[i].destinationText.text = GameManager.Instance.validDestination[rnd]; 
            GameManager.Instance.validDestinationLevel.Add(GameManager.Instance.validDestination[rnd]);
            memory.Add(GameManager.Instance.validDestination[rnd]);
            GameManager.Instance.validDestination.RemoveAt(rnd);
        }

        for (int i = 0; i < GameManager.Instance.invalidDestination.Count; i++)
            GameManager.Instance.invalidDestinationLevel.Add(GameManager.Instance.invalidDestination[i]);
        for (int i = 0; i < GameManager.Instance.validDestination.Count; i++)
            GameManager.Instance.invalidDestinationLevel.Add(GameManager.Instance.validDestination[i]);

        for (int i = 0; i < spacecraft.Count; i++)
            GameManager.Instance.validDestination.Add(memory[i]);
    }
}
