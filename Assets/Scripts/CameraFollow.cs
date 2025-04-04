using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float walkSmoothSpeed = 8f;
    public float runSmoothSpeed = 3f;
    public Vector2 runOffset = new Vector2(1.5f, 0f);

    void LateUpdate()
    {
        if (target == null) return;

        float currentSmoothSpeed = Input.GetKey(KeyCode.LeftShift) ? runSmoothSpeed : walkSmoothSpeed;

        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            targetPosition += new Vector3(runOffset.x, runOffset.y, 0f);
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, currentSmoothSpeed * Time.deltaTime);
    }
}
