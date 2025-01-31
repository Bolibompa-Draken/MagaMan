using System;
using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    GameObject player;
    Animator animator;
    SpriteRenderer spriteRenderer;

    [SerializeField] bool isPaused = false;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] GameObject bloodSplatterParticleEffect;

    bool isAlive = false;
    bool isAttacking = false;
    private Vector3 initialScale;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (animator == null)
        {
            Debug.LogError("Animator component missing from enemy!");
        }

        isAlive = true;
        initialScale = transform.localScale;
        SetIdleState();
    }

    void Update()
    {
        if (!isPaused && isAlive && !isAttacking)
        {
            MoveTowardsPlayer();
        }
        else if (!isAttacking && isAlive)
        {
            SetIdleState();
        }
    }

    private void MoveTowardsPlayer()
    {
        if (player != null)
        {
            animator.SetBool("isIdle", false);
            animator.SetBool("isRunning", true);
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
            FlipSprite();
        }
    }

    private void FlipSprite()
    {
       
        if (player.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(Mathf.Abs(initialScale.x), initialScale.y, initialScale.z);
        }
        else
        {
            transform.localScale = new Vector3(-Mathf.Abs(initialScale.x), initialScale.y, initialScale.z);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(HandleAttack());
        }
    }

    private IEnumerator HandleAttack()
    {
        Damage.isPaused = false;
        isPaused = true;
        isAttacking = true;

        animator.SetBool("isRunning", false);
        animator.SetBool("isIdle", false);
        animator.SetBool("isAttacking", true);

        yield return new WaitForSeconds(1f);

        animator.SetBool("isAttacking", false);
        yield return new WaitForSeconds(2f);

        isPaused = false;
        isAttacking = false;
    }

    public void Die()
    {
        if (!isAlive) return;

        isAlive = false;
        animator.SetBool("isAlive", false);
        animator.SetBool("isRunning", false);
        animator.SetBool("isAttacking", false);
        animator.SetBool("isIdle", false);
        Instantiate(bloodSplatterParticleEffect, transform.position, Quaternion.identity);

        Destroy(gameObject, 5f);
    }

    private void SetIdleState()
    {
        animator.SetBool("isRunning", false);
        animator.SetBool("isAttacking", false);
        animator.SetBool("isIdle", true);
    }

    internal float GetMoveDirection()
    {
        throw new NotImplementedException();
    }
}



