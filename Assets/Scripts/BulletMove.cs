using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(this.gameObject,5);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += this.transform.right * speed * Time.deltaTime;
    }
}
