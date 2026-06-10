using System.Collections;
using TMPro;
using UnityEngine;

public class TypewriterEffect : MonoBehaviour
{
    [SerializeField] private TMP_Text targetText;

    [SerializeField] private float letterDelay = 0.02f;

    [SerializeField] private AudioSource audioSource;

    public void ShowText(string message)
    {
        StopAllCoroutines();

        StartCoroutine(
            TypeRoutine(message)
        );
    }

    private IEnumerator TypeRoutine(string message)
    {
        targetText.text = "";

        foreach (char letter in message)
        {

            if (letter != ' ')
            {
                audioSource.PlayOneShot(audioSource.clip);
            }

            targetText.text += letter;

            yield return new WaitForSeconds(letterDelay);
        }
    }
}