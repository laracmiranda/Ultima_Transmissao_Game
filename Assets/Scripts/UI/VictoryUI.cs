using TMPro;
using UnityEngine;

public class VictoryUI : MonoBehaviour
{
    public static VictoryUI Instance;
    private bool showingVictory;

    // Referências
    [SerializeField] private GameObject panel;

    [SerializeField] private TMP_Text victoryText;
    [SerializeField] private TypewriterEffect typewriterEffect;

    private void Awake()
    {
        Instance = this;
    }

    public void Show(string message)
    {
        InteractionUI.Instance.Hide();
        
        panel.SetActive(true);

        typewriterEffect.ShowText(message);

        showingVictory = true;
    }

    private void Update()
    {
        if (!showingVictory)
            return;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            showingVictory = false;

            Hide();

            CreditsUI.Instance.Show();
        }
    }
    
    public void Hide()
    {
        panel.SetActive(false);
    }
}