using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator doorAnimation;
    void Start()
    {
        doorAnimation.enabled = false;
    }
    public void Open()
    {
        doorAnimation.enabled = true;
        doorAnimation.Play("Open");
    }
    public void Close()
    {
        doorAnimation.enabled = true;
        doorAnimation.Play("Close");
    }
}
