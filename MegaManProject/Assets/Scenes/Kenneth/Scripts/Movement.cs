using UnityEngine;

public class Movement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float sprintSpeed = 16f;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;
    private float staminaCooldown;
    public float staminaDecayRate = 20f;
    public float staminaRegenRate = 10f;
    public float staminaCooldownTime = 2f;
    public float stamina = 100f;
    
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

   
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
        float currentSpeed = speed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = sprintSpeed;
        }
        rb.linearVelocity = new Vector2(horizontal * currentSpeed, rb.linearVelocity.y);

        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0)
        {
            currentSpeed = horizontal;
            stamina -= staminaDecayRate * Time.deltaTime;
            staminaCooldown = staminaCooldownTime;
        }
        else
        {
            currentSpeed = speed;

            if (staminaCooldown > 0)
            {
                staminaCooldown -= Time.deltaTime;
            }
            else
            {
                stamina += staminaRegenRate * Time.deltaTime;
                stamina = Mathf.Clamp(stamina, 0, 100f);
            }
        }

        Flip();
    }
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}