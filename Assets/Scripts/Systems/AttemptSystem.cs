using UnityEngine;

// Função responsável por verificar qual mecânica deve estar ativa
public class AttemptSystem : MonoBehaviour
{
    public static AttemptSystem Instance;

    [SerializeField] private Transform simButton;
    [SerializeField] private Transform naoButton;
    [SerializeField] private SwapZone swapZone;
    private int lastAppliedAttempt = -1;

    private Vector3 originalSimPosition;
    private Vector3 originalNaoPosition;

    private void Awake()
    {
        Instance = this;
        originalSimPosition = simButton.position;
        originalNaoPosition = naoButton.position;
    }

    public void ApplyAttemptEffects(int naoAttempts)
    {
        if (lastAppliedAttempt == naoAttempts)
            return;

        lastAppliedAttempt = naoAttempts;

        switch (naoAttempts)
        {
            case 2:
                break;
        }
    }

    public void SwapButtons()
    {
        Vector3 tempPosition = simButton.position;

        simButton.position = naoButton.position;

        naoButton.position = tempPosition;
    }

    public void ResetAttemptEffects()
    {
        simButton.position = originalSimPosition;
        naoButton.position = originalNaoPosition;
    }
}