using UnityEngine;

public class WeaponAim : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireCooldown = 0.5f;

    private float cooldownTimer = 0f;

    void Update()
    {
        if (Camera.main == null || bulletPrefab == null || firePoint == null)
            return;

        
        if (cooldownTimer > 0f)
            cooldownTimer -= Time.deltaTime;

        
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        Vector2 direction = (mousePos - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        
        if (Input.GetMouseButton(0) && cooldownTimer <= 0f)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            
            cooldownTimer = fireCooldown;
        }
    }
}
