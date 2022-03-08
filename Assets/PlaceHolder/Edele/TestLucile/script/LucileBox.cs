using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LucileBox : MonoBehaviour
{
    public GameObject bomb;
    public bool isArmed { get; private set; }
    public string destination = "test";
    string company = "Amazoon";
    string content = "Stuff";
    //destination (faire une liste de nom dans un manager)

    void Start()
    {
        if (Random.Range(0, 100) < 50)
        {
            destination = GameManager.Instance.validDestination[Random.Range(0, GameManager.Instance.validDestination.Count)];
        }
        else
        {
            destination = GameManager.Instance.invalidDestination[Random.Range(0, GameManager.Instance.invalidDestination.Count)];
        }
        isArmed = false;
        if (Random.Range(0, 100) < 20) //mettre une variable ?manager? pour la proba de bombe
        {
            bomb.SetActive(true);
            isArmed = true;
        }
    }

    private void OnMouseDown()
    {
        if(LucileCharacter.Instance.grabObject == null)
            LucileCharacter.Instance.GrabBox(this.gameObject);
    }

    public void DisplayInfo()
    {
        UIManager.Instance.ActivateBoxInfo(destination, company, content);
    }

    public void ReachEndBelt()
    {
        Destroy(gameObject);
    }

    public void Diffuse()
    {
        bomb.SetActive(false);
        isArmed = false;
    }
}
