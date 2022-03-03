using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public GameObject bomb;
    public bool isArmed;
    public string destination = "test";
    //destination (faire une liste de nom dans un manager)

    void Start()
    {
        //destination rnd liste
        isArmed = false;
        if(Random.Range(0, 100) < 20) //mettre une variable ?manager? pour la proba de bombe
        {
            bomb.SetActive(true);
            isArmed = true;
        }
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
