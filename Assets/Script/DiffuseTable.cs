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
        if(Input.GetKeyDown("b"))
            BoxInspector.Instance.HideInspector();
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

    void DiffuseMod()
    {
        bomb.transform.parent = null;
        bombScript.ActiveCollider();
        cam.Priority = 15;
        Cursor.lockState = CursorLockMode.Confined;
        GameManager.Instance.LockPlayer();
        //maybe deactive UI
    }

    void QuitDiffuseMod()
    {
        bomb.transform.parent = bombedBox.transform;
        bombScript.DeactiveCollider();
        cam.Priority = 5;
        Cursor.lockState = CursorLockMode.Locked;
        GameManager.Instance.UnlockPlayer();
    }
}
