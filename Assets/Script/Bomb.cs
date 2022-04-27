using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField]BoxCollider bCollider;
    GameObject parent;
    public DiffuseTable diffuseTable;
    public List<GameObject> buttons;

    void Start()
    {
        parent = transform.parent.gameObject;
    }
    /*private void OnMouseDown()
    {
        parent.GetComponent<Box>().Diffuse();
    }*/

    public void Diffuse(int index)
    {
        if(index == diffuseTable.combination[diffuseTable.step])
        {
            diffuseTable.step++;
            SoundManager.Instance.Play("Boop");
            if (diffuseTable.step == diffuseTable.combinationLength)
            {
                parent.GetComponent<Box>().Diffuse();
            }
        }
        else
            SoundManager.Instance.Play("explosion");
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
