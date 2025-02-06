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
    [SerializeField] public bool infinitestamina = false;

    public Slider staminaBar;
    private bool isRunning = false;
    private bool isStaminaEmpty = false;

    private float cooldownTimer = 0f;

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

        if (Input.GetKey(KeyCode.LeftShift) && !isStaminaEmpty && currentStamina > 0 && Mathf.Abs(horizontal) > 0)
        {
            isRunning = true;
            isRegenerating = false;
            cooldownTimer = 0f;
            if (infinitestamina) return;

            currentStamina -= staminaDrainRate * Time.deltaTime;

            if (currentStamina <= 0)
            {
                currentStamina = 0;
                isRunning = false;
                isStaminaEmpty = true;
                cooldownTimer = regenDelay;
            }
        }
        else
        {
            if (isRunning)
            {
                isRunning = false;
                cooldownTimer = regenDelay;
            }
        }

        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }

        if (cooldownTimer <= 0 && !isRunning && currentStamina < maxStamina)
        {
            isRegenerating = true;
            isStaminaEmpty = false;
        }

        if (isRegenerating && currentStamina < maxStamina)
        {
            currentStamina += staminaRegenRate * Time.deltaTime;
            if (currentStamina > maxStamina)
                currentStamina = maxStamina;

            if (currentStamina == maxStamina)
            {
                isRegenerating = false;
            }
        }
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
