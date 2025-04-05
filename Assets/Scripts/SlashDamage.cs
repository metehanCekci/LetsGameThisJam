using UnityEngine;

public class SlashDamage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            var enemyHealth = other.GetComponent<EnemyHealthScript>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamageFromPlayer(Mathf.RoundToInt(GameManagerScript.instance.AttackPower), transform);
                Debug.Log("Ana düþmana vuruldu, zincir baþlatýldý.");
            }
        }
    }
}
