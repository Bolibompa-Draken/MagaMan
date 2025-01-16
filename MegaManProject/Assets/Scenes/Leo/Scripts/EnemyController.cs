using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    GameObject player;
    [SerializeField] bool isPaused = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        
        if (!isPaused)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, 3f * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(PauseEnemy(3f));
        }
    }
    IEnumerator PauseEnemy(float duration)
    {
        isPaused = true;
        yield return new WaitForSeconds(duration);
        isPaused = false;
    }
}
