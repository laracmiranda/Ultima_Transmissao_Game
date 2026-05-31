using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GlitchEffect : MonoBehaviour
{
    public static GlitchEffect Instance;

    [SerializeField] private GameObject glitchOverlay;

    [SerializeField] private Transform player;

    [SerializeField] private Transform naoButton;

    private Coroutine glitchRoutine;

    private void Awake()
    {
        Instance = this;
    }

    // Ativa/desativa efeito do glitch visual
    public void EnableGlitch()
    {
        if (glitchRoutine != null)
            return;

        glitchRoutine = StartCoroutine(GlitchRoutine());
    }

    public void DisableGlitch()
    {
        if (glitchRoutine != null)
        {
            StopCoroutine(glitchRoutine);
            glitchRoutine = null;
        }

        glitchOverlay.SetActive(false);
    }

    // Calcula proximidade do player com o botão
    private float GetProximity()
    {
        float distance =
            Vector2.Distance(
                player.position,
                naoButton.position
            );

        float maxDistance = 8f;

        float proximity =
            1f - Mathf.Clamp01(distance / maxDistance);

        return proximity;
    }

    // Funcionamento do glitch
    private IEnumerator GlitchRoutine()
    {
        while (true)
        {
            float proximity = GetProximity();

            int flashes =
                Mathf.RoundToInt(
                    Mathf.Lerp(2, 8, proximity)
                );

            for (int i = 0; i < flashes; i++)
            {
                glitchOverlay.SetActive(true);

                float shakeIntensity =
                    Mathf.Lerp(
                        0.05f,
                        0.18f,
                        proximity
                    );

                CameraShake.Instance.Shake(shakeIntensity);

                yield return new WaitForSeconds(0.08f);

                glitchOverlay.SetActive(false);

                CameraShake.Instance.ResetPosition();

                yield return new WaitForSeconds(0.05f);
            }

            // Define o tempo entre os glitches de acordo com a proximidade do player ao botão
            float minDelay =
                Mathf.Lerp(
                    1.2f,
                    0.08f,
                    proximity
                );

            float maxDelay =
                Mathf.Lerp(
                    2f,
                    0.1f,
                    proximity
                );

            yield return new WaitForSeconds(
                Random.Range(
                    minDelay,
                    maxDelay
                )
            );
        }
    }
}