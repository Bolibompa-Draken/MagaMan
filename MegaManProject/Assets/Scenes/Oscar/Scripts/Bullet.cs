using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    [SerializeField] public float lifeTime;
    void Start()
    {
       rb.linearVelocity= transform.right*speed;
        Invoke("DestroyBullet", lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D ()
    {
        
    }
    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
