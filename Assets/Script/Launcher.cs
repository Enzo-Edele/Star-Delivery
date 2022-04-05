using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public static Launcher Instance { get; private set; }

    public Spacecraft spacecraft;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        
    }

    public void Launch()
    {
        Debug.Log("a");
        spacecraft.launchCo = StartCoroutine(spacecraft.LaunchCoroutine());
    }
}
