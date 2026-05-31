using TMPro;
using UnityEngine;

// Timer para as tentativas 5+
public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance;

    [SerializeField] private GameObject timerTextObject;
    [SerializeField] private TMP_Text timerText;

    [SerializeField] private float maxTime = 15f;

    private float currentTime;
    private bool timerRunning;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (!timerRunning)
            return;

        currentTime -= Time.deltaTime;

        timerText.text =
            Mathf.CeilToInt(currentTime).ToString();

        if (currentTime <= 0)
        {
            timerRunning = false;

            GameManager.Instance.RegisterTimeOut();
        }
    }

    public void StartTimer()
    {
        currentTime = maxTime;

        timerTextObject.SetActive(true);

        timerRunning = true;
    }

    public void StopTimer()
    {
        timerRunning = false;

        timerTextObject.SetActive(false);
    }
}