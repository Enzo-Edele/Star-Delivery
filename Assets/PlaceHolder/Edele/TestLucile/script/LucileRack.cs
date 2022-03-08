using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LucileRack : MonoBehaviour
{
    private void OnMouseDown()
    {
        LucileCharacter.Instance.DropBox(this.gameObject);
    }
}
