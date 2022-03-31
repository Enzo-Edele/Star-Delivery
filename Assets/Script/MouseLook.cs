using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Transform playerBody;

    float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = 0;
        float mouseY = 0;
        if (!GameManager.Instance.lockPlayer)
        {
            mouseX = Input.GetAxis("Mouse X") * GameManager.Instance.mouseSensitivity * Time.deltaTime;
            mouseY = Input.GetAxis("Mouse Y") * GameManager.Instance.mouseSensitivity * Time.deltaTime;
        }
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouseX);
    }
}
