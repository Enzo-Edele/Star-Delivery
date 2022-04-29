using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltDoor : MonoBehaviour
{
    [SerializeField] GameObject door;
    public Vector3 amplitude;
    Vector3 pos;

    bool opened = false;

    void Open()
    {
        pos = door.transform.position;
        pos.y += amplitude.y;
        door.transform.position = pos;
        opened = true;
    }
    void Close()
    {
        pos = door.transform.position;
        pos.y -= amplitude.y;
        door.transform.position = pos;
        opened = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (opened)
            Close();
        else
            Open();
    }
}
