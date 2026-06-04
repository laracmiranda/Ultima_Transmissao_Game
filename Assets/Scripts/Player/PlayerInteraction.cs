using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private Interactable currentInteractable;

    private void Update()
    {
        if (StartUI.Instance != null &&
            StartUI.Instance.IsWaitingForStart())
            return;
            
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteract();
        }
    }

    private void TryInteract()
    {
        if (currentInteractable == null)
            return;

        switch (currentInteractable.buttonType)
        {
            case ButtonType.Sim:
                GameManager.Instance.RegisterSimChoice();
                break;

            case ButtonType.Nao:
                GameManager.Instance.RegisterNaoAttempt();
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Interactable interactable =
            other.GetComponent<Interactable>();

        if (interactable != null)
        {
            currentInteractable = interactable;
            InteractionUI.Instance.Show();

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Interactable interactable =
            other.GetComponent<Interactable>();

        if (interactable != null &&
            currentInteractable == interactable)
        {
            currentInteractable = null;

            InteractionUI.Instance.Hide();
        }
    }
}