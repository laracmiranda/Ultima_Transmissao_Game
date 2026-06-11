using UnityEngine;

public class ButtonVisual : MonoBehaviour
{
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite clickedSprite;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer =
            GetComponent<SpriteRenderer>();
    }

    public void SetClicked()
    {
        spriteRenderer.sprite =
            clickedSprite;
    }

    public void ResetButton()
    {
        spriteRenderer.sprite =
            normalSprite;
    }
}