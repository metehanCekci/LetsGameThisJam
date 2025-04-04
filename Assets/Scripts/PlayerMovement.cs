using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 10f;

    void Update()
    {
        Vector2 moveDirection = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
            moveDirection += Vector2.up;
        if (Input.GetKey(KeyCode.S))
            moveDirection += Vector2.down;
        if (Input.GetKey(KeyCode.A))
            moveDirection += Vector2.left;
        if (Input.GetKey(KeyCode.D))
            moveDirection += Vector2.right;

        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        transform.Translate(moveDirection.normalized * currentSpeed * Time.deltaTime);
    }
}
