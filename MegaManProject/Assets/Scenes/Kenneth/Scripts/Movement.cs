using UnityEngine;

public class Movement : MonoBehaviour
{
    public bool isRunning = false;
    public bool AisRunning = false;
    public bool isJumpingUp = false;
    public bool isJumpingDown = false;
    public bool isIdle = false;
    public float horizontal;
    private float speed = 8f;
    private float currentSpeed;
    private float sprintSpeed = 16f;
    private float jumpingPower = 16f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private StaminaBar staminaBar;
    public Animator animator;


    void Start()
    {
        staminaBar = FindAnyObjectByType<StaminaBar>();
        if (staminaBar == null)
        {
           
            Debug.LogError("StaminaBar reference not found! Make sure there's a StaminaBar component in the scene.");
        }
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
            isJumpingUp = true;
        }
        if (Input.GetButtonUp("Jump") && rb.linearVelocity.y < 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }

        currentSpeed = speed;
        
        if (Input.GetKey(KeyCode.LeftShift) && staminaBar != null && staminaBar.CurrentStamina > 0)
        {
            currentSpeed = sprintSpeed;
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
        if (rb.linearVelocity.y < 0)
        {
            isJumpingUp = false;
            isJumpingDown = true; 
        }
        else if (rb.linearVelocity.y > 0)
        {
            isJumpingDown = false;
        }
        if (IsGrounded() && rb.linearVelocity.y == 0)
        {
            isJumpingUp = false;
            isJumpingDown = false;
        }
        
        isIdle = horizontal == 0 && IsGrounded();
        AisRunning = horizontal != 0;

        animator.SetBool("AisRunning", AisRunning);
        animator.SetBool("isJumpingUp", isJumpingUp);
        animator.SetBool("isJumpingDown", isJumpingDown);
        animator.SetBool("isIdle", isIdle);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * currentSpeed, rb.linearVelocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
}
