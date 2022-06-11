using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spacecraft : MonoBehaviour
{
    public Material spacecraftMaterial;
    public float orderInList;
    public GameObject spacecraft;
    public Coroutine launchCo;
    public int packages = 0;
    public int maximumCharge = 4;
    public int overload = 2;
    public float estimatedTime = 20;
    public string spacecraftDestination;
    public bool delivered;
    private bool full;
    public float deliveredTime;
    private int sendScore = 0;
    public TMP_Text destinationText; //set up tout ca avec une fct
    [SerializeField] Launcher launcher;

    private void Awake()
    {
        spacecraftMaterial = spacecraft.GetComponent<Renderer>().material;
        delivered = false;
        spacecraft = this.gameObject.transform.parent.gameObject;
        orderInList = GameManager.Instance.spacecraft.Count;
        GameManager.Instance.spacecraft.Add(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Box box = collision.gameObject.GetComponent<Box>();
        if (!full)
        { 
            packages++;
            sendScore += 100;
            if (box.isFragile) sendScore += 50;
            box.Navette(spacecraftDestination);
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

    public IEnumerator LaunchCoroutine()
    {
        delivered = true;
        GameManager.Instance.SpacecraftDeliver(packages);
        GameManager.Instance.UpdateScore(sendScore, -1);
        packages = 0;
        spacecraftMaterial.color = Color.blue;//anim décollage
        yield return new WaitForSeconds(estimatedTime);
        estimatedTime = 20;
        spacecraftMaterial.color = Color.red;//anim atterrisage
        delivered = false;
        full = false;
        launcher.ButtonUp();
    }
}
