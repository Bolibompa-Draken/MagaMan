using UnityEngine;
using System.Collections;
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] public int maxHealth = 10;
    [SerializeField] public int currentHealth;
    [SerializeField] private Material flashMaterial;
    [SerializeField] private float duration;
    [SerializeField] private Material originalMaterial;
    [SerializeField] GameObject mainCamera;
    private SpriteRenderer spriteRenderer;
    private Coroutine flashRoutine;
    private EnemyController enemyController;
    [SerializeField] public GameObject[] Powerup;

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;

        enemyController = GetComponent<EnemyController>();
    }


    public void TakeDamage(int damage)
    {
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
        

        if (enemyController != null)
        {
            enemyController.Die();
        }

        if (Powerup.Length > 0 && Random.value <= 0.1f)
        {
            GameObject RandomPowerup = Powerup[Random.Range(0, Powerup.Length)];
            Instantiate(RandomPowerup, transform.position, Quaternion.identity);
        }
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