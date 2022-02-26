using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public CinemachineVirtualCamera cam;
    public static GameManager Instance { get; private set; }
    void Start()
    {
        Instance = this;
    }

    void Update()
    {
        
    }
}
