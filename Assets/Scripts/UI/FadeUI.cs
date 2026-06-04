using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeUI : MonoBehaviour
{
    public static FadeUI Instance;

    [SerializeField] private Image fadeImage;

    [SerializeField] private float fadeSpeed = 2f;

    private void Awake()
    {
        Instance = this;
    }

    public IEnumerator FadeOutRoutine()
    {
        Color color = fadeImage.color;

        while (color.a < 1f)
        {
            color.a += fadeSpeed * Time.deltaTime;

            fadeImage.color = color;

            yield return null;
        }

        color.a = 1f;
        fadeImage.color = color;
    }

    public IEnumerator FadeInRoutine()
    {
        Color color = fadeImage.color;

        while (color.a > 0f)
        {
            color.a -= fadeSpeed * Time.deltaTime;

            fadeImage.color = color;

            yield return null;
        }

        color.a = 0f;
        fadeImage.color = color;
    }
}