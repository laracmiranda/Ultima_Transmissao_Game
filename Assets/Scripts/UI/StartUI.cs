using UnityEngine;

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
        waitingForStart = false;

        panel.SetActive(false);
    }

    public bool IsWaitingForStart()
    {
        return waitingForStart;
    }
}