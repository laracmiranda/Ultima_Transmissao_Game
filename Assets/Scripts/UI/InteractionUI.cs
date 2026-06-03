using TMPro;
using UnityEngine;

public class InteractionUI : MonoBehaviour
{
    public static InteractionUI Instance;

    [SerializeField] private GameObject interactionText;

    private void Awake()
    {
        Instance = this;
    }

    public void Show()
    {
        interactionText.SetActive(true);
    }

    public void Hide()
    {
        interactionText.SetActive(false);
    }
}