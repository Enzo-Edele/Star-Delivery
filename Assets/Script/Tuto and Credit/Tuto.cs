using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuto : MonoBehaviour
{
    public PlayerGrabDrop grab;
    public Spacecraft spacecraft;

    public enum TutoStates
    {
        TutoAState,
        TutoBState,
        TutoCState,
        TutoDState,
        TutoEState,
    }
    private static TutoStates tutoState;
    public static TutoStates TutoState;

    void Awake()
    {
        ChangeGameState(TutoStates.TutoAState);
    }

    public void ChangeGameState(TutoStates currentState)
    {
        tutoState = currentState;
        TutoState = tutoState;
        switch (tutoState)
        {
            case TutoStates.TutoAState:
                break;
            case TutoStates.TutoBState:
                break;
        }
    }

    private void Update()
    {
        TutoB();
        TutoC();

    }

    void TutoA()
    {
        //Pub (�ventuelle)
        //Lancement dialogue SOGLAD
        //Diriger le joueur vers la salle suivante
        //Ouverture porte A
    }
    
    void TutoB()
    {
        //Fermeture porte A
        //Apparition colis
        //Arret colis
        //Dialogue SOGLAD : attrape le colis
        if (grab.grabObject != null) //D�tection colis attrap�
        {
            Debug.Log("colis attrap�");
        }
    }

    void TutoC()
    {
        //Dialogue SOGLAD : diriger vers la navette
        if (spacecraft.packages == 1)//D�poser colis dans la navette
        {
            Debug.Log("colis livr�");
        }
        //Dialogue SOGLAD : explication information tablette + bouton lancement
        //D�tection navette lanc�
    }

    void TutoD()
    {
        //Apparition colis fragile
        //Dialogue SOGLAD : explication colis fragile
        //Indication touche de marche
        //*Si colis fragile cass� : nouvelle apparition colis fragile + Dialogue SOGLAD : Insulte
        //Dialogue SOGLAD : explication stockage colis
        //Indication Armoir � colis
        //D�tection colis stocker
    }

    void TutoE()
    {
        //Apparition colis avec bombe
        //Dialogue SOGLAD : explication bombe + table de d�samorcage
        //Indication table de d�samorcage
        //Dialogue SOGLAD : explication affiche pour d�samorcage (1 fil)
        //D�tection d�samorcage
        //Dialogue SOGLAD : destruction du colis
        //Indication destructeur de colis
        //D�tection colis d�truit
    }
}
