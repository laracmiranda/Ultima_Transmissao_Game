using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CutscenePlayer : MonoBehaviour
{
    [SerializeField] private Image cutsceneImage;
    [SerializeField] private float frameRate = 0.01f;

    [Header("Cutscenes")]
    [SerializeField] private Sprite[] cutscene1;

    [SerializeField] private Sprite[] cutscene2;

    [SerializeField] private Sprite[] cutscene3;

    private Coroutine loopRoutine;

    // Método genérico de reprodução reutilizável
    private IEnumerator PlayFrames(Sprite[] frames)
    {
        for (int i = 0; i < frames.Length; i++)
        {
            cutsceneImage.sprite = frames[i];

            yield return new WaitForSeconds(frameRate);
        }
    }

    // Métodos específicos para cada cutscene, chamando a rotina genérica

    // Cutscene 1
    public IEnumerator PlayCutscene1()
    {
        yield return StartCoroutine(
            PlayFrames(cutscene1)
        );
    }

    // Cutscene 2
    public IEnumerator PlayCutscene2()
    {
        yield return StartCoroutine(
            PlayFrames(cutscene2)
        );
    }

    // Cutscene 3 (loop)
    public IEnumerator PlayLoopCutscene()
    {
        while (true)
        {
            for (int i = 0; i < cutscene3.Length; i++)
            {
                cutsceneImage.sprite =
                    cutscene3[i];

                yield return new WaitForSeconds(
                    frameRate
                );
            }
        }
    }

    public void StartLoopCutscene()
    {
        if (loopRoutine != null)
        {
            StopCoroutine(loopRoutine);
        }

        loopRoutine =
            StartCoroutine(
                PlayLoopCutscene()
            );
    }

    public void StopLoopCutscene()
    {
        if (loopRoutine != null)
        {
            StopCoroutine(loopRoutine);

            loopRoutine = null;
        }
    }


}