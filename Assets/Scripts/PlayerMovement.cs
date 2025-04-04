using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float currentSpeed;

    public float maxStamina = 100f;
    public float currentStamina;
    public float staminaDrainRate = 25f;
    public float staminaRegenRate = 15f;

    public GameObject Slash;
    public Transform firePoint;
    public float slashLifetime = 0.2f;
    public float slashCooldown = 0.5f;

    private Vector2 lastMoveDirection = Vector2.right;
    private bool canSlash = true;
    private Vector2 moveInput;
    private bool isSprinting = false;

    private void Start()
    {
        currentSpeed = walkSpeed;
        currentStamina = maxStamina;
    }

    void FixedUpdate()
    {
        if (moveInput.sqrMagnitude > 0.01f)
        {
            lastMoveDirection = moveInput.normalized;
        }

        // Stamina kontrolü
        if (isSprinting && currentStamina > 0)
        {
            currentStamina -= staminaDrainRate * Time.fixedDeltaTime;
            if (currentStamina <= 0)
            {
                currentStamina = 0;
                StopSprinting();
            }
        }
        else if (!isSprinting && currentStamina < maxStamina)
        {
            currentStamina += staminaRegenRate * Time.fixedDeltaTime;
            if (currentStamina > maxStamina)
                currentStamina = maxStamina;
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
        if (context.performed && currentStamina > 0)
        {
            StartSprinting();
        }
        else if (context.canceled)
        {
            StopSprinting();
        }
    }

    void StartSprinting()
    {
        isSprinting = true;
        currentSpeed = runSpeed;
    }

    void StopSprinting()
    {
        isSprinting = false;
        currentSpeed = walkSpeed;
    }

    IEnumerator PerformSlash()
    {
        canSlash = false;

        Vector3 offset = (Vector3)(lastMoveDirection.normalized * 1f);
        Vector3 spawnPosition = firePoint.position + offset;

        GameObject slash = Instantiate(Slash, spawnPosition, Quaternion.identity);
        slash.transform.parent = Slash.transform.parent;
        slash.SetActive(true);

        Destroy(slash, slashLifetime);

        yield return new WaitForSeconds(slashCooldown);
        canSlash = true;
    }
}
