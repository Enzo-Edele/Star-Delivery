using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spacecraft : MonoBehaviour
{
    public float orderInList;
    public GameObject spacecraft;
    public int packages = 0;
    public int maximumCharge = 20;
    public float estimatedTime = 20;
    public string spacecraftDestination = "test";
    public TMP_Text destinationText; //set up tout ca avec une fct

    private void Awake()
    {
        spacecraft = this.gameObject.transform.parent.gameObject;
        orderInList = TouchPad.Instance.spacecraft.Count;
        TouchPad.Instance.spacecraft.Add(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Box box = collision.gameObject.GetComponent<Box>();
        if (box.destination != spacecraftDestination)
        {
            Debug.Log("mauviase adresse");
        }
        else
        { 
            packages++;
            box.Navette();
        }
    }

    public IEnumerator Launch()
    {
        Debug.Log("a");
        spacecraft.SetActive(false); //anim décollage
        yield return new WaitForSeconds(estimatedTime);
        spacecraft.SetActive(true); //anim attérissage
    }
}
