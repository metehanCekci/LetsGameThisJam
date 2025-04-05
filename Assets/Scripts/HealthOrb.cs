using UnityEngine;

public class HealthOrb : MonoBehaviour
{
    public int healAmount = 5;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManagerScript.instance.Health += healAmount;
            if (GameManagerScript.instance.Health > GameManagerScript.instance.MaxHealth)
            {
                GameManagerScript.instance.Health = GameManagerScript.instance.MaxHealth;
            }

            Destroy(gameObject);
        }
    }
}
