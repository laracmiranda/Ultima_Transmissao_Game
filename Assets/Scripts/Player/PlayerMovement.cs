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
    private bool invertedControls;

    private Animator animator;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentSpeed = normalSpeed;
    }

    private void Update()
    {
        if (GameManager.Instance.isGameOver)
            return;

        if (StartUI.Instance != null &&
            StartUI.Instance.IsWaitingForStart())
            return;
    
        float horizontal =
            Input.GetAxisRaw("Horizontal");

        float vertical =
            Input.GetAxisRaw("Vertical");

        if (invertedControls)
        {
            horizontal *= -1;
            vertical *= -1;
        }

        movement.x = horizontal;
        movement.y = vertical;

        // Verifica o lado que o player está virado para ajustar a animação
        if (horizontal > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (horizontal < 0)
        {
            spriteRenderer.flipX = true;
        }

        bool isMoving = movement.sqrMagnitude > 0;
        animator.SetBool("IsMoving", isMoving);
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

    // Habilita/desabilita que os controles de movimento sejam invertidos
    public void EnableInvertedControls()
    {
        invertedControls = true;
    }

    public void DisableInvertedControls()
    {
        invertedControls = false;
    }  

    // Ativa/desativa o movimento do player
    public void DisableMovement()
    {
        movement = Vector2.zero;
        rb.linearVelocity = Vector2.zero;

        enabled = false;
    }

    public void EnableMovement()
    {
        enabled = true;
    }

    // Reseta o estado do player para o original
    public void ResetPlayerState()
    {
        currentSpeed = normalSpeed;
        invertedControls = false;
    }
}