using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DiffuseTable : MonoBehaviour
{
    [SerializeField]CinemachineVirtualCamera cam;

    GameObject bomb;
    GameObject bombedBox;
    Bomb bombScript;
    //trouver un moyen de bypass la bombe
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
        bombScript = bomb.GetComponent<Bomb>();
    }

    public void RetrieveBox()
    {
        bomb = null;
        bombedBox = null;
        bombScript = null;
    }

    //mettre dans un manager et pensé a lock mouvement du perso

    void DiffuseMod()
    {
        bomb.transform.parent = null;
        bombScript.ActiveCollider();
        cam.Priority = 15;
        Cursor.lockState = CursorLockMode.Confined;
    }

    void QuitDiffuseMod()
    {
        bomb.transform.parent = bombedBox.transform;
        bombScript.DeactiveCollider();
        cam.Priority = 5;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
