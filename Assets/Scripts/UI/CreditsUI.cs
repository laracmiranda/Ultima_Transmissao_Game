using UnityEngine;
using TMPro;
using System.Collections;

public class CreditsUI : MonoBehaviour
{
    public static CreditsUI Instance;
    private bool showingCredits;

    private Vector2 initialCreditsPosition;

    private bool canExit;

    [SerializeField] private GameObject panel;

    [SerializeField] private RectTransform creditsText;
    [SerializeField] private GameObject quitText;
    [SerializeField] private float scrollSpeed = 80f;
    [SerializeField] private float targetY = 0f;

    private void Awake()
    {
        Instance = this;

        initialCreditsPosition = creditsText.anchoredPosition;
    }

    public void Show()
    {
        panel.SetActive(true);

        creditsText.anchoredPosition = initialCreditsPosition;

        showingCredits = true;
        quitText.SetActive(false);

        canExit = false;
        StartCoroutine(ScrollCredits());
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

    private IEnumerator ScrollCredits()
    {
        while (
            creditsText.anchoredPosition.y <
            targetY
        )
        {
            creditsText.anchoredPosition +=
                Vector2.up *
                scrollSpeed *
                Time.deltaTime;

            yield return null;
        }

        creditsText.anchoredPosition =
            new Vector2(
                creditsText.anchoredPosition.x,
                targetY
            );

        quitText.SetActive(true);

        EnableExit();
    }
}