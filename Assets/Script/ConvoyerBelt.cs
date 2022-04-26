using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvoyerBelt : MonoBehaviour
{
    Rigidbody rigidBody;
    public float speed;
    public bool isOn;
    Vector3 position;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        position = rigidBody.position;
        isOn = true;
    }

    void FixedUpdate()
    {
        if (!GameManager.Instance.gameIsPause && isOn)
        {
            rigidBody.position += (transform.forward * -1) * speed * Time.fixedDeltaTime;
            rigidBody.MovePosition(position);
        }
    }
}
