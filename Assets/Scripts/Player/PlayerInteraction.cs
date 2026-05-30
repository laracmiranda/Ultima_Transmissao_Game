using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private Interactable currentInteractable;

    private void Update()
    {
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
                Debug.Log("CLICOU NO SIM");
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

            Debug.Log(
                "Perto de: " +
                interactable.buttonType
            );
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

            Debug.Log("Saiu da área");
        }
    }
}