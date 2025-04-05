using System.Collections;
using UnityEngine;

public class RangedEnemyAi : MonoBehaviour
{
    public float moveSpeed = 3f;              // Speed at which the enemy follows the player
    public float followRange = 10f;           // The distance at which the enemy starts following the player
    public float attackRange = 5f;            // The range at which the enemy will start shooting
    public float shootDelay = 1f;             // Delay between bursts of shooting
    public int burstCount = 3;                // Number of bullets per burst
    public float timeBetweenShots = 0.1f;    // Time between each bullet in a burst
    public GameObject bulletPrefab;           // Bullet prefab to shoot

    private Transform player;                 // Reference to the player
    private bool isShooting = false;          // Whether the enemy is currently shooting
    private bool isFollowing = false;         // Whether the enemy is currently following the player

    void Start()
    {
        // Find the player in the scene by tag
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Check the distance between the enemy and the player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // If the player is within the follow range, move toward the player
        if (distanceToPlayer <= followRange)
        {
            isFollowing = true;
        }
        else
        {
            isFollowing = false;
        }

        // If following, move the enemy toward the player
        if (isFollowing && distanceToPlayer > attackRange)
        {
            FollowPlayer();
        }

        // If within shooting range, start shooting in bursts
        if (distanceToPlayer <= attackRange && !isShooting)
        {
            StartCoroutine(ShootBurst());
        }
    }

    // Function to move the enemy towards the player
    void FollowPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }

    // Coroutine to handle shooting bursts
    IEnumerator ShootBurst()
    {
        isShooting = true;

        for (int i = 0; i < burstCount; i++)
        {
            ShootBullet();  // Shoot one bullet
            yield return new WaitForSeconds(timeBetweenShots);  // Wait for the next shot in the burst
        }

        // Wait for the burst delay before shooting again
        yield return new WaitForSeconds(shootDelay);
        isShooting = false;
    }

    // Function to shoot a single bullet
    void ShootBullet()
    {
        // Instantiate the bullet at the enemy's position and point it towards the player
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.SetActive(true);  // Activate the bullet
        Vector2 direction = (player.position - transform.position).normalized;

        // Set bullet rotation so it faces the player
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

    }
}
