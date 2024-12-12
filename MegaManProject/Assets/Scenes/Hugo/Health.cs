    using UnityEngine;
    using UnityEngine.UI;

    public class Health : MonoBehaviour
    {
        [SerializeField] public int maxHealth = 10;
        [SerializeField] public int currentHealth;
        [SerializeField] public Slider healthSlider;

        void Start()
        {
            currentHealth = maxHealth;
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
        public void TakeDamage(int damage)
        {
            currentHealth -= damage;

            if (currentHealth < 0) currentHealth = 0;

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
