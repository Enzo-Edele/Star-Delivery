using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBomb : MonoBehaviour
{
    [SerializeField] int index;
    [SerializeField] Bomb bomb;

    private void OnMouseDown()
    {
        bomb.Diffuse(index);
    }
}
