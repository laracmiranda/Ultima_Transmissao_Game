using UnityEngine;
using System.Collections;

public class StartUI : MonoBehaviour
{
    public static StartUI Instance;

    [SerializeField] private GameObject panel;

    private bool waitingForStart = true;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        panel.SetActive(true);
        AudioManager.Instance.PlayStartMusic();
        StartCoroutine(FadeUI.Instance.FadeInRoutine());
    }

    private void Update()
    {
        if (!waitingForStart)
            return;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartGame();
        }
    }

    private void StartGame()
    {
        StartCoroutine(StartGameRoutine());
    }

    private IEnumerator StartGameRoutine()
    {
        yield return StartCoroutine(
            FadeUI.Instance.FadeOutRoutine()
        );

        panel.SetActive(false);

        yield return StartCoroutine(
            IntroUI.Instance.PlayIntro()
        );

        yield return StartCoroutine(
            FadeUI.Instance.FadeInRoutine()
        );

        waitingForStart = false;
        AudioManager.Instance.PlayGameplayMusic();
    }
    
    public bool IsWaitingForStart()
    {
        return waitingForStart;
    }
}