using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game State")]
    public int naoAttempts = 0;

    public int CurrentAttempt
    {
        get
        {
            return naoAttempts;
        }
    }

    public const int MaxAttempts = 10;
    public bool isGameOver = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterNaoAttempt()
    {
        if (naoAttempts >= MaxAttempts)
            return;

        naoAttempts++;

        isGameOver = true;

        string message =
            GameTexts.NaoMessages[naoAttempts - 1];

        GameOverUI.Instance.Show(message);
    }

    public void ContinueGame()
    {
        isGameOver = false;

        GameOverUI.Instance.Hide();
    }
}