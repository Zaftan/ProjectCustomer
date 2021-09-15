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

    //audio
    [Header("Audio")]
    [SerializeField] private AudioClip doorOpenClip;
    [SerializeField] private AudioClip doorCloseClip;
    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void Open(float startDelay)
    {
        StartCoroutine(OpenCo(startDelay));
    }

    private IEnumerator OpenCo(float startDelay)
    {
        yield return new WaitForSeconds(startDelay);
        //open door audio
        PlayAudio(doorOpenClip);
        //open door
        targetRotation = Quaternion.Euler(0, maxAngle, 0);
        StartCoroutine(RotateCo());
        yield return new WaitForSeconds(delay);
        //close door audio
        PlayAudio(doorCloseClip);
        //close door
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

    private void PlayAudio(AudioClip clip)
    {
        source.clip = clip;
        source.Play();
    }
}