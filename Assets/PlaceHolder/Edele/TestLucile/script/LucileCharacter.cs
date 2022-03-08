using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LucileCharacter : MonoBehaviour
{
    [SerializeField] GameObject mainCam;
    [SerializeField] List<CinemachineVirtualCamera> playerCams;
    int camInUse;

    public GameObject grabObject;

    public static LucileCharacter Instance { get; private set; }

    void Start()
    {
        Instance = this;
        camInUse = 0;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            playerCams[camInUse].Priority = 5;
            camInUse++;
            if (camInUse >= playerCams.Count)
                camInUse = 0;
            playerCams[camInUse].Priority = 10;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            playerCams[camInUse].Priority = 5;
            camInUse--;
            if (camInUse < 0)
                camInUse = playerCams.Count - 1;
            playerCams[camInUse].Priority = 10;
        }
    }

    public void ChangeCam(bool usePlayer)
    {
        if(usePlayer)
            playerCams[camInUse].Priority = 10;
        else
            playerCams[camInUse].Priority = 5;
    }

    public void GrabBox(GameObject box)
    {
        if (box.transform.parent != null)
        {
            box.transform.parent.GetComponent<DiffuseTable>().RetrieveBox(); //essayer de virer ici aussi
        }
        grabObject = box;
        Rigidbody rBody = grabObject.GetComponent<Rigidbody>();
        rBody.velocity = Vector3.zero;
        rBody.angularVelocity = Vector3.zero;
        grabObject.transform.parent = mainCam.transform;
        grabObject.transform.transform.localScale = new Vector3(1, 1, 1);
        grabObject.transform.transform.localRotation = Quaternion.Euler(0, 0, 0);
        grabObject.transform.localPosition = new Vector3(0, -0.8f, 1.35f);
        rBody.useGravity = false;
        grabObject.GetComponent<BoxCollider>().enabled = false; //faire un tag BoxGrab et désactiver collision de ce tag
        grabObject.GetComponent<LucileBox>().DisplayInfo();
    }
    public void DropBox(GameObject dropArea)
    {
        grabObject.transform.parent = null;
        grabObject.GetComponent<BoxCollider>().enabled = true;
        grabObject.GetComponent<Rigidbody>().useGravity = true;
        grabObject.transform.parent = dropArea.transform;
        grabObject.transform.transform.localRotation = Quaternion.Euler(0, 0, 0);
        grabObject.transform.localPosition = new Vector3(0, 1.1f, 0f);
        grabObject.transform.parent = null;
        grabObject.transform.transform.localScale = new Vector3(1, 1, 1);
        grabObject = null;
        UIManager.Instance.DeactivateBoxInfo();
    }
}
