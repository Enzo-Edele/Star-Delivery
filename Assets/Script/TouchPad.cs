using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchPad : MonoBehaviour
{
    public static TouchPad Instance { get; private set; }

    public List<Spacecraft> spacecraft;

    void Awake()
    {
        Instance = this;
        StarLevel();
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

    public void EndLevel()
    {
        for (int i = 0; i < spacecraft.Count; i++)
        {
            if (spacecraft[i].launchCo != null)
            {
                StopCoroutine(spacecraft[i].launchCo);
            }
        }
        spacecraft.Clear();
    }
}
