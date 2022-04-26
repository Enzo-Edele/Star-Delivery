using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTuto : MonoBehaviour
{
    [SerializeField] TutoBoxSpawner spawner;

    public void StartTuto()
    {
        spawner.StartLevel();
        Vector3 pos = gameObject.transform.localPosition;
        pos.y -= 0.08f;
        gameObject.transform.localPosition = pos;
    }
}
