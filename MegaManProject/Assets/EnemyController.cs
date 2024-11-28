using UnityEngine;

public class EnemyController : MonoBehaviour
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindWithTag("Player");
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, 7f * Time.deltaTime);
    }
}
