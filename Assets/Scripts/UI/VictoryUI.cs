using TMPro;
using UnityEngine;
using System.Collections;

public class VictoryUI : MonoBehaviour
{
    public static VictoryUI Instance;

    // Referências
    [SerializeField] private GameObject panel;

    [Header("Texto")]
    [SerializeField] private TMP_Text victoryText;
    [SerializeField] private TypewriterEffect typewriterEffect;
    [SerializeField] private GameObject victoryTextObject;
    [SerializeField] private TypewriterEffect victoryTypewriter;

    // Cutscene
    [Header("Cutscene")]
    [SerializeField] private GameObject cutscenePanel;
    [SerializeField] private CutscenePlayer cutscenePlayer;

    [Header("Reminder")]
    [SerializeField] private GameObject reminderPanel;
    [SerializeField] private TMP_Text reminderText;
    [SerializeField] private TypewriterEffect reminderTypewriter;

    private void Awake()
    {
        Instance = this;
    }

    public void Show(string message)
    {
        InteractionUI.Instance.Hide();

        panel.SetActive(true);

        StartCoroutine(
            PlayVictorySequence(message)
        );
    }

    private IEnumerator PlayVictorySequence(string message)
    {
        victoryTextObject.SetActive(true);
        cutscenePanel.SetActive(false);
        reminderPanel.SetActive(false);

        victoryText.text = "";
        yield return null;

        victoryTypewriter.ShowText(message);
        yield return new WaitForSeconds(6f);

        yield return StartCoroutine(
            FadeUI.Instance.FadeOutRoutine()
        );

        victoryTextObject.SetActive(false);

        yield return StartCoroutine(
            FadeUI.Instance.FadeInRoutine()
        );

        cutscenePanel.SetActive(true);

        // Verificar se é um final especial
        bool isSpecialEnding =
            GameManager.Instance.CurrentAttempt == 0 ||
            GameManager.Instance.CurrentAttempt == 10;

        // Tocar a cutscene correta dependendo da tentativa atual
        if (isSpecialEnding)
        {
            yield return StartCoroutine(
                cutscenePlayer.PlayCutscene1()
            );

            yield return StartCoroutine(
                ShowReminder()
            );

            yield return StartCoroutine(
                cutscenePlayer.PlayCutscene2()
            );

            cutscenePlayer.StartLoopCutscene();
        }
        else
        {
            cutscenePlayer.StartLoopCutscene();
        }

        yield return new WaitForSeconds(10f);
        cutscenePlayer.StopLoopCutscene();

        Hide();
        CreditsUI.Instance.Show();
    }

    // Sequência para mostrar o texto de lembrança entre as cutscenes
    private IEnumerator ShowReminder()
    {
        yield return StartCoroutine(
            FadeUI.Instance.FadeOutRoutine()
        );

        cutscenePanel.SetActive(false);
        reminderPanel.SetActive(true);

        yield return StartCoroutine(
            FadeUI.Instance.FadeInRoutine()
        );

        reminderText.text = "";

        reminderTypewriter.ShowText(
            "Tenho uma pergunta antes de chegarmos..."
        );

        yield return new WaitForSeconds(3f);

        reminderText.text = "";

        reminderTypewriter.ShowText(
            "Você quer namorar comigo?"
        );

        yield return new WaitForSeconds(3f);

        yield return StartCoroutine(
            FadeUI.Instance.FadeOutRoutine()
        );

        reminderPanel.SetActive(false);
        cutscenePanel.SetActive(true);

        yield return StartCoroutine(
            FadeUI.Instance.FadeInRoutine()
        );
    }
    
    public void Hide()
    {
        cutscenePlayer.StopLoopCutscene();
        panel.SetActive(false);
        cutscenePanel.SetActive(false);
        reminderPanel.SetActive(false);
    }
}