using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrabDrop : MonoBehaviour
{
    [SerializeField] Vector3 direction;
    float interactRange = 3;
    float angleRaycast = 15.0f;
    bool canBePushed;
    RaycastHit hit;
    bool hasHit;
    Ray[] rays = new Ray[5];
    LayerMask maskRay;
    [SerializeField] GameObject grabableObject;
    public GameObject grabObject;
    [SerializeField] Box grabObjectScript;
    GameObject dropAreaRack;
    GameObject dropAreaRay;
    GameObject dropAreaDiffuse;
    GameObject dropAreaCrusher;
    GameObject dropAreaSpacecraft;
    Launcher launcherScript;
    ButtonAll button;

    public PlayerMovement player;

    void Start()
    {
        maskRay = LayerMask.GetMask("Package", "Interractible");
    }

    void Update()
    {
        rays[0] = new Ray(transform.position, transform.forward);
        rays[1] = new Ray(transform.position, Quaternion.Euler(0, -angleRaycast, 0) * transform.forward);
        rays[2] = new Ray(transform.position, Quaternion.Euler(0, angleRaycast, 0) * transform.forward);
        rays[3] = new Ray(transform.position, Quaternion.Euler(angleRaycast, 0, 0) * transform.forward);
        rays[4] = new Ray(transform.position, Quaternion.Euler(-angleRaycast, 0, 0) * transform.forward);

        Debug.DrawRay(rays[0].origin, rays[0].direction * interactRange, Color.red);
        Debug.DrawRay(rays[1].origin, rays[1].direction * interactRange, Color.blue);
        Debug.DrawRay(rays[2].origin, rays[2].direction * interactRange, Color.cyan);
        Debug.DrawRay(rays[3].origin, rays[3].direction * interactRange, Color.yellow);
        Debug.DrawRay(rays[4].origin, rays[4].direction * interactRange, Color.green);

        hasHit = false;
        for (int i = 0; i < 5; i++) //mettre en fonction return hit
        {
            if (Physics.Raycast(rays[i], out hit, interactRange/*, maskRay*/) &&
                !GameManager.Instance.lockPlayer
                )
            {
                if (hit.transform.gameObject.tag == "Box" && grabObject == null && hit.transform.gameObject.transform.parent != null && !hasHit)
                {
                    UIManager.Instance.ActivateIconGrab();
                    UIManager.Instance.ActivateIconDiffuse();
                    grabableObject = hit.transform.gameObject;
                    hasHit = true;
                }
                else if (hit.transform.gameObject.tag == "Box" && grabObject == null && !hasHit)
                {
                    UIManager.Instance.ActivateIconGrab();
                    grabableObject = hit.transform.gameObject;
                    hasHit = true;
                }
                else if (hit.transform.gameObject.tag == "Drop" && grabObject != null && !hasHit)
                {
                    UIManager.Instance.ActivateIconDrop();
                    dropAreaRack = hit.transform.gameObject;
                    hasHit = true;
                }
                else if (hit.transform.gameObject.tag == "Diffuse" && grabObject != null && !hasHit)
                {
                    UIManager.Instance.ActivateIconDrop();
                    dropAreaDiffuse = hit.transform.gameObject;
                    hasHit = true;
                }
                else if (hit.transform.gameObject.tag == "Crusher" && grabObject != null && !hasHit)
                {
                    UIManager.Instance.ActivateIconDrop();
                    dropAreaCrusher = hit.transform.gameObject;
                    hasHit = true;
                }
                else if (hit.transform.gameObject.tag == "Spacecraft" && grabObject != null && !hasHit)
                {
                    UIManager.Instance.ActivateIconDrop();
                    dropAreaSpacecraft = hit.transform.gameObject;
                    hasHit = true;
                }
                else if (hit.transform.gameObject.tag == "Ray" && grabObject != null && !hasHit)
                {
                    UIManager.Instance.ActivateIconDrop();
                    dropAreaRay = hit.transform.gameObject;
                    hasHit = true;
                }
                else if (hit.transform.gameObject.tag == "Button" && !hasHit)
                {
                    UIManager.Instance.ActivateIconDrop();
                    canBePushed = true;
                    launcherScript = hit.transform.gameObject.GetComponent<Launcher>();
                    hasHit = true;
                }
                else if (hit.transform.gameObject.tag == "ButtonAll" && !hasHit)
                {
                    UIManager.Instance.ActivateIconDrop();
                    canBePushed = true;
                    button = hit.transform.gameObject.GetComponent<ButtonAll>();
                    hasHit = true;
                }
                else if (GameManager.Instance.lockPlayer && GameManager.GameState != GameManager.GameStates.isDiffusing && !hasHit)
                    NullRaycast();
                else if (GameManager.GameState != GameManager.GameStates.isDiffusing && !hasHit)
                {
                    NullRaycast();
                }
            }
        }

        if (Input.GetMouseButtonDown(0) && grabableObject != null && !GameManager.Instance.lockPlayer)
        {
            if (grabableObject.transform.parent != null)
            {
                grabableObject.transform.parent.GetComponent<DiffuseTable>().RetrieveBox(); //essayer de virer ici aussi
            }
            grabObject = grabableObject;
            grabObjectScript = grabObject.GetComponent<Box>();
            Rigidbody rBody = grabObject.GetComponent<Rigidbody>();
            rBody.velocity = Vector3.zero;
            rBody.angularVelocity = Vector3.zero;
            grabObject.transform.parent = this.transform;
            grabObject.transform.transform.localScale = new Vector3(1, 1, 1);
            grabObject.transform.transform.localRotation = Quaternion.Euler(0, 0, 0);
            grabObject.transform.localPosition = new Vector3(0, -0.8f, 1.35f);
            rBody.useGravity = false;
            grabObject.layer = 31;
            grabObject.GetComponent<BoxCollider>().enabled = false; //faire un tag BoxGrab et désactiver collision de ce tag
            grabObjectScript.isStored = false;
            if (grabObjectScript.isFragile)
            {
                UIManager.Instance.ActivateIconWalk();
                UIManager.Instance.ActivateFragileWarning();
            }
        }

        if (Input.GetMouseButtonDown(0) && grabObject != null && dropAreaRack != null)
        {
            grabObject.GetComponent<Box>().isStored = true;
            Drop(dropAreaRack);
        }
        else if (Input.GetMouseButtonDown(0) && grabObject != null && dropAreaDiffuse != null)
        {
            dropAreaDiffuse.GetComponent<DiffuseTable>().RecieveBox(grabObject);
            Drop(dropAreaDiffuse);
            NullRaycast();
        }
        else if (Input.GetMouseButtonDown(0) && grabObject != null && dropAreaCrusher != null)
        {
            dropAreaCrusher.GetComponent<Crusher>().RecieveBox(grabObject);
            Drop(dropAreaCrusher);
        }
        else if (Input.GetMouseButtonDown(0) && grabObject != null && dropAreaRay != null)
        {
            dropAreaRay.GetComponent<InspectionTable>().RecieveBox(grabObject);
            Drop(dropAreaRay);
        }

        if (Input.GetMouseButtonDown(0) && canBePushed == true && launcherScript != null)
        {
            launcherScript.Launch();
        }
        if (Input.GetMouseButtonDown(0) && canBePushed == true && button != null)
        {
            button.Push();
        }

        if (grabObject != null)
            if(grabObject.GetComponent<Box>().isFragile && player.isRunning == true)
            {
                grabObject.GetComponent<Box>().isBroken = true;
                grabObject.GetComponent<Box>().isFragile = false;
                if (GameManager.Instance.levelUnlock == 0)
                    Destroy(grabObject);
                SoundManager.Instance.Play("FragileBreak");
                GameManager.Instance.ChangeLife(-1);
            }
    }

    void Drop(GameObject dropArea)
    {
        grabObject.transform.parent = null;
        grabObject.GetComponent<BoxCollider>().enabled = true;
        grabObject.GetComponent<Rigidbody>().useGravity = true;
        grabObject.layer = 8;
        grabObject.transform.parent = dropArea.transform;
        grabObject.transform.transform.localRotation = Quaternion.Euler(0, 0, 0);
        grabObject.transform.localPosition = new Vector3(0, 1f, 0f);
        grabObject.transform.parent = null;
        grabObject.transform.transform.localScale = new Vector3(1, 1, 1);
        if (dropArea.GetComponent<DiffuseTable>() != null)
        {
            grabObject.transform.parent = dropArea.transform;
            dropArea.GetComponent<DiffuseTable>().DiffuseMod();
        }
        if (grabObjectScript.isFragile || grabObjectScript.isBroken)
            UIManager.Instance.DeactivateIconWalk();
        grabObject = null;
    }

    void NullRaycast()
    {
        UIManager.Instance.DeactivateIconGrab();
        UIManager.Instance.DeactivateIconDiffuse();
        canBePushed = false;
        launcherScript = null;
        button = null;
        grabableObject = null;
        dropAreaRack = null;
        dropAreaRay = null;
        dropAreaDiffuse = null;
        dropAreaCrusher = null;
        dropAreaSpacecraft = null;
    }
}
