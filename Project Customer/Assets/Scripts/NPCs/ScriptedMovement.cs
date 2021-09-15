using System.Collections;
using UnityEngine;

public class ScriptedMovement : MonoBehaviour
{
    //position data
    [SerializeField] private Transform[] keyPositions;
    private int targetPos = 0;

    [SerializeField] private bool useRotation = true;
    [SerializeField] private float moveSpeed, rotateSpeed;
    //allow multiple steps in 1 call
    private int stepsToTake = 0;
    private bool stepped = false;

    public void Move(int steps)
    {
        //loop targetPos
        if (targetPos == keyPositions.Length)
        {
            targetPos = 0;
        }
        stepsToTake = steps;
        StartStep();
    }

    public void ResetTargetPos()
    {
        targetPos = 0;
    }
    public void SetPosition(int toAdd)
    {
        StartCoroutine(SetPositionCo(toAdd - 1));
    }
    private IEnumerator SetPositionCo(int toAdd)
    {
        yield return new WaitForSeconds(2f);
        targetPos += toAdd;
        //loop index over keyposition array
        if (targetPos >= keyPositions.Length) targetPos -= keyPositions.Length;
        if (targetPos < 0) targetPos += keyPositions.Length;
        //set position
        transform.position = keyPositions[targetPos].position;
    }

    private void StartStep()
    {
        StartCoroutine(MoveToNextPos());
        if (useRotation)
        {
            StartCoroutine(RotateToNextRotation());
        }
        else
        {
            EndMovementPart();
        }
    }

    private IEnumerator MoveToNextPos()
    {
        bool reached = false;
        while (!reached)
        {
            //move towards target
            transform.position = Vector3.MoveTowards(transform.position, keyPositions[targetPos].position, moveSpeed * Time.deltaTime);
            //on reach target
            reached = Vector3.Distance(transform.position, keyPositions[targetPos].position) < 0.1f;
            //wait frame
            yield return null;
        }
        targetPos++;
        EndMovementPart();
    }

    private IEnumerator RotateToNextRotation()
    {
        int target = targetPos;
        Quaternion targetRotation = keyPositions[target].rotation;
        while (Quaternion.Angle(transform.rotation, targetRotation) > 1f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
            yield return null;
        }
        transform.rotation = targetRotation;
    }

    private void EndMovementPart()
    { //method needs to be called twice to end the step
        stepped = !stepped;
        if (!stepped)
        {
            EndStep();
        }
    }

    private void EndStep()
    {
        stepsToTake--;
        if (stepsToTake > 0)
        { //take more steps
            if (targetPos >= keyPositions.Length)
            { //loop
                targetPos = 0;
            }
            StartStep();
        }
    }
}