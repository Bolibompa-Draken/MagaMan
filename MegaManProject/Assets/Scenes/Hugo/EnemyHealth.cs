using UnityEngine;
using System.Collections;
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] public int maxHealth = 10;
    [SerializeField] public int currentHealth;
    [SerializeField] private Material flashMaterial;
    [SerializeField] private float duration;
    [SerializeField] private Material originalMaterial;
    [SerializeField] GameObject bloodSplatterParticleEffect;
    private SpriteRenderer spriteRenderer;
    private Coroutine flashRoutine;

    void Start()
    {
        currentHealth = maxHealth;

        spriteRenderer = GetComponent<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;
    }

    public void TakeDamage(int damage)
    {
        Instantiate(bloodSplatterParticleEffect, transform.position, Quaternion.identity);
        currentHealth -= damage;
        if (currentHealth < 0)
            currentHealth = 0;
        if (currentHealth <= 0)
        {
            Die();
        }

        Flash();
    }

    public void Heal(int amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    private void Die()
    {
        Destroy(gameObject);
    }


    public void Flash()
    {
        
        if (flashRoutine != null)
        {
            
            StopCoroutine(flashRoutine);
        }
        flashRoutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        spriteRenderer.material = flashMaterial;

        yield return new WaitForSeconds(duration);

        spriteRenderer.material = originalMaterial;

        flashRoutine = null;
    }

}
