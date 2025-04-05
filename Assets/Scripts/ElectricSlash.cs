using UnityEngine;

public class ElectricSlash : MonoBehaviour
{
    public float chainRange = 3f;
    public int maxChains = 3;
    public LayerMask enemyLayer;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;

      
        ChainToNearbyEnemies(other.transform, maxChains);
    }

    void ChainToNearbyEnemies(Transform origin, int chainsLeft)
    {
        if (chainsLeft <= 0) return;

        Collider2D[] hits = Physics2D.OverlapCircleAll(origin.position, chainRange, enemyLayer);

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Enemy") && hit.transform != origin)
            {
                // Hasar ver
                hit.GetComponent<EnemyHealthScript>()?.TakeDamage((int)GameManagerScript.instance.AttackPower);

                // Zinciri devam ettir
                ChainToNearbyEnemies(hit.transform, chainsLeft - 1);
                break; // sadece bir sonraki düþmana sek
            }
        }
    }
}
