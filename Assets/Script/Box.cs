using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public GameObject bomb;
    public bool isArmed { get; private set; }
    public string destination = "test";
    string company = "Amazoon";
    string content = "Stuff";
    public bool isFragile;
    public bool isBroken;
    public bool isSus;

    void Awake() //start
    {
        if (Random.Range(0, 100) < GameManager.Instance.percentageValid)
            destination = GameManager.Instance.validDestinationLevel[Random.Range(0, GameManager.Instance.validDestinationLevel.Count)];
        else
            destination = GameManager.Instance.invalidDestinationLevel[Random.Range(0, GameManager.Instance.invalidDestinationLevel.Count)];

        if (Random.Range(0, 2) == 1) isSus = true;
        else                         isSus = false;

        isArmed = false;
        if (Random.Range(0, 100) < GameManager.Instance.percentageBomb) 
        {
            bomb.SetActive(true);
            isArmed = true;
        }
    }

    public void ForceValue(bool valid, bool fragile, bool sus, bool bomb)
    {
        if(!valid)
            destination = GameManager.Instance.invalidDestinationLevel[Random.Range(0, GameManager.Instance.invalidDestinationLevel.Count)];
        else
            destination = GameManager.Instance.validDestinationLevel[Random.Range(0, GameManager.Instance.validDestinationLevel.Count)];

        this.bomb.SetActive(bomb);
        isArmed = bomb;

        isSus = sus;

        isFragile = fragile;
    }

    public void DisplayInfo()
    {
        UIManager.Instance.ActivateBoxInfo(destination, company, content);
    }

    public void Belt()
    {
        if (isArmed)
        {
            SoundManager.Instance.Play("explosion");
        }
        bool isValid = false;
        for (int i = 0; i < GameManager.Instance.validDestinationLevel.Count; i++)
            if (destination == GameManager.Instance.validDestinationLevel[i]) 
                isValid = true;
        if(isValid)
            Debug.Log("valid non trait�");
        else
            Debug.Log("pas valid non trait�");
        Destroy(gameObject);
    }

    public void Diffuse()
    {
        bomb.SetActive(false);
        isArmed = false;
    }

    public void Crusher()
    {
        if (isArmed)
            SoundManager.Instance.Play("explosion");
        /*for (int i = 0; i < GameManager.Instance.validDestinationLevel.Count; i++)
            if (destination == GameManager.Instance.validDestinationLevel[i]) 
                Debug.Log("valid d�truit");*/
        Destroy(gameObject);
    }

    public void Navette()
    {
        if (isArmed)
            SoundManager.Instance.Play("explosion");
        Destroy(gameObject);
    }
}
