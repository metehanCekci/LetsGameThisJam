using UnityEngine;

public class EnemyHealthScript : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public float flashDuration = 0.1f;
    public GameObject healthOrbPrefab;

    private bool hasTakenChainDamage = false;

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

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"{gameObject.name} took {damage} damage. Health: {currentHealth}");

        if (spriteRenderer != null)
            StartCoroutine(FlashRed());

        if (cameraShake != null)
            cameraShake.ShakeCamera(2f, 0.1f);

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

    public void TakeDamageFromPlayer(int damage, Transform attacker)
    {
        if (hasTakenChainDamage) return;

        hasTakenChainDamage = true;
        TakeDamage(damage);

        if (GameManagerScript.instance.hasElectricSword)
        {
            ElectricChain(attacker);
        }

        Invoke(nameof(ResetChainFlag), 0.1f);
    }

    void ResetChainFlag()
    {
        hasTakenChainDamage = false;
    }

    void ElectricChain(Transform attacker)
    {
        float chainRange = 4f;
        int maxChains = 3;
        int chains = 0;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, chainRange);

        foreach (var enemy in hitEnemies)
        {
            if (enemy.gameObject == gameObject) continue;
            if (!enemy.CompareTag("Enemy")) continue;

            var enemyHealth = enemy.GetComponent<EnemyHealthScript>();
            if (enemyHealth != null && enemyHealth.currentHealth > 0)
            {
                enemyHealth.TakeDamageFromPlayer(Mathf.RoundToInt(GameManagerScript.instance.AttackPower), transform);
                chains++;
                if (chains >= maxChains)
                    break;
            }
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
        if (healthOrbPrefab != null)
            Instantiate(healthOrbPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

    public event System.Action OnDeath;
}
