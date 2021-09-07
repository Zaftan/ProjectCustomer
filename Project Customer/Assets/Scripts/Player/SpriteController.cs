using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour
{
    private Transform camPos;

    private void Start()
    {
        camPos = Camera.main.transform;
    }

    private void Update()
    {
        UpdateRotation();
    }

    private void UpdateRotation()
    {
        transform.LookAt(camPos);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }
}
