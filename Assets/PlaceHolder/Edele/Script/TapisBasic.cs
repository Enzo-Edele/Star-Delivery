using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapisBasic : MonoBehaviour
{
    public float speed;
    public Vector3 direction;
    public List<GameObject> onBelt;

    void Start()
    {
        
    }

    void Update()
    {
        for (int i = 0; i < onBelt.Count; i++)
            onBelt[i].GetComponent<Rigidbody>().velocity = speed * direction * Time.deltaTime;
    }
    public void OnCollisionEnter(Collision collision)
    {
        onBelt.Add(collision.gameObject);
    }
    private void OnCollisionExit(Collision collision)
    {
        onBelt.Remove(collision.gameObject);
    }
}
