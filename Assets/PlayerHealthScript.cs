using UnityEngine;

public class PlayerHealthScript : MonoBehaviour
{
    public int maxHealth = 100;
    public float knockbackForce = 5f;
    public float flashDuration = 0.1f;

    private int currentHealth;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private Rigidbody2D rb;

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        rb = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hurtbox"))
        {
            TakeDamage(10, other.transform);
        }
    }

    void TakeDamage(int damage, Transform damageSource)
    {
        currentHealth -= damage;

        // Flash red
        StartCoroutine(FlashRed());

        // Knockback
        Vector2 knockbackDirection = (transform.position - damageSource.position).normalized;
        rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    System.Collections.IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = originalColor;
    }

    void Die()
    {
        Debug.Log("Player Died");
        // Oyuncu öldüğünde olacaklar buraya (örn. sahneyi yeniden başlatma)
        Destroy(gameObject);
    }
}
