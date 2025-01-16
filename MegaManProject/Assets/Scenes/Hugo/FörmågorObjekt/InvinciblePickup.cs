using UnityEngine;
using System.Collections;

public class InvinciblePickup : MonoBehaviour
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
        Health playerHealth = collision.GetComponent<Health>();
        if (playerHealth != null)
        {
            StartCoroutine(GrantInvincibility(playerHealth));
        }
    }

    private IEnumerator GrantInvincibility(Health playerHealth)
    {
        spriteRenderer.enabled = false;
        pickupCollider.enabled = false;
        playerHealth.isInvincible = true;
        yield return new WaitForSeconds(invincibilityDuration);

        playerHealth.isInvincible = false;

        Destroy(gameObject);
    }
}
