using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHealth : MonoBehaviour
{
    public GameObject UIStatusBars;

    public float iFrameDuration = 1f;
    public bool isInvincible = false;
    Transform DeathScreen;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private Collider2D playerCollider;

    public string enemyLayerName = "Enemy";

    void Start()
    {
        UIStatusBars = GameObject.FindGameObjectWithTag("UI");
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<Collider2D>();
        DeathScreen = UIStatusBars.transform.Find("DeathBackground");
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
        DeathScreen.gameObject.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        // �nce ka��nma kontrol� yap
        if (Random.value < GameManagerScript.instance.dodgeChance)
        {
            Debug.Log("Ka��n�ld�!");
            return;
        }

        // Sonra iframe kontrol�
        if (isInvincible) return;

        SFXScript.Instance.PlayHurt();
        GameManagerScript.instance.Health -= damage;

        Debug.Log($"Player took {damage} damage. Health: {GameManagerScript.instance.Health}");

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
        Time.timeScale = 0f;
        gameObject.GetComponent<PlayerMovement>().enabled = false;
        gameObject.GetComponent<PlayerInput>().enabled = false;
        DeathScreen.gameObject.SetActive(true);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hurtbox"))
        {
            TakeDamage(10);
        }
    }
}
