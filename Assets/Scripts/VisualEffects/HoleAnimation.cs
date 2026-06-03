using System.Collections;
using UnityEngine;

public class HoleAnimation : MonoBehaviour
{
    public static HoleAnimation Instance;

    private Vector3 originalScale;

    [SerializeField] private Transform holeTransform;
    [SerializeField] private Transform naoButton;

    private void Awake()
    {
        Instance = this;
        originalScale = holeTransform.localScale;
    }

    public IEnumerator PlayHoleAnimation()
    {
        // Faz com que buraco apareça onde o botão NÃO está
        transform.position = naoButton.position;

        gameObject.SetActive(true);

        holeTransform.localScale = Vector3.zero;

        float duration = 0.3f;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float progress =
                elapsed / duration;

            holeTransform.localScale =
                Vector3.Lerp(
                    Vector3.zero,
                    originalScale,
                    progress
                );

            yield return null;
        }

        holeTransform.localScale =
            originalScale;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}