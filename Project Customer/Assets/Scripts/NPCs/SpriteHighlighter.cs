using UnityEngine;

public class SpriteHighlighter : MonoBehaviour
{
    private Sprite baseSprite;
    [SerializeField] private Sprite highlightSprite;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        baseSprite = spriteRenderer.sprite;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            spriteRenderer.sprite = highlightSprite;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            spriteRenderer.sprite = baseSprite;
        }
    }
}
