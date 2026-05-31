using UnityEngine;

// Função para empurrar o player na segunda tentativa
public class NaoPushZone : MonoBehaviour
{

    private bool hasPushedPlayer;

    private void OnTriggerEnter2D(Collider2D other)
    {

        // Verifica se o player já foi empurrado
        if (hasPushedPlayer)
            return;

        if (!other.CompareTag("Player"))
            return;

        // Verifica se é a segunda tentativa
        if (GameManager.Instance.CurrentAttempt != 1)
            return;

        // Faz o "empurrão"
        Vector2 direction =
            (other.transform.position - transform.position).normalized;

        other.transform.position +=
            (Vector3)(direction * 2.5f);

        // Marca que o player já foi empurrado e assim não repete o estado
        hasPushedPlayer = true;
    }

    public void ResetPush()
    {
        hasPushedPlayer = false;
    }
}