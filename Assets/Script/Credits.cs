using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(CreditsTime());
    }

    public IEnumerator CreditsTime()
    {
        yield return new WaitForSeconds(22);
        Debug.Log("a");
    }
}
