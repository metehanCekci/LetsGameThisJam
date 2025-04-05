using System.Collections;
using UnityEngine;

public class BossAi : MonoBehaviour
{
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

        // Move hand to attack position
        yield return StartCoroutine(MoveToPosition(hand.transform, attackPos.position));

        // Shoot bullets
        StartCoroutine(ShootBulletsFrom(attackPos));

        // Wait at attack position
        yield return new WaitForSeconds(attackPauseDuration);

        // Move hand back to rest position
        yield return StartCoroutine(MoveToPosition(hand.transform, restPos.position));
    }

    IEnumerator Horde()
    {
        int iterations = 6;
        float spawnInterval = 0.2f;
        int projectileCount = ProjectilesParent.transform.childCount;

        for (int i = 0; i < iterations; i++)
        {
            int randIndex = Random.Range(0, projectileCount);
            GameObject clone = Instantiate(ProjectilesParent.transform.GetChild(randIndex).gameObject);
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
    yield return StartCoroutine(MoveToPosition(hand.transform, startPos.position));

    // === Step 2: Fast swipe to end position
    yield return StartCoroutine(MoveToPosition(hand.transform, endPos.position));

    // === Step 3: Fast return to rest position
    yield return StartCoroutine(MoveToPosition(hand.transform, restPos.position));
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
