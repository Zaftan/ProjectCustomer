using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour
{
    //flip state
    private bool flipped;
    [SerializeField] private float flipThreshold;

    private void Update()
    {
        MovementCheck();
    }

    private void MovementCheck()
    {
        //check movement to see if sprite should be flipped
        float xMovement = Input.GetAxis("Horizontal");
        if (flipped && xMovement > flipThreshold || !flipped && xMovement < -flipThreshold)
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
