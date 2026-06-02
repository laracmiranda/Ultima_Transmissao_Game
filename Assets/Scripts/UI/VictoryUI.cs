using TMPro;
using UnityEngine;

public class VictoryUI : MonoBehaviour
{
    public static VictoryUI Instance;

    // Referências
    [SerializeField] private GameObject panel;

    [SerializeField] private TMP_Text victoryText;

    private void Awake()
    {
        Instance = this;
    }

    public void Show(string message)
    {
        panel.SetActive(true);

        victoryText.text = message;
    }

    public void Hide()
    {
        panel.SetActive(false);
    }
}