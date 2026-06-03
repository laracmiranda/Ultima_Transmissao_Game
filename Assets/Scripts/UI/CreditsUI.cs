using UnityEngine;

public class CreditsUI : MonoBehaviour
{
    public static CreditsUI Instance;
    private bool showingCredits;

    [SerializeField] private GameObject panel;

    private void Awake()
    {
        Instance = this;
    }

    public void Show()
    {
        panel.SetActive(true);
        showingCredits = true;
    }

    // Jogo encerra após exibição dos créditos
    private void Update()
    {
        if (!showingCredits)
            return;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            QuitGame();
        }
    }

    private void QuitGame()
    {
        Debug.Log("Fechando jogo...");

        Application.Quit();
    }

    public void Hide()
    {
        panel.SetActive(false);
    }
}