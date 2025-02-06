    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] public int maxHealth = 10;
    [SerializeField] public int currentHealth;
    [SerializeField] public Slider healthSlider;
    [SerializeField] public bool isInvincible = false;

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    private void Die()
    {
        SceneManager.LoadScene("StartMenu");
    }


    public void TakeDamage(int damage)
    {

        if (isInvincible) return;
        currentHealth -= damage;

        if (currentHealth < 0)
            currentHealth = 0;
        if (currentHealth <= 0)
        {
            Die();
        }

        healthSlider.value = currentHealth;
    }


    public void Heal(int amount)
        {
            currentHealth += amount;

            if (currentHealth > maxHealth) currentHealth = maxHealth;

            healthSlider.value = currentHealth;
        }
        public int GetCurrentHealth()
        {
            return currentHealth;
        }
}
