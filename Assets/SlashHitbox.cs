using UnityEngine;

public class SlashHitbox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Düþmana vuruldu!");
            
        }
    }
}
