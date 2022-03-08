using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LucileSpacecraft : MonoBehaviour
{
    public int packages = 0;
    public int maximumCharge = 20;
    public float estimatedTime = 20;
    public string spacecraftDestination = "test";
    private void OnMouseDown()
    {
        if (LucileCharacter.Instance.grabObject != null)
        {
            if (LucileCharacter.Instance.grabObject.GetComponent<LucileBox>().destination != spacecraftDestination)
                Debug.Log("mauviase adresse");
            else
            {
                packages++;
                Destroy(LucileCharacter.Instance.grabObject);
            }
        }
    }
}
