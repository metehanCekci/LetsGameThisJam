using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealthScript : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public bool isBoss = false;

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
            SFXScript.Instance.PlayHit();
            TakeDamage(10);
        }
    }

    public void TakeDamage(int damage)
    {
        // Kritik hasar hesaplama
        if (Random.value < GameManagerScript.instance.critChance)
        {
            damage = Mathf.RoundToInt(damage * GameManagerScript.instance.critMultiplier);
            Debug.Log("Kritik vuru�! Yeni Hasar: " + damage);
        }

        currentHealth -= damage;
        Debug.Log($"{gameObject.name} took {damage} damage. Health: {currentHealth}");

        if (spriteRenderer != null)
        {
            StartCoroutine(FlashRed());
        }

        if (cameraShake != null)
        {
            cameraShake.ShakeCamera(2f, 0.1f);
        }

        // Can �alma
        if (GameManagerScript.instance.Lifesteal > 0)
        {
            GameManagerScript.instance.Health += GameManagerScript.instance.Lifesteal;

            if (GameManagerScript.instance.Health > GameManagerScript.instance.MaxHealth)
                GameManagerScript.instance.Health = GameManagerScript.instance.MaxHealth;
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

        if (healthOrbPrefab != null)
        {
            Instantiate(healthOrbPrefab, transform.position, Quaternion.identity);
        }

        if(isBoss)SceneManager.LoadScene("End");

        Destroy(gameObject);
    }

    public void TakeDamageFromPlayer(int damage, Transform attacker)
    {
        float missingHealthRatio = 1f - (GameManagerScript.instance.Health / GameManagerScript.instance.MaxHealth);
        float bonusDamage = GameManagerScript.instance.missingHealthDamageBonus * missingHealthRatio * damage;

        int totalDamage = Mathf.RoundToInt(damage + bonusDamage);
        TakeDamage(totalDamage);
    }


}
