using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{


    public float iFrameDuration = 1f;
    private bool isInvincible = false;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private Collider2D playerCollider;

    public string enemyLayerName = "Enemy";

    void Start()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<Collider2D>();

        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible) return;

        GameManagerScript.instance.Health -= damage;
        

        if (GameManagerScript.instance.Health <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(InvincibilityFrames());
        }
    }

    IEnumerator InvincibilityFrames()
    {
        isInvincible = true;

        // Make semi-transparent
        spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.5f);

        // Ignore collisions with enemy layer
        int enemyLayer = LayerMask.NameToLayer(enemyLayerName);
        Physics2D.IgnoreLayerCollision(gameObject.layer, enemyLayer, true);

        yield return new WaitForSeconds(iFrameDuration);

        // Restore appearance and collisions
        spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1);
        Physics2D.IgnoreLayerCollision(gameObject.layer, enemyLayer, false);

        isInvincible = false;
    }

    void Die()
    {
        Debug.Log("Player has died.");
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hurtbox"))
        {
            TakeDamage(10);
        }
    }
}
