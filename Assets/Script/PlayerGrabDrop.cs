using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrabDrop : MonoBehaviour
{
    [SerializeField] Vector3 direction;
    float interactRange = 3;
    [SerializeField] RaycastHit hit;
    GameObject grabableObject;
    GameObject grabObject;
    GameObject dropAreaBelt;
    GameObject dropAreaRay;
    GameObject dropAreaDiffuse;
    GameObject dropAreaCrusher;
    GameObject dropAreaSpacecraft;


    void Start()
    {
        
    }

    void Update() //mettre pls raycast
    {
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z),
            transform.forward * interactRange,
            Color.red);

        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z),
            transform.forward,
            out hit,
            interactRange))
        {
            if (hit.transform.gameObject.tag == "Box" && grabObject == null)
            {
                UIManager.Instance.ActivateIconGrab();
                grabableObject = hit.transform.gameObject;
            }
            else if (hit.transform.gameObject.tag == "Drop" && grabObject != null)
            {
                UIManager.Instance.ActivateIconDrop();
                dropAreaBelt = hit.transform.gameObject;
            }
            else if (hit.transform.gameObject.tag == "Diffuse" && grabObject != null)
            {
                UIManager.Instance.ActivateIconDrop();
                dropAreaDiffuse = hit.transform.gameObject;
            }
            else if (hit.transform.gameObject.tag == "Crusher" && grabObject != null)
            {
                UIManager.Instance.ActivateIconDrop();
                dropAreaCrusher = hit.transform.gameObject;
            }
            else if (hit.transform.gameObject.tag == "Spacecraft" && grabObject != null)
            {
                UIManager.Instance.ActivateIconDrop();
                dropAreaSpacecraft = hit.transform.gameObject;
            }
            else if (hit.transform.gameObject.tag == "Ray" && grabObject != null)
            {
                UIManager.Instance.ActivateIconDrop();
                dropAreaRay = hit.transform.gameObject;
            }
        }
        else
        {
            UIManager.Instance.DeactivateIconGrab();
            grabableObject = null;
            dropAreaBelt = null;
            dropAreaRay = null;
            dropAreaDiffuse = null;
            dropAreaCrusher = null;
            dropAreaSpacecraft = null;
        }

        if (Input.GetMouseButtonDown(0) && grabableObject != null && !GameManager.Instance.lockPlayer)
        {
            if(grabableObject.transform.parent != null)
            {
                grabableObject.transform.parent.GetComponent<DiffuseTable>().RetrieveBox(); //essayer de virer ici aussi
            }
            grabObject = grabableObject;
            Rigidbody rBody = grabObject.GetComponent<Rigidbody>();
            rBody.velocity = Vector3.zero;
            rBody.angularVelocity = Vector3.zero;
            grabObject.transform.parent = this.transform;
            grabObject.transform.transform.localScale = new Vector3(1, 1, 1);
            grabObject.transform.transform.localRotation = Quaternion.Euler(0, 0, 0);
            grabObject.transform.localPosition = new Vector3(0, -0.8f, 1.35f);
            rBody.useGravity = false;
            grabObject.GetComponent<BoxCollider>().enabled = false; //faire un tag BoxGrab et désactiver collision de ce tag
            grabObject.GetComponent<Box>().DisplayInfo();
        }

        if (Input.GetMouseButtonDown(0) && grabObject != null && dropAreaBelt != null)
        {
            Drop(dropAreaBelt);
        }

        if (Input.GetMouseButtonDown(0) && grabObject != null && dropAreaDiffuse != null)
        {
            dropAreaDiffuse.GetComponent<DiffuseTable>().RecieveBox(grabObject);
            Drop(dropAreaDiffuse);
        }

        if (Input.GetMouseButtonDown(0) && grabObject != null && dropAreaCrusher != null)
        {
            dropAreaCrusher.GetComponent<Crusher>().RecieveBox(grabObject);
            Drop(dropAreaCrusher);
        }

        if (Input.GetMouseButtonDown(0) && grabObject != null && dropAreaRay != null)
        {
            dropAreaRay.GetComponent<InspectionTable>().RecieveBox(grabObject);
            Drop(dropAreaRay);
        }
    }

    void Drop(GameObject dropArea)
    {
        grabObject.transform.parent = null;
        grabObject.GetComponent<BoxCollider>().enabled = true;
        grabObject.GetComponent<Rigidbody>().useGravity = true;
        grabObject.transform.parent = dropArea.transform;
        grabObject.transform.transform.localRotation = Quaternion.Euler(0, 0, 0);
        grabObject.transform.localPosition = new Vector3(0, 0.6f, 0f);
        grabObject.transform.parent = null;
        grabObject.transform.transform.localScale = new Vector3(1, 1, 1);
        grabObject = null;
        UIManager.Instance.DeactivateBoxInfo();
    }
}
