// EnemyHealthScript.cs

using UnityEngine;

public class EnemyHealthScript : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public float flashDuration = 0.1f;

    private CameraShake cameraShake;

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        cameraShake = FindAnyObjectByType<CameraShake>(); // Kamera sarsıntısını bul

        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hitbox"))
        {
            TakeDamage(10);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"{gameObject.name} took {damage} damage. Health: {currentHealth}");

        if (spriteRenderer != null)
        {
            StartCoroutine(FlashRed());
        }

        if (cameraShake != null)
        {
            cameraShake.ShakeCamera(5f, 0.2f); // Singleton yok, referans ile çağır
        }

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

    public event System.Action OnDeath;

    void Die()
    {
        OnDeath?.Invoke();
        Destroy(gameObject);
    }
}
