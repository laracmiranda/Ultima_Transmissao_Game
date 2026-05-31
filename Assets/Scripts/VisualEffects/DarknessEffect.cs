using UnityEngine;

public class DarknessEffect : MonoBehaviour
{
    public static DarknessEffect Instance;

    [SerializeField] private GameObject darknessOverlay;

    private void Awake()
    {
        Instance = this;
    }

    // Ativa/desativa efeito de escuridão
    public void EnableDarkness()
    {
        darknessOverlay.SetActive(true);
    }

    public void DisableDarkness()
    {
        darknessOverlay.SetActive(false);
    }
}