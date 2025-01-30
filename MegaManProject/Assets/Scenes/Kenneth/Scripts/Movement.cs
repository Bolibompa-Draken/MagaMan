using UnityEngine;
using System.Collections;

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
    private Weapon weapon; // Reference to Weapon script

    [Header("Fire Rate Power-Up")]
    [SerializeField] private float boostedFireRateMultiplier = 3f;
    [SerializeField] private float powerUpDuration = 20f;
    private bool isFireRateBoosted = false;


    void Start()
    {
        staminaBar = FindAnyObjectByType<StaminaBar>();
        if (staminaBar == null)
        {
            Debug.LogError("StaminaBar reference not found!");
        }

        weapon = FindAnyObjectByType<Weapon>(); 
        if (weapon == null)
        {
            Debug.LogError("Weapon script not found!");
           
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FireRatePowerUp"))
        {
            ActivateFireRatePowerUp();
            Destroy(collision.gameObject);
        }
    }

    private void ActivateFireRatePowerUp()
    {
        if (!isFireRateBoosted && weapon != null)
        {
            StartCoroutine(FireRatePowerUpCoroutine());
        }
    }

    private IEnumerator FireRatePowerUpCoroutine()
    {
        isFireRateBoosted = true;
        float originalFireRate = weapon.fireRate;
        weapon.fireRate *= boostedFireRateMultiplier;

        yield return new WaitForSeconds(powerUpDuration);

        weapon.fireRate = originalFireRate;
        isFireRateBoosted = false;
    }
}

