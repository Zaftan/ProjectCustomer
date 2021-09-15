using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Polariod : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Vector2 startPosition, stopPosition;
    private RectTransform rect;
    private Image image;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        rect.anchoredPosition = startPosition;
        image = GetComponent<Image>();
    }

    public void ShowPolariod(Sprite sprite)
    {
        image.sprite = sprite;
        rect.anchoredPosition = startPosition;
        StartCoroutine(ShowCo());
    }

    private IEnumerator ShowCo()
    {
        bool start = true;
        //go down
        while (rect.anchoredPosition.y > 0 - rect.sizeDelta.y)
        {
            if (start)
            {
                rect.anchoredPosition = Vector2.Lerp(rect.anchoredPosition, stopPosition, moveSpeed);
                if (rect.anchoredPosition.y - stopPosition.y < 0.1f)
                {
                    start = false;
                    yield return new WaitUntil(() => Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space));
                }
            }
            else
            {
                rect.anchoredPosition = Vector2.Lerp(rect.anchoredPosition, new Vector2(0, -100 - rect.sizeDelta.y), moveSpeed);
            }
            yield return null;
        }
    }
}