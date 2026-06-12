using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip laserActivateSound;

    [SerializeField] private AudioClip buttonClick;

    [SerializeField] private AudioClip holeSound;

    private void Awake()
    {
        Instance = this;
    }

    // Sons dos botões
    public void PlayButtonClick()
    {
        audioSource.PlayOneShot(buttonClick);
    }

    // Som do buraco
    public void PlayHoleSound()
    {
        audioSource.PlayOneShot(holeSound);
    }

    // Sons do laser
    public void PlayLaserActivateSound()
    {
        audioSource.PlayOneShot(laserActivateSound);
    }
}