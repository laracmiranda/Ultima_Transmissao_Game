using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    public static GameOverUI Instance;

    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text gameOverText;

    private void Awake()
    {
        Instance = this;
    }

    public void Show(string message)
    {
        panel.SetActive(true);
        gameOverText.text = message;
    }

    public void Hide()
    {
        panel.SetActive(false);
    }

    private void Update()
    {
        if (!panel.activeSelf)
            return;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            GameManager.Instance.ContinueGame();
        }
    }
}