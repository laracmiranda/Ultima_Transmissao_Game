using UnityEngine;

// Função responsável por verificar qual mecânica deve estar ativa
public class AttemptSystem : MonoBehaviour
{
    public static AttemptSystem Instance;

    // -- Referências --

    // Botões
    [SerializeField] private Transform simButton;
    [SerializeField] private Transform naoButton;
    [SerializeField] private Transform tinyNaoPosition;
    [SerializeField] private SwapZone swapZone;
    [SerializeField] private Transform centerPosition;

    // Player
    [SerializeField] private PlayerMovement playerMovement;

    // Lasers
    [SerializeField] private LaserController laserBottom;
    [SerializeField] private LaserController laserMiddle;
    [SerializeField] private LaserController laserTop;

    // Escala original do botão NÃO
    private Vector3 originalNaoScale;

    // Última tentativa
    private int lastAppliedAttempt = -1;

    // Posições originais dos botões
    private Vector3 originalSimPosition;
    private Vector3 originalNaoPosition;

    // O que o jogo faz quando inicia
    private void Awake()
    {
        Instance = this;

        // Define onde os botões estarão quando o jogo rodar
        originalSimPosition = simButton.position;
        originalNaoPosition = naoButton.position;

        // Define a escala original do botão NÃO quando o jogo rodar
        originalNaoScale = naoButton.localScale;
    }

    // Aplica os efeitos das tentativas
    public void ApplyAttemptEffects(int naoAttempts)
    {
        if (lastAppliedAttempt == naoAttempts)
            return;

        lastAppliedAttempt = naoAttempts;

        switch (naoAttempts)
        {
            case 2:
                break;

            case 3:
                MakeNaoButtonTiny();
                break;
            
            case 4:
                TimerManager.Instance.StartTimer();
                EnableSlowMovement();
                break;
            
            case 5:
                TimerManager.Instance.StartTimer();
                EnableInvertedControls();
                break;

            case 6:
                TimerManager.Instance.StartTimer();
                EnableDarkness();
                break;
            
            case 7:
                TimerManager.Instance.StartTimer();
                EnableGlitch();
                EnableDarkness();
                EnableSlowMovement();
                break;

            case 8:
                TimerManager.Instance.StartTimer();
                laserBottom.EnableLaser();
                laserMiddle.EnableLaser();
                laserTop.EnableLaser();
                break;

            case 9:
                ShowOnlyNaoButton();
                break;

            case 10:
                ShowOnlySimButton();
                break;
        }
    }

    // -- Mecânicas --

    // Inverte a posição dos botões
    public void SwapButtons()
    {
        Vector3 tempPosition = simButton.position;
        simButton.position = naoButton.position;
        naoButton.position = tempPosition;
    }

    // Diminui o tamanho do botão NÃO
    private void MakeNaoButtonTiny()
    {
        naoButton.localScale =
            new Vector3(0.1f, 0.1f, 1f);

        naoButton.position =
            tinyNaoPosition.position;
    }

    // Ativa o timer
    private void ActivateTimer()
    {
        TimerManager.Instance.StartTimer();
    }

    // Ativa a lentidão do player
    private void EnableSlowMovement()
    {
        playerMovement.SetSpeed(1f);
    }

    // Torna os controles de movimento do player invertidos - WASD
    private void EnableInvertedControls()
    {
        playerMovement.EnableInvertedControls();
    }

    // Adiciona escuridão do mapa
    private void EnableDarkness()
    {
        DarknessEffect.Instance.EnableDarkness();
    }

    // Adiciona efeitos de glitch - flashes e tremor
    private void EnableGlitch()
    {
        GlitchEffect.Instance.EnableGlitch();
    }
    
    // Desabilita todos os lasers
    public void DisableAllLasers()
    {
        laserBottom.DisableLaser();
        laserMiddle.DisableLaser();
        laserTop.DisableLaser();
    }

    // Exibe apenas o botão NÃO
    public void ShowOnlyNaoButton()
    {
        simButton.gameObject.SetActive(false);

        naoButton.gameObject.SetActive(true);

        naoButton.position =
            centerPosition.position;
    }

    // Exibe apenas o botão SIM 
    public void ShowOnlySimButton()
    {
        naoButton.gameObject.SetActive(false);

        simButton.gameObject.SetActive(true);

        simButton.position =
            centerPosition.position;
    }

    // -- Resets --

    // Força que o estado seja reaplicado na tentativa quando der timeout
    public void ForceReapplyCurrentAttempt()
    {
        lastAppliedAttempt = -1;
    }

    // Reseta os estados para os originais
    public void ResetAttemptEffects()
    {
        simButton.gameObject.SetActive(true);
        naoButton.gameObject.SetActive(true);
        simButton.position = originalSimPosition;
        naoButton.position = originalNaoPosition;
        naoButton.localScale = originalNaoScale;
        playerMovement.ResetPlayerState();
        DarknessEffect.Instance.DisableDarkness();
        GlitchEffect.Instance.DisableGlitch();

        // Desabilita todos os lasers
        laserBottom.DisableLaser();
        laserMiddle.DisableLaser();
        laserTop.DisableLaser();
    }
}