using UnityEngine;

public class CreditsUI : MonoBehaviour
{
    public static CreditsUI Instance;
    private bool showingCredits;

    private bool canExit;


    [SerializeField] private GameObject panel;

    private void Awake()
    {
        Instance = this;
    }

    public void Show()
    {
        panel.SetActive(true);
        showingCredits = true;

        canExit = false;
        Invoke(nameof(EnableExit), 0.3f);
    }

    // Jogo encerra após exibição dos créditos
    private void Update()
    {
        if (!showingCredits)
            return;

        if (!canExit)
            return;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            QuitGame();
        }
    }

    private void EnableExit()
    {
        canExit = true;
    }

    private void QuitGame()
    {
        Debug.Log("Fechando jogo...");

        Application.Quit();
    }

    public void Hide()
    {
        panel.SetActive(false);
        showingCredits = false;
        canExit = false;
    }
}