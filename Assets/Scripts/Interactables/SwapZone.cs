using UnityEngine;

public class SwapZone : MonoBehaviour
{
    private bool hasSwapped;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (hasSwapped)
            return;

        if (GameManager.Instance.CurrentAttempt != 2)
            return;

        AttemptSystem.Instance.SwapButtons();

        hasSwapped = true;
    }

public void ResetSwap()
    {
        hasSwapped = false;
    }
}