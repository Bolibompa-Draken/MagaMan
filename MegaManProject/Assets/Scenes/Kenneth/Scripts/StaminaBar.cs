using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public float maxStamina = 100f;
    public float staminaDrainRate = 20f;
    public float staminaRegenRate = 15f;
    public float regenDelay = 2f;
    public float currentStamina;
    private bool isRegenerating = false;

    public Slider staminaBar;
    private bool isRunning = false;
    private bool isStaminaEmpty = false;

    private float cooldownTimer = 0f;  // Tracks the cooldown time for regen delay

    void Start()
    {
        currentStamina = maxStamina;
        staminaBar.maxValue = maxStamina;
        staminaBar.value = currentStamina;
    }

    void Update()
    {
        HandleRunning();
        UpdateStaminaUI();
    }

    void HandleRunning()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        // Check if the player is running (holding shift and moving)
        if (Input.GetKey(KeyCode.LeftShift) && currentStamina > 0 && Mathf.Abs(horizontal) > 0)
        {
            isRunning = true;
            isRegenerating = false;
            cooldownTimer = 0f;  // Reset cooldown timer when running
            currentStamina -= staminaDrainRate * Time.deltaTime;

            if (currentStamina <= 0)
            {
                currentStamina = 0;
                isRunning = false;
                isStaminaEmpty = true;
                Invoke(nameof(StartRegeneration), regenDelay);  // Start regeneration after 2 seconds delay
            }
        }
        else
        {
            if (isRunning)
            {
                isRunning = false;
                cooldownTimer = regenDelay;  // Start the cooldown timer when player stops running
            }
        }

        // If the cooldownTimer is greater than 0, decrement it
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }

        // Only start regenerating if the cooldown timer has finished
        if (cooldownTimer <= 0 && !isRunning && currentStamina < maxStamina)
        {
            isRegenerating = true;
        }

        // Stamina regeneration logic
        if (isRegenerating && currentStamina < maxStamina)
        {
            currentStamina += staminaRegenRate * Time.deltaTime;
            if (currentStamina > maxStamina)
                currentStamina = maxStamina;

            if (currentStamina == maxStamina)
            {
                isRegenerating = false;
                isStaminaEmpty = false;
            }
        }
    }

    void StartRegeneration()
    {
        // Called to start stamina regeneration after the regen delay
        isRegenerating = true;
    }

    void UpdateStaminaUI()
    {
        staminaBar.value = currentStamina;
    }

    public float CurrentStamina
    {
        get { return currentStamina; }
    }
}
