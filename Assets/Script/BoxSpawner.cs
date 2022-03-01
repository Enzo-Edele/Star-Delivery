using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    [SerializeField]GameObject box;

    [SerializeField] float time;
    float timer;

    void Start()
    {
        timer = time;
    }

    void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = time;
            Instantiate(box, transform.position + new Vector3(1,0,0), Quaternion.identity);
        }
    }
}
