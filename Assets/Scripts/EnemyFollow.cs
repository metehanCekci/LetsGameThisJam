using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 2f;
    private Transform player;
    private Rigidbody2D rb;
    private EnemyMeleeAttack meleeAttack;

    private bool isStunned = false;
    private float stunDuration = 0.5f;

    void Start()
    {
        meleeAttack = GetComponent<EnemyMeleeAttack>();

        rb = GetComponent<Rigidbody2D>();
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void FixedUpdate()
    {
        if (player != null && !isStunned && (meleeAttack == null || !meleeAttack.isAttacking))
        {
            Vector2 direction = (player.position - transform.position).normalized;
            float characterFacingX = player.localScale.x;
            float enemyApproachX = direction.x;

            bool isApproachingFromBehind = (characterFacingX > 0 && enemyApproachX < 0) || (characterFacingX < 0 && enemyApproachX > 0);

            gameObject.GetComponent<SpriteRenderer>().flipX = !isApproachingFromBehind;

            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hitbox"))
        {
            StartCoroutine(StunCoroutine());
        }
    }

    System.Collections.IEnumerator StunCoroutine()
    {
        isStunned = true;
        yield return new WaitForSeconds(stunDuration);
        isStunned = false;
    }
}
