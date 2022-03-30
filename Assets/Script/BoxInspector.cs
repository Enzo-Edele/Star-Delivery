using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BoxInspector : MonoBehaviour, IDragHandler
{
    [SerializeField]GameObject screen;

    GameObject inspected;

    public static BoxInspector Instance { get; private set; }
    void Awake()
    {
        Instance = this;
    }
    public void DisplayInspector(GameObject box)
    {
        screen.SetActive(true);
        if (inspected != null)
            inspected = null;
        inspected = Instantiate(box, new Vector3(100, 100, 100), Quaternion.identity);
        GameManager.Instance.LockPlayer();
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void HideInspector()
    {
        if(screen != null)
            screen.SetActive(false);
        GameManager.Instance.UnlockPlayer();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnDrag(PointerEventData eventData)
    {
        inspected.transform.eulerAngles += new Vector3(eventData.delta.y, -eventData.delta.x);
    }
}
