using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DiffuseTable : MonoBehaviour
{
    [SerializeField]CinemachineVirtualCamera cam;

    GameObject bomb;
    GameObject bombedBox;

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown("f"))
        {
            QuitDiffuseMod();
        }
        if (Input.GetKeyDown("r"))
        {
            DiffuseMod();
        }
    }

    public void RecieveBox(GameObject box)
    {
        bomb = box.GetComponent<Box>().bomb;
        bombedBox = box;
    }

    public void RetrieveBox()
    {
        bomb = null;
        bombedBox = null;
    }

    //mettre dans un manager et pensé a lock mouvement du perso

    void DiffuseMod()
    {
        bomb.transform.parent = null;
        cam.Priority = 15;
        Cursor.lockState = CursorLockMode.Confined;
    }

    void QuitDiffuseMod()
    {
        bomb.transform.parent = bombedBox.transform;
        cam.Priority = 5;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
