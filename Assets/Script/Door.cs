using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] float timeOpen;
    float timerOpen = 0;
    public Animator doorAnimation;

    private void Start()
    {
        doorAnimation.enabled = false;
    }

    private void Update()
    {
        if (timerOpen > 0 && !GameManager.Instance.gameIsPause)
            timerOpen -= Time.deltaTime;
        else if (timerOpen < 0)
        {
            timerOpen = 0;
            PackagesDoor(false);
        }
    }

    public void PackagesDoor(bool state)
    {
        doorAnimation.enabled = true;
        if (state == true)
        {
            doorAnimation.Play("OpenDoor");
            timerOpen = timeOpen;
        }
        else
        {
            doorAnimation.Play("CloseDoor");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        PackagesDoor(true);
    }
}
