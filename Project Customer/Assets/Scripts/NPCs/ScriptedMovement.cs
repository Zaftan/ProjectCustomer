using System.Collections;
using UnityEngine;

public class ScriptedMovement : MonoBehaviour
{
    //position data
    [SerializeField] private Transform[] keyPositions;
    private int targetPos = 0;

    [SerializeField] private float moveSpeed, rotateSpeed;

    public void Move()
    {
        StartCoroutine(MoveToNextPos());
        StartCoroutine(RotateToNextRotation());
    }

    private IEnumerator MoveToNextPos()
    {
        bool reached = false;
        while (!reached)
        {
            //move towards target
            transform.position = Vector3.MoveTowards(transform.position, keyPositions[targetPos].position, moveSpeed * Time.deltaTime);
            //on reach target
            reached = transform.position == keyPositions[targetPos].position;
            //wait frame
            yield return null;
        }
        targetPos++;
    }

    private IEnumerator RotateToNextRotation()
    {
        float distance = (transform.eulerAngles - keyPositions[targetPos].eulerAngles).magnitude;
        float totalTime = distance / rotateSpeed;
        //make sure target isn't changed from outside
        int target = targetPos;

        for (float t = 0; t < 1; t += Time.deltaTime / totalTime)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, keyPositions[target].rotation, t);
            yield return null;
        }
        transform.rotation = keyPositions[target].rotation;
    }
}