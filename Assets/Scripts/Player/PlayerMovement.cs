using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]

    // Define a velocidade normal do personagem 
    [SerializeField] private float normalSpeed = 3.5f;

    // Velocidade atual do personagem
    private float currentSpeed;

    private Rigidbody2D rb;
    private Vector2 movement;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        currentSpeed = normalSpeed;
    }

    private void Update()
    {
        if (GameManager.Instance.isGameOver)
            return;
    
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = movement.normalized * currentSpeed;
    }

    // Ajusta a velocidade atual
    public void SetSpeed(float speed)
    {
        currentSpeed = speed;
    }

    // Reseta a velocidade atual para a normal
    public void ResetSpeed()
    {
        currentSpeed = normalSpeed;
    }
}