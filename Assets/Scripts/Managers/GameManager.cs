using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private Transform player;
    [SerializeField] private Transform playerSpawnPoint;
    [Header("Game State")]
    public int naoAttempts = 0;

    private bool retryCurrentAttempt;

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

    // Confere se o tempo se esgotou no timer - Tentativas 5+
    public void RegisterTimeOut()
    {
        isGameOver = true;

        TimerManager.Instance.StopTimer();

        GameOverUI.Instance.Show(
            "O tempo acabou...\nVou assumir que isso foi um não."
        );

        retryCurrentAttempt = true;
    }

    public void RegisterNaoAttempt()
    {
        if (naoAttempts >= MaxAttempts)
            return;

        naoAttempts++;

        isGameOver = true;
        TimerManager.Instance.StopTimer();

        string message =
            GameTexts.NaoMessages[naoAttempts - 1];

        GameOverUI.Instance.Show(message);
    }

    public void RegisterLaserHit()
    {

        if (isGameOver)
            return;
            
        isGameOver = true;

        AttemptSystem.Instance.DisableAllLasers();

        TimerManager.Instance.StopTimer();
        
        GameOverUI.Instance.Show(
            "Nem tente passar por aí.."
        );

        retryCurrentAttempt = true;
    }

    // Mecânica para continuar jogo depois do gameover
    public void ContinueGame()
    {

        // Reseta a tentativa atual caso o tempo acabar - Tentativas 5+
        if (retryCurrentAttempt)
        {
            retryCurrentAttempt = false;
            isGameOver = false;
            GameOverUI.Instance.Hide();
            RespawnPlayer();
            AttemptSystem.Instance.ResetAttemptEffects();
            AttemptSystem.Instance.ForceReapplyCurrentAttempt();
            AttemptSystem.Instance.ApplyAttemptEffects(naoAttempts);
            TimerManager.Instance.StartTimer();
            return;
        }

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