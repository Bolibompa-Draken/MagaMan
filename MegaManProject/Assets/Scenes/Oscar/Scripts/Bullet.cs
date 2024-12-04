using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    public float speed = 20f;
    [SerializeField] public float lifeTime = 5f;

    [Header("References")]
    public Rigidbody2D rb;

    void Start()
    {
        rb.linearVelocity = transform.right * speed;
        Invoke("DestroyBullet", lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}


