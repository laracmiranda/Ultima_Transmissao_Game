using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private Transform player;
    [SerializeField] private Transform playerSpawnPoint;
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

    // Mecânica para continuar jogo depois do gameover
    public void ContinueGame()
    {
        isGameOver = false;
        GameOverUI.Instance.Hide();
        RespawnPlayer();
        AttemptSystem.Instance.ResetAttemptEffects();
        AttemptSystem.Instance.ApplyAttemptEffects(naoAttempts);
    }

    // Mecânica de respawn do player para posição inicial
    private void RespawnPlayer()
    {
        player.position = playerSpawnPoint.position;
    }
}