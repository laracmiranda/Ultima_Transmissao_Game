using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    private AudioClip currentMusic;
    
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

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
    private void PlayMusic(AudioClip clip)
    {
        Debug.Log("PLAY MUSIC: " + clip.name);

        if (currentMusic == clip)
            return;

        musicSource.Stop();

        currentMusic = clip;

        musicSource.clip = clip;

        musicSource.Play();
    }
    // Música do início
    public void PlayStartMusic()
    {
        musicSource.volume = 1f;
        PlayMusic(startMusic);
    }

    // Música do gameplay
    public void PlayGameplayMusic()
    {
        musicSource.volume = 0.015f;
        PlayMusic(gameplayMusic);
    }

    // Música da vitória
    public void PlayVictoryMusic()
    {
        musicSource.volume = 0.05f;
        PlayMusic(victoryMusic);
    }

    // Música do gameover
    public void PlayGameOverMusic()
    {
        musicSource.volume = 1f;
        PlayMusic(gameOverMusic);
    }

    // Para a música
    public void StopMusic()
    {
        Debug.Log("STOP MUSIC");
        musicSource.Stop();

        Debug.Log(
            "MusicSource isPlaying: " +
            musicSource.isPlaying
        );
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
}