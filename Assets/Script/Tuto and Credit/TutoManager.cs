using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoManager : MonoBehaviour
{
    [SerializeField] int step;

    [SerializeField] int messages;
    [SerializeField] float timerMessage;

    [SerializeField] int doors;
    [SerializeField] GameObject[] door;

    [SerializeField] TutoBoxSpawner spawner;
    [SerializeField] PlayerGrabDrop grab;
    [SerializeField] Spacecraft spacecraft;

    void Start()
    {
        messages = 0;
        doors = 0;
        PlayAdvice();
        UIManager.Instance.ActivateDialogue(TextManager.Instance.tutoDial[0], 7);
    }
    /* step 0 : intro(0) explication déplacement //mettre une lumiére pour pointer les élément clef
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
            SpawnBox(true, false, false, false, spacecraft.spacecraftDestination);
        }
        else if (step == 5)
        {
            PlayAdvice();
            UIManager.Instance.ActivateDialogue(TextManager.Instance.tutoDial[3], 15);
        }
        else if (step == 6)
        {
            PlayAdvice();
            UIManager.Instance.ActivateDialogue(TextManager.Instance.tutoDial[4], 15);
        }
        else if (step == 9)
            SpawnBox(true, true, false, false, spacecraft.spacecraftDestination);
        else if (step == 12)
            SpawnBox(false, false, false, false, null);
        else if (step == 14)
            SpawnBox(true, false, true, true, GameManager.Instance.invalidDestinationLevel[Random.Range(0, 2)]);
        else if (step == 16)
            SpawnBox(true, false, true, false, spacecraft.spacecraftDestination);

        if (grab.grabObject != null && step == 4)
        {
            PlayAdvice();
            UIManager.Instance.ActivateDialogue(TextManager.Instance.tutoDial[2], 10);
        }
        if (spacecraft.packages == 1 && step == 7)
        {
            PlayAdvice();
            UIManager.Instance.ActivateDialogue(TextManager.Instance.tutoDial[5], 15);
        }
        if (spacecraft.delivered == true && step == 8)
        {
            PlayAdvice();
            UIManager.Instance.ActivateDialogue(TextManager.Instance.tutoDial[6], 15);

        }
        if (grab.grabObject != null && step == 10)
        {
            PlayAdvice();
            UIManager.Instance.ActivateDialogue(TextManager.Instance.tutoDial[7], 15);
        }
        if (spawner.package != null)
            if(spawner.package.GetComponent<Box>().isStored && step == 11)
            {
                PlayAdvice();
                UIManager.Instance.ActivateDialogue(TextManager.Instance.tutoDial[8], 15);
            }
        if (spawner.package == null && step == 13)
        {
            PlayAdvice();
            UIManager.Instance.ActivateDialogue(TextManager.Instance.tutoDial[9], 15);
        }
        if (spawner.package == null && step == 15)
        {
            PlayAdvice();
            UIManager.Instance.ActivateDialogue(TextManager.Instance.tutoDial[10], 15);
        }
        if (spawner.package == null && step == 17)
        {
            UIManager.Instance.ActivateDialogue(TextManager.Instance.tutoDial[11], 15);
            OpenDoor();
        }
    }

    void PlayAdvice()
    {
        timerMessage = SoundManager.Instance.PlayTuto(messages);
        messages++;
        step++;
    }
    void OpenDoor()
    {
        /*
        Vector3 position = door[doors].transform.position;
        position.y -= 5;
        door[doors].transform.position = position;
        */
        //anim ouverture + Collider
        door[doors].GetComponent<BoxCollider>().enabled = false;
        door[doors].GetComponent<Door>().Open();
        doors++;
        step++;
        if(step == 2)
        {
            PlayAdvice();
            UIManager.Instance.ActivateDialogue(TextManager.Instance.tutoDial[1], 10);
            return;
        }
    }
    //remplacer par script indépendant
    /*
    void CloseDoor()
    {
        Vector3 position = door[doors - 1].transform.position;
        position.y += 5;
        door[doors - 1].transform.position = position;
    }
    */

    void SpawnBox(bool valid, bool fragile, bool sus, bool bomb, string destination)
    {
        spawner.SpawnBox(valid, fragile, sus, bomb, destination);
        step++;
    }

    public void ReDoStep(int step, int message)
    {
        this.step = step;
        this.messages = message;
        UIManager.Instance.DeactivateIconWalk(); //mettre if si on reuse la fct
    }
}
