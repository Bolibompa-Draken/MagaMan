using UnityEngine;

public class Movement : MonoBehaviour
{
    public bool isRunning = false;
    private float horizontal;
    private float speed = 8f;
    private float currentSpeed;
    private float sprintSpeed = 16f;
    private float jumpingPower = 16f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private StaminaBar staminaBar;  // Reference to StaminaBar script

    void Start()
    {
        // Automatically find the StaminaBar component attached to the same GameObject (or assign manually in Inspector)
        staminaBar = FindAnyObjectByType<StaminaBar>();
        if (staminaBar == null)
        {
            Debug.LogError("StaminaBar reference not found! Make sure there's a StaminaBar component in the scene.");
        }
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
        }
        if (Input.GetButtonUp("Jump") && rb.linearVelocity.y < 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }

        currentSpeed = speed;

        // Only allow running if stamina is greater than 0
        if (Input.GetKey(KeyCode.LeftShift) && staminaBar != null && staminaBar.CurrentStamina > 0)
        {
            currentSpeed = sprintSpeed;
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
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
