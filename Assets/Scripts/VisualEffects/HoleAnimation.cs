using System.Collections;
using UnityEngine;

public class HoleAnimation : MonoBehaviour
{
    public static HoleAnimation Instance;

    private Vector3 originalScale;

    [SerializeField] private Transform holeTransform;
    [SerializeField] private Transform naoButton;

    private void Awake()
    {
        Instance = this;
        originalScale = holeTransform.localScale;
    }

    // Animação do buraco se abrindo quando o jogador aperta o botão NÃO
   public IEnumerator PlayHoleAnimation()
    {
        transform.position = naoButton.position;

        gameObject.SetActive(true);

        holeTransform.localScale =
            originalScale;

        yield return null;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}