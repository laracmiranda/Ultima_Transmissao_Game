using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    private AudioClip currentMusic;
    
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource footstepSource;

    [Header("Footsteps")]
    [SerializeField] private AudioClip footstepSound;

    [Header("SFX")]
    // SFX
    [SerializeField] private AudioClip laserActivateSound;
    [SerializeField] private AudioClip buttonClick;
    [SerializeField] private AudioClip holeSound;

    // Musica
    [Header("Music")]
    [SerializeField] private AudioClip startMusic;
    [SerializeField] private AudioClip gameplayMusic;
    [SerializeField] private AudioClip gameOverMusic;
    [SerializeField] private AudioClip victoryMusic;

    private void Awake()
    {
        Instance = this;
    }

    // Métodos para tocar música
    private void PlayMusic(AudioClip clip, bool loop = true)
    {
        if (currentMusic == clip)
            return;

        musicSource.Stop();

        currentMusic = clip;

        musicSource.clip = clip;

        musicSource.loop = loop;

        musicSource.Play();
    }
    // Música do início
    public void PlayStartMusic()
    {
        musicSource.volume = 0.35f;
        PlayMusic(startMusic, true);
    }

    // Música do gameplay
    public void PlayGameplayMusic()
    {
        musicSource.volume = 0.01f;
        PlayMusic(gameplayMusic, true);
    }

    // Música da vitória
    public void PlayVictoryMusic()
    {
        musicSource.volume = 0.45f;
        PlayMusic(victoryMusic, true);
    }

    // Música do gameover
    public void PlayGameOverMusic()
    {
        musicSource.volume = 0.6f;
        PlayMusic(gameOverMusic, false);
    }

    // Para a música
    public void StopMusic()
    {
        musicSource.Stop();
        musicSource.clip = null;
        currentMusic = null;
    }

    // SFX

    // Sons dos botões
    public void PlayButtonClick()
    {
        sfxSource.PlayOneShot(buttonClick);
    }

    // Som do buraco
    public void PlayHoleSound()
    {
        sfxSource.PlayOneShot(holeSound);
    }

    // Sons do laser
    public void PlayLaserActivateSound()
    {
        sfxSource.PlayOneShot(laserActivateSound);
    }

    // Sons dos passos
    public void StartFootsteps()
    {
        if (footstepSource.isPlaying)
            return;

        footstepSource.clip = footstepSound;
        footstepSource.loop = true;
        footstepSource.Play();
    }

    public void StopFootsteps()
    {
        footstepSource.Stop();
    }
}