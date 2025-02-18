using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    public float speed = 20f;
    [SerializeField] public int damage = 3;
    [SerializeField] public float lifeTime = 5f;

    [Header("References")]
    public Rigidbody2D rb;
    public GameObject hitEffectPrefab;

    

    void Start()
    {
        rb.linearVelocity = transform.right * speed; 
        Invoke("DestroyBullet", lifeTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
           
            GameObject hitEffect = Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
            Destroy(hitEffect, hitEffect.GetComponent<ParticleSystem>().main.duration);
            Destroy(gameObject);
            if (collision.CompareTag("Player"))
            {
                return;
            }
        }

        if (collision.CompareTag("Enemy"))
        {
         
            EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }          
            GameObject hitEffect = Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
            Destroy(hitEffect, hitEffect.GetComponent<ParticleSystem>().main.duration);
            Destroy(gameObject);
            
            
        }
    }
    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}




