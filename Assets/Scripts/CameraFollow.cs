using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 5f;
    public Vector2 runOffset = new Vector2(1.5f, 0f); 

    void LateUpdate()
    {
        if (target == null) return;

        
        Vector3 basePosition = new Vector3(target.position.x, target.position.y, transform.position.z);

       
        if (Input.GetKey(KeyCode.LeftShift))
        {
            basePosition += new Vector3(runOffset.x, runOffset.y, 0f);
        }

    
        transform.position = Vector3.Lerp(transform.position, basePosition, smoothSpeed * Time.deltaTime);
    }
}
