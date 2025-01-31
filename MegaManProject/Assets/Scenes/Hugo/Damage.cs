using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] public int damage = 1;
    public static bool isPaused = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (isPaused) return;
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
}
