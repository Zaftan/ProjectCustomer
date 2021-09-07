using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour
{
    private Transform camPos;

    //flip state
    private bool flipped;
    [SerializeField] private float minFlipValue;

    private void Start()
    {
        camPos = Camera.main.transform;
    }

    private void Update()
    {
        UpdateRotation();
        MovementCheck();
    }

    private void UpdateRotation()
    {
        transform.LookAt(camPos);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        //add 180 because sprites are meant to be viewed from behind
    }

    private void MovementCheck()
    {
        //check movement to see if sprite should be flipped
        float xMovement = Input.GetAxis("Horizontal");
        if (flipped && xMovement > minFlipValue || !flipped && xMovement < -minFlipValue)
        {
            Flip();
        }
    }

    private void Flip()
    {
        //flip sprite
        flipped = !flipped;
        transform.localScale = new Vector3(flipped? -1: 1, 1, 1);
    }
}
