using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LucileDiffuse : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera cam;

    GameObject bomb;
    GameObject bombedBox;
    Bomb bombScript;
    //trouver un moyen de bypass la bombe
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown("f") && bombedBox != null)
        {
            QuitDiffuseMod();
            LucileCharacter.Instance.GrabBox(bombedBox);
            RetrieveBox();
        }
    }

    private void OnMouseDown()
    {
        if(LucileCharacter.Instance.grabObject != null)
        {
            RecieveBox(LucileCharacter.Instance.grabObject);
            LucileCharacter.Instance.DropBox(this.gameObject);
            DiffuseMod();
        }
    }

    public void RecieveBox(GameObject box)
    {
        bomb = box.GetComponent<LucileBox>().bomb;
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
        GameManager.Instance.lockPlayer = true;
        //maybe deactive UI
    }

    void QuitDiffuseMod()
    {
        bomb.transform.parent = bombedBox.transform;
        bombScript.DeactiveCollider();
        cam.Priority = 5;
        GameManager.Instance.lockPlayer = false;
    }
}
