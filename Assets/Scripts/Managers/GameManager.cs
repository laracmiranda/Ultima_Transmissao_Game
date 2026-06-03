using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private Transform player;
    [SerializeField] private Transform playerSpawnPoint;

    [SerializeField] private HoleAnimation holeAnimation;

    private Vector3 originalPlayerScale;

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
            originalPlayerScale = player.localScale;
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

    // Registra as tentativas de apertar no botão NÃO
    public void RegisterNaoAttempt()
    {
        StartCoroutine(RegisterNaoAttemptRoutine());
    }

    private IEnumerator RegisterNaoAttemptRoutine()
    {
        if (naoAttempts >= MaxAttempts)
            yield break;

        naoAttempts++;

        isGameOver = true;

        TimerManager.Instance.StopTimer();

        yield return StartCoroutine(
            holeAnimation.PlayHoleAnimation()
        );

        yield return new WaitForSeconds(0.5f);

        yield return StartCoroutine(
            FallAnimation()
        );

        yield return new WaitForSeconds(0.3f);

        string message =
            GameTexts.NaoMessages[naoAttempts - 1];

        GameOverUI.Instance.Show(message);
    }

    // Registra em qual tentativa o botão SIM foi pressionado
    public void RegisterSimChoice()
    {
        isGameOver = true;
        AttemptSystem.Instance.DisableAllLasers();
        TimerManager.Instance.StopTimer();

        int messageIndex;

        // Verifica se está na tentativa final para exibir a mensagem correta
        if (naoAttempts == 10)
        {
            messageIndex = 9;
        } else
        {
            messageIndex = naoAttempts;
        }

        VictoryUI.Instance.Show(
            GameTexts.SimMessages[messageIndex]
        );
    }

    // GameOver personalizado para caso o player seja atingido pelo laser
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

    // Animação de queda após apertar o botão NÃO
    private IEnumerator FallAnimation()
    {
        float duration = 0.5f;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float progress =
                elapsed / duration;

            player.localScale =
                Vector3.Lerp(
                    originalPlayerScale,
                    Vector3.zero,
                    progress
                );

            yield return null;
        }

        player.localScale = Vector3.zero;
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

    // Mecânica de respawn do player para estado inicial
    private void RespawnPlayer()
    {
        player.position = playerSpawnPoint.position;
        player.localScale = originalPlayerScale;
        holeAnimation.Hide();
    }
}