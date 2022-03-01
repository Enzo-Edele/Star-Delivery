using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField]BoxCollider bCollider;
    GameObject parent;

    void Start()
    {
        parent = transform.parent.gameObject;
    }
    private void OnMouseDown()
    {
        parent.GetComponent<Box>().Diffuse();
    }

    public void ActiveCollider()
    {
        bCollider.enabled = true;
    }

    public void DeactiveCollider()
    {
        if (bCollider != null)
            bCollider.enabled = false;
    }
}
