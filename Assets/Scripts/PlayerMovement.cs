using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float currentSpeed;
    public GameObject Slash;
    public Transform firePoint;
    public float slashSpeed = 10f;
    public float slashLifetime = 0.3f;
    public float slashCooldown = 0.5f;

    private Vector2 lastMoveDirection = Vector2.right;
    private bool canSlash = true;
    private Vector2 moveInput;

    private void Start()
    {
        currentSpeed = walkSpeed;
    }

    void FixedUpdate()
    {
        if (moveInput.sqrMagnitude > 0.01f)
        {
            lastMoveDirection = moveInput.normalized;
        }

        transform.Translate(moveInput.normalized * currentSpeed * Time.deltaTime);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed && canSlash)
        {
            StartCoroutine(PerformSlash());
        }
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            currentSpeed = runSpeed;
        }

        if (context.canceled)
        {
            currentSpeed = walkSpeed;
        }
    }

    IEnumerator PerformSlash()
    {
        canSlash = false;

        float angle = Mathf.Atan2(lastMoveDirection.y, lastMoveDirection.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);

        GameObject slash = Instantiate(Slash, firePoint.position, rotation);
        slash.SetActive(true);

        Rigidbody2D rb = slash.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = lastMoveDirection * slashSpeed;
        }

        Destroy(slash, slashLifetime);

        yield return new WaitForSeconds(slashCooldown);
        canSlash = true;
    }
}
