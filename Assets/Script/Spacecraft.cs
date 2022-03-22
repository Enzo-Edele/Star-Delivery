using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spacecraft : MonoBehaviour
{
    public float orderInList;
    public GameObject spacecraft;
    public int packages = 0;
    public int maximumCharge = 10;
    public int overload = 5;
    public float estimatedTime = 20;
    public string spacecraftDestination = "test";
    public bool delivered;
    private bool full;
    public TMP_Text destinationText; //set up tout ca avec une fct

    private void Awake()
    {
        delivered = false;
        spacecraft = this.gameObject.transform.parent.gameObject;
        orderInList = TouchPad.Instance.spacecraft.Count;
        TouchPad.Instance.spacecraft.Add(this);
    }

    private void Update()
    {
        if (packages >= (maximumCharge + overload))
        {
            full = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Box box = collision.gameObject.GetComponent<Box>();
        if (box.destination != spacecraftDestination)
        {
            Debug.Log("mauvaise adresse");
        }
        else if (!full)
        { 
            packages++;
            box.Navette();
        }
    }

    public IEnumerator LaunchCoroutine()
    {
        delivered = true;
        spacecraft.SetActive(false); //anim décollage
        yield return new WaitForSeconds(estimatedTime);
        spacecraft.SetActive(true);  //anim atterrisage
        delivered = false;
    }
}
