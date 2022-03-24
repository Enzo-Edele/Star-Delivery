using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchPad : MonoBehaviour
{
    public static TouchPad Instance { get; private set; }

    public List<Spacecraft> spacecraft;
    public Text destiantion;
    public Text packages;
    public Text estimatedTime;
    public Text spacecraftNumber;
    public Text time;
    public Button launch;
    public Image spacecraftVisu;

    public GameObject launchPanel;
    public GameObject timePanel;

    public int screen = 0;

    public Button leftArrow;
    public Button rightArrow;

    void Awake()
    {
        Instance = this;
        StarLevel();
    }

    void Update()
    {
        if (spacecraft.Count > 0)
        {
            destiantion.text =      "Destination : " + spacecraft[screen].spacecraftDestination;
            packages.text =         "Packages : " + spacecraft[screen].packages + " / " + spacecraft[screen].maximumCharge;
            estimatedTime.text =    "Estimated Time : " + spacecraft[screen].estimatedTime + "s";
            spacecraftNumber.text = "Spacecraft n°" + (screen + 1);

            if (spacecraft[screen].delivered)
            {
                time.text = "Time : " + Mathf.Round(spacecraft[screen].estimatedTime + (spacecraft[screen].deliveredTime - Time.time)) + "s";
            }
            else
            {
                spacecraft[screen].deliveredTime = Time.time;
            }

            if (spacecraft[screen].packages > spacecraft[screen].maximumCharge)
            {
                if (spacecraft[screen].packages == (spacecraft[screen].overload + spacecraft[screen].maximumCharge))
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
        }

        if (spacecraft.Count != screen + 1)
        {
            rightArrow.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown("d"))
            {
                Arrow(1);
            }
        }
        else rightArrow.gameObject.SetActive(false);
        
        if (screen != 0)
        {
            leftArrow.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown("q"))
            {
                Arrow(-1);
            }
        }
        else leftArrow.gameObject.SetActive(false);



        if (spacecraft.Count > 0)
        {
            launchPanel.SetActive(!spacecraft[screen].delivered);
            timePanel.SetActive(spacecraft[screen].delivered);
        }
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

    public void Launch()
    {       
        StartCoroutine(spacecraft[screen].LaunchCoroutine());
    }
}
