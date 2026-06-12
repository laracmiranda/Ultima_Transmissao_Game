using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverUI : MonoBehaviour
{
    public static GameOverUI Instance;

    public enum GameOverType
    {
        Falling,
        Hurt
    }

    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text gameOverText;
    [SerializeField] private TypewriterEffect typewriterEffect;

    [SerializeField] private RectTransform fallingPlayer;
    [SerializeField] private GameObject fallingPlayerObject;

    [SerializeField] private GameObject hurtPlayerObject;

    private Coroutine fallingRoutine;

    private void Awake()
    {
        Instance = this;
    }

    public void Show(string message, GameOverType type)
    {
        InteractionUI.Instance.Hide();
        panel.SetActive(true);

        fallingPlayerObject.SetActive(
        type == GameOverType.Falling
        );

        hurtPlayerObject.SetActive(
            type == GameOverType.Hurt
        );
        
        typewriterEffect.ShowText(message);

        if (fallingRoutine != null)
        {
            StopCoroutine(fallingRoutine);
        }

        fallingRoutine =
            StartCoroutine(
                FallingAnimation()
            );
    }

    public void Hide()
    {
        panel.SetActive(false);

        if (fallingRoutine != null)
        {
            StopCoroutine(fallingRoutine);
            fallingRoutine = null;
        }
    }

    private void Update()
    {
        if (!panel.activeSelf)
            return;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            GameManager.Instance.ContinueGame();
        }
    }

    // Animação de queda na tela de GameOver
    private IEnumerator FallingAnimation()
    {
        while (true)
        {
            fallingPlayer.anchoredPosition =
                new Vector2(0f, 300f);

            while (
                fallingPlayer.anchoredPosition.y > -300f
            )
            {
                fallingPlayer.anchoredPosition +=
                    Vector2.down *
                    250f *
                    Time.deltaTime;

                yield return null;
            }
        }
    }
}