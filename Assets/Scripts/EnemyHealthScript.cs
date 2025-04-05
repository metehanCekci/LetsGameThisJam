// EnemyHealthScript.cs

using UnityEngine;

public class EnemyHealthScript : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public float flashDuration = 0.1f;
    public GameObject healthOrbPrefab;

    private CameraShake cameraShake;

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        cameraShake = FindAnyObjectByType<CameraShake>(); 

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
            cameraShake.ShakeCamera(2f, 0.1f); // Singleton yok, referans ile çağır
        }

        if (currentHealth <= 0)
        {
            Die();
        }
        if (GameManagerScript.instance.Lifesteal > 0)
        {
            GameManagerScript.instance.Health += GameManagerScript.instance.Lifesteal;

            
            if (GameManagerScript.instance.Health > GameManagerScript.instance.MaxHealth)
                GameManagerScript.instance.Health = GameManagerScript.instance.MaxHealth;
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

        if (healthOrbPrefab != null)
        {
            Instantiate(healthOrbPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }




}
