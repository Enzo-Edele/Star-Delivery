using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator doorAnimation;

    private void Start()
    {
        doorAnimation.enabled = false;
    }

    public void PackagesDoor(bool state)
    {
        doorAnimation.enabled = true;
        if (state == true)
        {
            doorAnimation.Play("OpenDoor");
        }
        else
        {
            doorAnimation.Play("CloseDoor");
        }
    }
}
