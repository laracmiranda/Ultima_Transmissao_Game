using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip buttonClick;


    [SerializeField] private AudioClip holeSound;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayButtonClick()
    {
        audioSource.PlayOneShot(buttonClick);
    }

    public void PlayHoleSound()
    {
        audioSource.PlayOneShot(holeSound);
    }
}