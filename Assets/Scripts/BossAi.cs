using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class BossAi : MonoBehaviour
{
    public Sprite pointHand;
    public Sprite Hand;
    public Sprite Hand2;

    public Sprite SwipeHand;
    public Sprite SwipeHand2;
    GameObject LeftHand;
    GameObject RightHand;

    GameObject ProjectilesParent;
    Transform LeftRestPos;
    Transform RightRestPos;
    Transform LeftAttackPos;
    Transform RightAttackPos;

    Transform HandSwipeLUP;
    Transform HandSwipeRUP;
    Transform HandSwipeLDOWN;
    Transform HandSwipeRDOWN;

    public float attackMoveSpeed = 5f;
    public float attackPauseDuration = 1f;
    public float timeBetweenAttacks = 2f;

    public int bulletCount = 5;
    public float bulletSpreadAngle = 45f;
    public float bulletSpeed = 10f;

    void Start()
    {
        LeftHand = transform.GetChild(4).gameObject;
        RightHand = transform.GetChild(2).gameObject;

        ProjectilesParent = transform.GetChild(7).gameObject;

        LeftRestPos = transform.GetChild(3);
        RightRestPos = transform.GetChild(1);

        RightAttackPos = transform.GetChild(5);
        LeftAttackPos = transform.GetChild(6);

        HandSwipeLUP = transform.GetChild(8);
        HandSwipeLDOWN = transform.GetChild(10);
        HandSwipeRDOWN = transform.GetChild(11);
        HandSwipeRUP = transform.GetChild(9);
    }

    void Awake()
    {
        StartCoroutine(StartAttackCycle());
    }

    IEnumerator StartAttackCycle()
    {
        while (true)
        {
            int attackIndex = Random.Range(0, 3); // Now includes swipe

            if (attackIndex == 0)
            {
                yield return StartCoroutine(Impale());
            }
            else if (attackIndex == 1)
            {
                yield return StartCoroutine(Horde());
            }
            else
            {
                yield return StartCoroutine(Swipe());
            }

            yield return new WaitForSeconds(timeBetweenAttacks);
        }
    }



    IEnumerator Impale()
    {
        bool isLeft = Random.Range(0, 2) == 0;

        GameObject hand = isLeft ? LeftHand : RightHand;
        Transform attackPos = isLeft ? LeftAttackPos : RightAttackPos;
        Transform restPos = isLeft ? LeftRestPos : RightRestPos;

        //hand.GetComponent<SpriteRenderer>().sprite = pointHand;
        // Move hand to attack position
        yield return StartCoroutine(MoveToPosition(hand.transform, attackPos.position));

        // Shoot bullets
        StartCoroutine(ShootBulletsFrom(attackPos));

        // Wait for ShootBulletsFrom to complete
        yield return new WaitForSeconds(bulletCount * 0.1f); // Adjust time to match bullet spawning interval

        // Change sprite based on which hand performed the attack
        hand.GetComponent<SpriteRenderer>().sprite = isLeft ? Hand : Hand2;

        // Wait at attack position
        yield return new WaitForSeconds(attackPauseDuration);

        // Move hand back to rest position
        yield return StartCoroutine(MoveToPosition(hand.transform, restPos.position));
    }


    IEnumerator Horde()
    {
        int iterations = 4;
        float spawnInterval = 0.3f;
        int projectileCount = ProjectilesParent.transform.childCount;

        // Create a list of indices to randomly pick from
        List<int> availableIndices = new List<int>();
        for (int i = 0; i < projectileCount; i++)
        {
            availableIndices.Add(i);
        }

        // Clamp iterations to the available number of projectiles
        iterations = Mathf.Min(iterations, projectileCount);

        for (int i = 0; i < iterations; i++)
        {
            // Pick a random index from available ones
            int randListIndex = Random.Range(0, availableIndices.Count);
            int chosenIndex = availableIndices[randListIndex];
            availableIndices.RemoveAt(randListIndex);

            GameObject clone = Instantiate(ProjectilesParent.transform.GetChild(chosenIndex).gameObject);
            clone.SetActive(true);

            yield return new WaitForSeconds(spawnInterval);
        }
    }


    IEnumerator Swipe()
    {
        bool isLeft = Random.Range(0, 2) == 0;
        bool isUp = Random.Range(0, 2) == 0;

        GameObject hand = isLeft ? LeftHand : RightHand;
        Transform startPos = null;
        Transform endPos = null;

        if (isLeft && isUp)
        {
            startPos = HandSwipeLUP;
            endPos = HandSwipeRUP;
        }
        else if (isLeft && !isUp)
        {
            startPos = HandSwipeLDOWN;
            endPos = HandSwipeRDOWN;
        }
        else if (!isLeft && isUp)
        {
            startPos = HandSwipeRUP;
            endPos = HandSwipeLUP;
        }
        else
        {
            startPos = HandSwipeRDOWN;
            endPos = HandSwipeLDOWN;
        }

        Transform restPos = isLeft ? LeftRestPos : RightRestPos;

        // === Step 1: Move hand slowly to starting swipe position
        yield return StartCoroutine(MoveToPositionWithSpeed(hand.transform, startPos.position, attackMoveSpeed * 1.5f));

        hand.GetComponent<CircleCollider2D>().enabled = true;
        yield return StartCoroutine(MoveToPositionWithSpeed(hand.transform, endPos.position, attackMoveSpeed * 5f));
        hand.GetComponent<CircleCollider2D>().enabled = false;
        // === Step 3: Fast return to rest position
        yield return StartCoroutine(MoveToPositionWithSpeed(hand.transform, restPos.position, attackMoveSpeed * 3f));
    }

    // New coroutine to move with different speed
    IEnumerator MoveToPositionWithSpeed(Transform hand, Vector3 target, float moveSpeed)
    {
        while (Vector3.Distance(hand.position, target) > 0.05f)
        {
            hand.position = Vector3.MoveTowards(hand.position, target, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }





    IEnumerator ShootBulletsFrom(Transform attackPos)
    {
        GameObject bulletPrefab = attackPos.GetChild(0).gameObject;

        float angleStep = bulletSpreadAngle / (bulletCount - 1);
        float startAngle = -bulletSpreadAngle / 2f;

        bool isLeft = (attackPos == LeftAttackPos);

        for (int i = 0; i < bulletCount; i++)
        {
            float angleOffset = startAngle + angleStep * i;
            float finalAngle = isLeft ? angleOffset : -angleOffset;

            // Add the spread angle to the bullet's original rotation
            Quaternion addedRotation = Quaternion.Euler(0, 0, finalAngle);
            Quaternion finalRotation = bulletPrefab.transform.rotation * addedRotation;

            GameObject bullet = Instantiate(bulletPrefab, attackPos.position, finalRotation);
            bullet.SetActive(true);

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 direction = finalRotation * Vector2.right;
                rb.linearVelocity = direction.normalized * bulletSpeed;
            }

            yield return new WaitForSeconds(0.1f);
        }
    }



    IEnumerator MoveToPosition(Transform hand, Vector3 target)
    {
        while (Vector3.Distance(hand.position, target) > 0.05f)
        {
            hand.position = Vector3.MoveTowards(hand.position, target, attackMoveSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
