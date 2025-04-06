using UnityEngine;

public class HeadTilt : MonoBehaviour
{
    public SpriteRenderer headRenderer;
    public Sprite headLeft;
    public Sprite headRight;
    public Sprite headMiddle;

    public float centerThreshold = 1f; // Range where the boss looks forward

    private Transform player;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        float distanceX = player.position.x - transform.position.x;

        if (Mathf.Abs(distanceX) < centerThreshold)
        {
            headRenderer.sprite = headMiddle;
        }
        else if (distanceX > 0)
        {
            headRenderer.sprite = headRight;
        }
        else
        {
            headRenderer.sprite = headLeft;
        }
    }
}
