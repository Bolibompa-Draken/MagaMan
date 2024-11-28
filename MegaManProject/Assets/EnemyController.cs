using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float enemyDamage = 2f;
    GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, 7f * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    //IEnumerator FollowPlayer()
    //{

    //}
}
