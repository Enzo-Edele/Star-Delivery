using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LucileCrusher : MonoBehaviour
{
    GameObject box;
    private void OnMouseDown()
    {
        if (LucileCharacter.Instance.grabObject != null)
        {
            box = LucileCharacter.Instance.grabObject;
            if (box.GetComponent<LucileBox>().isArmed)
                Debug.Log("boom");
            else
                Destroy(box);
        }
    }
}
