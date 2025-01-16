using UnityEngine;
using System.Collections;

public class StaminaPickup : MonoBehaviour
{
    [SerializeField] private float invincibilityDuration = 20f;
    private SpriteRenderer spriteRenderer;
    private Collider2D pickupCollider;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        pickupCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StaminaBar playerStamina = collision.GetComponent<StaminaBar>();
        if (playerStamina != null)
        {
            StartCoroutine(GrantStamina(playerStamina));
        }
    }

    private IEnumerator GrantStamina(StaminaBar playerStamina)
    {
        spriteRenderer.enabled = false;
        pickupCollider.enabled = false;
        playerStamina.infinitestamina = true;
        yield return new WaitForSeconds(invincibilityDuration);

        playerStamina.infinitestamina = false;

        Destroy(gameObject);
    }
}
