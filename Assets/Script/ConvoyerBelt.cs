using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvoyerBelt : MonoBehaviour
{
    Rigidbody rigidBody;
    [SerializeField] float speed;
    Vector3 position;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        position = rigidBody.position;
    }

    void FixedUpdate()
    {
        rigidBody.position += (transform.forward * -1) * speed * Time.fixedDeltaTime;
        rigidBody.MovePosition(position);
    }
}
