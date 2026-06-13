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
    private bool footstepsPlaying;

    private Animator animator;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentSpeed = normalSpeed;
    }

    // Lê a entrada do jogador para movimentação
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

        // Controla a animação de movimento
        bool isMoving = movement.sqrMagnitude > 0;
        animator.SetBool("IsMoving", isMoving);

        // Controla o som dos passos
        if (isMoving && !footstepsPlaying)
        {
            AudioManager.Instance.StartFootsteps();
            footstepsPlaying = true;
        }
        else if (!isMoving && footstepsPlaying)
        {
            AudioManager.Instance.StopFootsteps();
            footstepsPlaying = false;
        }
    }

    // Aplica o movimento do personagem usando física
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

        AudioManager.Instance.StopFootsteps();
        footstepsPlaying = false;

        enabled = false;
    }

    public void EnableMovement()
    {
        footstepsPlaying = false;
        enabled = true;
    }

    // Reseta o estado do player para o original
    public void ResetPlayerState()
    {
        currentSpeed = normalSpeed;
        invertedControls = false;
    }
}