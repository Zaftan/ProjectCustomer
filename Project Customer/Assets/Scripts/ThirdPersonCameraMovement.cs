using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraMovement : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform body;

    float xAxisRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xAxisRotation -= mouseY;
        xAxisRotation = Mathf.Clamp(xAxisRotation, 0f, 10f);

        transform.localRotation = Quaternion.Euler(xAxisRotation, 0f, 0f);
        body.Rotate(Vector3.up * mouseX);
    }
}