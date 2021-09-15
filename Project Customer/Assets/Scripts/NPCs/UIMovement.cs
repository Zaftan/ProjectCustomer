using System.Collections;
using UnityEngine;

public class UIMovement : MonoBehaviour
{
    [SerializeField] private Vector2[] positions;
    [SerializeField] private float moveSpeed;

    private int targetPos = 0;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void Move(RectTransform rect)
    {
        StartCoroutine(MoveCo(rect));
    }
    private IEnumerator MoveCo(RectTransform rect)
    {
        int pos = targetPos;
        //update targetPos
        targetPos++;
        if (targetPos >= positions.Length) targetPos = 0;
        //move object
        while (Vector2.Distance(rect.anchoredPosition, positions[pos]) > 1f)
        {
            rect.anchoredPosition = Vector2.Lerp(rect.anchoredPosition, positions[pos], moveSpeed * Time.deltaTime);
            yield return null;
        }
        rect.anchoredPosition = positions[pos];
    }

    //help functions
    public void WaitSetActive(float delay)
    {
        StartCoroutine(WaitSetAcitveCo(delay));
    }
    private IEnumerator WaitSetAcitveCo(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }

    public void SetCursorMode(int mode)
    {
        switch (mode)
        {
            case 0:
                Cursor.lockState = CursorLockMode.None;
                break;

            default:
                Cursor.lockState = CursorLockMode.Locked;
                break;
        }
        
    }
}
