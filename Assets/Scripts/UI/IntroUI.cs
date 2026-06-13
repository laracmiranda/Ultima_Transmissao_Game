using UnityEngine;
using TMPro;
using System.Collections;

public class IntroUI : MonoBehaviour
{
    public static IntroUI Instance;

    [SerializeField] private GameObject panel;

    [SerializeField] private TMP_Text introText;

    [SerializeField] private TypewriterEffect typewriterEffect;

    private void Awake()
    {
        Instance = this;
    }

    public IEnumerator PlayIntro()
    {
        AudioManager.Instance.StopMusic();
        panel.SetActive(true);

        yield return StartCoroutine(
            FadeUI.Instance.FadeInRoutine()
        );

        typewriterEffect.ShowText(
            "Tenho uma pergunta antes de chegarmos..."
        );

        yield return new WaitForSeconds(4f);

        introText.text = "";

        typewriterEffect.ShowText(
            "Você quer namorar comigo?"
        );

        yield return new WaitForSeconds(4f);

        yield return StartCoroutine(
            FadeUI.Instance.FadeOutRoutine()
        );

        panel.SetActive(false);
    }
}