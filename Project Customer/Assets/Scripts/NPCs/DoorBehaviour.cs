using System.Collections;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    private Quaternion targetRotation;
    //rotation data
    [Header("Rotation data")]
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float maxAngle;

    //timings
    [Header("Timings")]
    [SerializeField] private float delay;

    public void Open(float startDelay)
    {
        StartCoroutine(OpenCo(startDelay));
    }

    private IEnumerator OpenCo(float startDelay)
    {
        yield return new WaitForSeconds(startDelay);
        targetRotation = Quaternion.Euler(0, maxAngle, 0);
        StartCoroutine(RotateCo());
        yield return new WaitForSeconds(delay);
        targetRotation = Quaternion.identity;
        StartCoroutine(RotateCo());
    }

    private IEnumerator RotateCo()
    {
        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            Rotate();
            yield return null;
        }
    }

    private void Rotate()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }
}