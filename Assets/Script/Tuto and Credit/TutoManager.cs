using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoManager : MonoBehaviour
{
    [SerializeField] int step;

    [SerializeField] int messages;
    [SerializeField] float timerMessage;

    [SerializeField] int doors;
    [SerializeField]GameObject[] door;

    [SerializeField] TutoBoxSpawner spawner;
    [SerializeField] PlayerGrabDrop grab;
    [SerializeField] Spacecraft spacecraft;

    void Start()
    {
        messages = 0;
        doors = 0;
        PlayAdvice();
    }
    /* step 0 : intro(0) explication déplacement
     * step 1 : ouverture porte(0)
     * step 2 : explication colis(1)
     * step 3 : close door(0)//via collider & spawn colis basique
     * step 4 : wait grab
     * step 5 : adresse(2)
     * step 6 : navette et tablette(3)
     * step 7 : dépose(4) & check dépot colis
     * step 8 : envoie(5) & check envoie
     * step 9 : fragile(6)
     * step 10 : spawn fragile & wait grab
     * step 11 : stockage(7) & wait player stock
     * step 12 : destroy(8)
     * step 13 : sapwn colis mauvaise adreese & wait grab
     * step 14 : wait destroy
     * step 15 : bombe(9)
     * step 16 : spawn bomb & wait destroy
     * step 17 : launch check level
     */
    void Update()
    {
        if (timerMessage > 0)
            timerMessage -= Time.deltaTime;
        else if (step == 1)
            OpenDoor();
        else if (step == 3)
        {
            CloseDoor();
            SpawnBox(true, false, false, false);
        }
        else if (step == 5)
            PlayAdvice();
        else if (step == 6)
            PlayAdvice();
        else if(step == 9)
            SpawnBox(true, true, false, false);

        if (grab.grabObject != null && step == 4)
        {
            PlayAdvice();
            Debug.Log("check grab");
        }
        if(spacecraft.packages == 1 && step == 7)
        {
            PlayAdvice();
        }
        if(spacecraft.delivered == true && step == 8)
        {
            PlayAdvice();
        }
        if(grab.grabObject != null && step == 10)
        {
            Debug.Log("check grab");
            PlayAdvice();
        }
    }

    void PlayAdvice()
    {
        timerMessage = SoundManager.Instance.PlayTime(messages);
        messages++;
        step++;
    }
    void OpenDoor()
    {
        Vector3 position = door[doors].transform.position;
        position.y -= 5;
        door[doors].transform.position = position;
        doors++;
        step++;
        if(step == 2)
        {
            PlayAdvice();
            return;
        }
    }
    void CloseDoor()
    {
        Vector3 position = door[doors - 1].transform.position;
        position.y += 5;
        door[doors - 1].transform.position = position;
    }

    void SpawnBox(bool valid, bool fragile, bool sus, bool bomb)
    {
        spawner.SpawnBox(valid, fragile, sus, bomb);
        step++;
    }

    public void ReDoStep(int step, int message)
    {
        this.step = step;
        this.messages = message;
    }
}
