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
        //Pub (éventuelle)
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
        if (grab.grabObject != null) //Détection colis attrapé
        {
            Debug.Log("colis attrapé");
        }
    }

    void TutoC()
    {
        //Dialogue SOGLAD : diriger vers la navette
        if (spacecraft.packages == 1)//Déposer colis dans la navette
        {
            Debug.Log("colis livré");
        }
        //Dialogue SOGLAD : explication information tablette + bouton lancement
        //Détection navette lancé
    }

    void TutoD()
    {
        //Apparition colis fragile
        //Dialogue SOGLAD : explication colis fragile
        //Indication touche de marche
        //*Si colis fragile cassé : nouvelle apparition colis fragile + Dialogue SOGLAD : Insulte
        //Dialogue SOGLAD : explication stockage colis
        //Indication Armoir à colis
        //Détection colis stocker
    }

    void TutoE()
    {
        //Apparition colis avec bombe
        //Dialogue SOGLAD : explication bombe + table de désamorcage
        //Indication table de désamorcage
        //Dialogue SOGLAD : explication affiche pour désamorcage (1 fil)
        //Détection désamorcage
        //Dialogue SOGLAD : destruction du colis
        //Indication destructeur de colis
        //Détection colis détruit
    }
}
