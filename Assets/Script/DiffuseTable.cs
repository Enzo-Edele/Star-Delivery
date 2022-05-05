using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DiffuseTable : MonoBehaviour
{
    [SerializeField]CinemachineVirtualCamera cam;

    public GameObject bomb;
    public List<GameObject> button = new List<GameObject>();
    public GameObject bombedBox;
    public Bomb bombScript;
    //trouver un moyen de bypass la bombe

    [SerializeField] List<Material> indicMat = new List<Material>();
    [SerializeField] List<Renderer> indicator = new List<Renderer>();
    public List<Renderer> numbers = new List<Renderer>();

    public int combinationLength;
    public List<int> combination;
    public int step;

    private void Start()
    {
        Randomise();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(1) && 
           GameManager.GameState == GameManager.GameStates.InGame && 
           bomb != null && 
           GameManager.Instance.canDiffuse && 
           !(GameManager.GameState == GameManager.GameStates.isDiffusing))
        {
            DiffuseMod();
        }
        else if (Input.GetMouseButtonDown(1) && GameManager.GameState == GameManager.GameStates.isDiffusing && bomb != null)
        {
            QuitDiffuseMod();
        }
        /*if(Input.GetKeyDown("b") && GameManager.GameState == GameManager.GameStates.InGame)
            BoxInspector.Instance.HideInspector();*/
    }

    public void RecieveBox(GameObject box)
    {
        bomb = box.GetComponent<Box>().bomb;
        bombedBox = box;
        bombScript = bomb.GetComponent<Bomb>();
        bombScript.diffuseTable = this;
        for (int i = 0; i < bombScript.buttons.Count; i++)
            button.Add(bombScript.buttons[i]);
    }

    public void RetrieveBox()
    {
        bomb = null;
        bombedBox = null;
        bombScript = null;
        button.Clear();
    }

    void DiffuseMod()
    {
        bomb.transform.parent = null;
        for (int i = 0; i < button.Count; i++)
            button[i].transform.parent = null;
        bombScript.ActiveCollider();
        step = 0;
        cam.Priority = 15;
        Cursor.lockState = CursorLockMode.Confined;
        GameManager.Instance.ChangeGameState(GameManager.GameStates.isDiffusing);
        UIManager.Instance.ActivateIconPush();
        UIManager.Instance.ActivateIconReturn();
        UIManager.Instance.DeactivateChrono();
        UIManager.Instance.DeactivateLives();
        //maybe deactive UI
    }

    void QuitDiffuseMod()
    {
        for (int i = 0; i < combinationLength; i++)
        {
            numbers[i].material.color = Color.white;
            numbers[i].material.SetColor("_EmissionColor", Color.white);
        }
        bomb.transform.parent = bombedBox.transform;
        for (int i = 0; i < button.Count; i++)
            button[i].transform.parent = bomb.transform;
        bombScript.DeactiveCollider();
        cam.Priority = 5;
        Cursor.lockState = CursorLockMode.Locked;
        GameManager.Instance.ChangeGameState(GameManager.GameStates.InGame);
        UIManager.Instance.ActivateChrono();
        UIManager.Instance.ActivateLives();
    }

    public void Randomise()
    {
        combination.Clear();
        for (int i = 0; i < combinationLength; i++)
        {
            combination.Add(Random.Range(0, 4));
        }
        for (int i = 0; i < combinationLength; i++)
        {
            indicator[i].material = indicMat[combination[i]];
        }
        for (int i = 0; i < combinationLength; i++)
        {
            numbers[i].material.color = Color.white;
            numbers[i].material.SetColor("_EmissionColor", Color.white);
        }
    }
}
