using UnityEngine;
using System.Collections;

public class EnemyMeleeAttack : MonoBehaviour
{
    public float moveSpeed = 2f;
    
    public int damage = 10;
    public float attackCooldown = 1f;
    public float attackDuration = 0.8f; // ← animasyon süresi

    private Transform player;
    private float lastAttackTime;
    private Rigidbody2D rb;
    private Animator animator;

    public float stopDistance = 0.5f; // Oyuncuya bu kadar yaklaşınca artık daha fazla ilerlemesin
    public float attackRange = 2f;
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

        Vector2 directionToPlayer = player.position - transform.position;
        float xDistance = Mathf.Abs(transform.position.x - player.position.x);
        float yDistance = Mathf.Abs(transform.position.y - player.position.y);

        bool inAttackRange = xDistance <= attackRange && yDistance <= yTolerance;
        bool tooCloseToPlayer = xDistance <= stopDistance;

        if (inAttackRange)
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

            if (!tooCloseToPlayer)
            {
                animator.SetBool("Walking", true);

                // Hareket sadece X yönünde olacaksa şu satırı kullan:
                Vector2 direction = new Vector2(Mathf.Sign(directionToPlayer.x), 0);

                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            }
            else
            {
                animator.SetBool("Walking", false);
                rb.linearVelocity = Vector2.zero;
            }
        }
    }


    IEnumerator AttackRoutine()
    {
        isAttacking = true;
        lastAttackTime = Time.time;

        // Biraz bekle → hasarın animasyonla senkron olması için (örnek: 0.3 saniye sonra vurur)
        yield return new WaitForSeconds(0.3f);

        // Hâlâ hedef varsa ve hâlâ saldırıyorsa vur
        if (player != null && isAttacking)
        {
            float xDistance = Mathf.Abs(transform.position.x - player.position.x);
            float yDistance = Mathf.Abs(transform.position.y - player.position.y);

            if (xDistance <= attackRange && yDistance <= yTolerance)
            {
                PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage);
                }
            }
        }
        // Saldırı tamamlanınca
        yield return new WaitForSeconds(attackDuration - 0.3f); // animasyonun kalan süresi
        isAttacking = false;
    }
}
