using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] AudioClip explosion;
    public GameObject bomb;
    public bool isArmed { get; private set; }
    public string destination= "test";
    string company = "Amazoon";
    string content = "Stuff";

    void Start()
    {
        if (Random.Range(0, 100) < GameManager.Instance.percentageValid)
            destination = GameManager.Instance.validDestinationLevel[Random.Range(0, GameManager.Instance.validDestinationLevel.Count)];
        else
            destination = GameManager.Instance.invalidDestinationLevel[Random.Range(0, GameManager.Instance.invalidDestinationLevel.Count)];
        isArmed = false;
        if(Random.Range(0, 100) < GameManager.Instance.percentageBomb) //mettre une variable ?manager? pour la proba de bombe
        {
            bomb.SetActive(true);
            isArmed = true;
        }
    }

    public bool GetData()
    {
        return isArmed;
    }

    public void SetData(bool armed)
    {
        isArmed = armed;
    }

    public void DisplayInfo()
    {
        UIManager.Instance.ActivateBoxInfo(destination, company, content);
    }

    public void Belt()
    {
        if (isArmed)
            SoundManager.Instance.PlaySoundEffect(explosion);
        bool isValid = false;
        for (int i = 0; i < GameManager.Instance.validDestinationLevel.Count; i++)
            if (destination == GameManager.Instance.validDestinationLevel[i]) 
                isValid = true;
        if(isValid)
            Debug.Log("valid non traité");
        else
            Debug.Log("pas valid non traité");
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
            SoundManager.Instance.PlaySoundEffect(explosion);
        for (int i = 0; i < GameManager.Instance.validDestinationLevel.Count; i++)
            if (destination == GameManager.Instance.validDestinationLevel[i]) 
                Debug.Log("valid détruit");
        Destroy(gameObject);
    }
    public void Navette()
    {
        if (isArmed)
            SoundManager.Instance.PlaySoundEffect(explosion);
        Destroy(gameObject);
    }
}
