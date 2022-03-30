using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spacecraft : MonoBehaviour
{
    public Material spacecraftMaterial;
    public float orderInList;
    public GameObject spacecraft;
    public int packages = 0;
    public int maximumCharge = 10;
    public int overload = 5;
    public float estimatedTime = 20;
    public string spacecraftDestination;
    public bool delivered;
    private bool full;
    public float deliveredTime;
    private float sendScore = 0;
    public TMP_Text destinationText; //set up tout ca avec une fct

    private void Awake()
    {
        spacecraftMaterial = spacecraft.GetComponent<Renderer>().material;
        delivered = false;
        spacecraft = this.gameObject.transform.parent.gameObject;
        orderInList = TouchPad.Instance.spacecraft.Count;
        TouchPad.Instance.spacecraft.Add(this);
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
            sendScore += 100;
            if (box.isFragile) sendScore += 50;
            box.Navette();
        }
        if (packages >= (maximumCharge + overload))
        {
            full = true;
        }
        if (packages > maximumCharge)
        {
            estimatedTime += 2;
        }
    }

    public IEnumerator LaunchCoroutine() //stopper coroutine fin du niveau
    {
        delivered = true;
        GameManager.Instance.SpacecraftDeliver(packages);
        packages = 0;
        spacecraftMaterial.color = Color.blue;//anim décollage
        yield return new WaitForSeconds(estimatedTime);
        estimatedTime = 20;
        spacecraftMaterial.color = Color.red;//anim atterrisage
        delivered = false;
    }
}
