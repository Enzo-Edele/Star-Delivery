using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float walkSpeed = 6f;
    public float gravity = -9.81f;
    public float jumpHeight = 3;

    public Transform groudCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;
    public bool isRunning;

    void Update()
    {
        isGrounded = Physics.CheckSphere(groudCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }

        if (!GameManager.Instance.lockPlayer)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            if (Input.GetKey(KeyCode.LeftShift)) {
                controller.Move(move * walkSpeed * Time.deltaTime);
            }
            else {
                controller.Move(move * speed * Time.deltaTime);
            }
            if ((Input.GetKeyDown("z") ||
               Input.GetKeyDown("s") ||
               Input.GetKeyDown("q") ||
               Input.GetKeyDown("d")) &&
               !Input.GetKey(KeyCode.LeftShift))
            {
                isRunning = true;
            }
            else
            {
                isRunning = false;
            }

            if (Input.GetButtonDown("Jump") && isGrounded) {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
