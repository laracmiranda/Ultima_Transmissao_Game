using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class VictoryAnimator : MonoBehaviour
{
    [Header("GIF 1")]
    [SerializeField] private Image introImage;
    [SerializeField] private Sprite[] introFrames;

    [Header("GIF 2")]
    [SerializeField] private Image loopImage;
    [SerializeField] private Sprite[] loopFrames;

    [SerializeField] private float frameRate = 0.2f;

    [SerializeField] private GameObject victoryText;

    [SerializeField] private GameObject continueText;

    [SerializeField] private TypewriterEffect typewriterEffect;

    public IEnumerator PlayVictoryAnimation()
    {
        loopImage.gameObject.SetActive(false);

        for (int i = 0; i < introFrames.Length; i++)
        {
            introImage.sprite =
                introFrames[i];

            yield return new WaitForSeconds(
                frameRate
            );
        }

        introImage.gameObject.SetActive(false);

        loopImage.gameObject.SetActive(true);

        StartCoroutine(
            LoopAnimation()
        );
    }

    private IEnumerator LoopAnimation()
    {
        int frame = 0;

        while (true)
        {
            loopImage.sprite =
                loopFrames[frame];

            frame++;

            if (frame >= loopFrames.Length)
            {
                frame = 0;
            }

            yield return new WaitForSeconds(
                frameRate
            );
        }
    }

    public IEnumerator PlaySequence(string message)
    {
        victoryText.SetActive(false);

        continueText.SetActive(false);

        yield return StartCoroutine(
            PlayVictoryAnimation()
        );

        victoryText.SetActive(true);

        typewriterEffect.ShowText(message);

        yield return new WaitForSeconds(10f);

        continueText.SetActive(true);

        VictoryUI.Instance.EnableContinue();
    }
}