using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField]BoxCollider bCollider;
    GameObject parent;
    public DiffuseTable diffuseTable;
    public List<GameObject> buttons;
    public float timerDiffuse;

    void Start()
    {
        parent = transform.parent.gameObject;
        timerDiffuse = 0;
    }
    private void Update()
    {
        if (timerDiffuse > 0)
            timerDiffuse -= Time.deltaTime;
        else if(timerDiffuse < 0)
        {
            timerDiffuse = 0;
            diffuseTable.QuitDiffuseMod();
            gameObject.SetActive(false);
        }
    }

    public void Diffuse(int index)
    {
        if (index == diffuseTable.combination[diffuseTable.step])
        {
            diffuseTable.numbers[diffuseTable.step].material.color = Color.green;
            diffuseTable.numbers[diffuseTable.step].material.SetColor("_EmissionColor", Color.green);
            diffuseTable.step++;
            SoundManager.Instance.Play("Boop");
            if (diffuseTable.step == diffuseTable.combinationLength)
            {
                diffuseTable.Randomise();
                parent.GetComponent<Box>().Diffuse();
                timerDiffuse = SoundManager.Instance.PlayTime("Disarmed") / 2;
            }
        }
        else
        {
            SoundManager.Instance.Play("explosion");
            diffuseTable.numbers[diffuseTable.step].material.color = Color.red;
            diffuseTable.numbers[diffuseTable.step].material.SetColor("_EmissionColor", Color.red);
            GameManager.Instance.ChangeLife(GameManager.Instance.lives * -1);
            for (int i = 0; i < buttons.Count; i++)
                Destroy(buttons[i]);
            Destroy(parent);
            Destroy(gameObject);
        }
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
