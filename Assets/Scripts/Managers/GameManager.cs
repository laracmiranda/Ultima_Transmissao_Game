using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private Transform player;
    [SerializeField] private Transform playerSpawnPoint;
    [SerializeField] private PlayerMovement playerMovement;

    [SerializeField] private HoleAnimation holeAnimation;

    [SerializeField] private ButtonVisual simButtonVisual;
    [SerializeField] private ButtonVisual naoButtonVisual;

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
        naoButtonVisual.SetClicked();

        AudioManager.Instance.PlayButtonClick();

        isGameOver = true;
        playerMovement.DisableMovement();
        TimerManager.Instance.StopTimer();
        AttemptSystem.Instance.DisableAllLasers();

        yield return new WaitForSeconds(0.4f);
        AudioManager.Instance.PlayHoleSound();
        yield return StartCoroutine(holeAnimation.PlayHoleAnimation());

        yield return new WaitForSeconds(0.2f);

        player.localScale = Vector3.zero;

        yield return new WaitForSeconds(1f);

        string message =
            GameTexts.NaoMessages[naoAttempts - 1];

        yield return StartCoroutine(
            FadeUI.Instance.FadeOutRoutine()
        );

        GameOverUI.Instance.Show(message,GameOverUI.GameOverType.Falling);
        AudioManager.Instance.PlayGameOverMusic();

        yield return StartCoroutine(
            FadeUI.Instance.FadeInRoutine()
        );
    }

    // Registra em qual tentativa o botão SIM foi pressionado
    public void RegisterSimChoice()
    {
        StartCoroutine(RegisterSimChoiceRoutine());
    }

    private IEnumerator RegisterSimChoiceRoutine()
    {
        simButtonVisual.SetClicked();

        AudioManager.Instance.PlayButtonClick();

        isGameOver = true;
        InteractionUI.Instance.Hide();
        playerMovement.DisableMovement();
        AttemptSystem.Instance.DisableAllLasers();
        TimerManager.Instance.StopTimer();

        yield return new WaitForSeconds(0.2f);

        int messageIndex;

        // Verifica se está na última tentativa para ajustar a mensagem
        if (naoAttempts == 10)
        {
            messageIndex = 9;
        }
        else
        {
            messageIndex = naoAttempts;
        }

        yield return StartCoroutine(
            FadeUI.Instance.FadeOutRoutine()
        );

        VictoryUI.Instance.Show(GameTexts.SimMessages[messageIndex]);

        yield return StartCoroutine(
            FadeUI.Instance.FadeInRoutine()
        );
    }

    // Telas de GameOver personalizadas

    // GameOver personalizado para caso o tempo esgote - Tentativas 5+
    public void RegisterTimeOut()
    {
        isGameOver = true;
        
        InteractionUI.Instance.Hide();
        playerMovement.DisableMovement();
        TimerManager.Instance.StopTimer();
        AttemptSystem.Instance.DisableAllLasers();

        retryCurrentAttempt = true;

        StartCoroutine(
            ShowSpecialGameOverRoutine(
                "O tempo acabou...\nVou assumir que isso foi um não."
            )
        );
    }

    // GameOver personalizado para caso o player seja atingido pelo laser
    public void RegisterLaserHit()
    {

        if (isGameOver)
            return;
            
        isGameOver = true;

        playerMovement.DisableMovement();
        InteractionUI.Instance.Hide();
        AttemptSystem.Instance.DisableAllLasers();
        TimerManager.Instance.StopTimer();

        retryCurrentAttempt = true;

        StartCoroutine(
            ShowSpecialGameOverRoutine(
                "Nem tente passar por aí.."
            )
        );
    }

    private IEnumerator ShowSpecialGameOverRoutine(string message)
    {
        yield return StartCoroutine(
            FadeUI.Instance.FadeOutRoutine()
        );

        GameOverUI.Instance.Show(message,GameOverUI.GameOverType.Hurt);
        AudioManager.Instance.PlayGameOverMusic();

        yield return StartCoroutine(
            FadeUI.Instance.FadeInRoutine()
        );
    }

    // Mecânica para continuar jogo depois do gameover

    public void ContinueGame()
    {
        StartCoroutine(
            ContinueGameRoutine()
        );
    }

    private IEnumerator ContinueGameRoutine()
{
    yield return StartCoroutine(
        FadeUI.Instance.FadeOutRoutine()
    );

    // Reseta a tentativa atual caso o tempo acabar
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

        yield return StartCoroutine(FadeUI.Instance.FadeInRoutine());
        AudioManager.Instance.PlayGameplayMusic();

        yield break;
    }

    isGameOver = false;
    GameOverUI.Instance.Hide();
    RespawnPlayer();
    AttemptSystem.Instance.ResetAttemptEffects();
    AttemptSystem.Instance.ApplyAttemptEffects(naoAttempts);

    yield return StartCoroutine(FadeUI.Instance.FadeInRoutine());
    AudioManager.Instance.PlayGameplayMusic();
}

    // Mecânica de respawn do player para estado inicial
    private void RespawnPlayer()
    {
        player.position = playerSpawnPoint.position;
        player.localScale = originalPlayerScale;
        playerMovement.EnableMovement();
        simButtonVisual.ResetButton();
        naoButtonVisual.ResetButton();
        holeAnimation.Hide();
    }
}