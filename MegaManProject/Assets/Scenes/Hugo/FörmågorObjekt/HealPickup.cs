using UnityEngine;

public class HealPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health playerHealth = collision.GetComponent<Health>();
        if (playerHealth != null)
        {
            playerHealth.Heal(playerHealth.maxHealth);
            Destroy(gameObject);
        }
    }
}
