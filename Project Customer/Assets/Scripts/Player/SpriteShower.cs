using UnityEngine;

public class SpriteShower : MonoBehaviour
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
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        //add 180 because sprites are meant to be viewed from behind
    }
}
