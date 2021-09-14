using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Sensitivity Setting")]
    [SerializeField] private float mouseSensitivity = 100f;

    [Header("Camera Angle Restrictions")]
    [SerializeField] private float minAngle;
    [SerializeField] private float maxAngle;

    private Transform body;
    private PlayerMovement player;

    private float xAxisRotation = 0f;

    void Start()
    {
        body = transform.parent;
        player = body.GetComponent<PlayerMovement>();
        //set cursor mode
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (!player.dialogueUI.isOpen) //talking? freeze camera!
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xAxisRotation -= mouseY;
            xAxisRotation = Mathf.Clamp(xAxisRotation, minAngle, maxAngle);

            transform.localRotation = Quaternion.Euler(xAxisRotation, 0f, 0f);
            body.Rotate(Vector3.up * mouseX);
        }
    }
}