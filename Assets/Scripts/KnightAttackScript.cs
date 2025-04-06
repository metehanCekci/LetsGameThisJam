using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float attackRange = 2f;
    public int damage = 10;
    public float attackCooldown = 1f;
    public float attackDuration = 0.8f; // ← animasyon süresi

    private Transform player;
    private float lastAttackTime;
    private Rigidbody2D rb;
    private Animator animator;
    public float yTolerance = 0.5f; // Y ekseni farkı toleransı

    [HideInInspector]
    public bool isAttacking = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void FixedUpdate()
    {
        if (player == null || isAttacking) return;

        float xDistance = Mathf.Abs(transform.position.x - player.position.x);
        float yDistance = Mathf.Abs(transform.position.y - player.position.y);

        // Hem X ekseni menzilde olmalı, hem de Y ekseni çok farklı olmamalı
        if (xDistance <= attackRange && yDistance <= yTolerance)
        {
            if (Time.time >= lastAttackTime + attackCooldown)
            {
                StartCoroutine(AttackRoutine());
            }

            animator.SetBool("Attacking", true);
            animator.SetBool("Walking", false);
            rb.linearVelocity = Vector2.zero;
        }
        else
        {
            animator.SetBool("Attacking", false);
            animator.SetBool("Walking", true);
        }
    }

    System.Collections.IEnumerator AttackRoutine()
    {
        isAttacking = true;
        lastAttackTime = Time.time;

        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }

        yield return new WaitForSeconds(attackDuration); // animasyon süresi kadar bekle
        isAttacking = false;
    }
}
