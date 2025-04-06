using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public GameObject Slash;
    public GameObject GameManager;
    public Transform firePoint;
    public float slashLifetime = 0.2f;
    public float currentMoveSpeed;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private Vector2 lastMoveDirection = Vector2.right;
    private bool canSlash = true;
    private Vector2 moveInput;
    private bool isSprinting = false;
    private Rigidbody2D rb;

    private void Start()
    {
        if (GameManagerScript.instance == null)
            Instantiate(GameManager);
        currentMoveSpeed = GameManagerScript.instance.WalkSpeed;
        GameManagerScript.instance.Stamina = GameManagerScript.instance.MaxStamina;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (moveInput.sqrMagnitude > 0.01f)
        {
            lastMoveDirection = moveInput.normalized;
        }

        if (isSprinting && GameManagerScript.instance.Stamina > 0)
        {
            GameManagerScript.instance.Stamina -= 25f * Time.fixedDeltaTime;
            if (GameManagerScript.instance.Stamina <= 0)
            {
                GameManagerScript.instance.Stamina = 0;
                StopSprinting();
            }
        }
        else if (!isSprinting && GameManagerScript.instance.Stamina < GameManagerScript.instance.MaxStamina)
        {
            GameManagerScript.instance.Stamina += 15f * Time.fixedDeltaTime;
            if (GameManagerScript.instance.Stamina > GameManagerScript.instance.MaxStamina)
                GameManagerScript.instance.Stamina = GameManagerScript.instance.MaxStamina;
        }

        MovePlayer();
    }

    void MovePlayer()
    {
        rb.linearVelocity = new Vector2(moveInput.x * currentMoveSpeed, moveInput.y * currentMoveSpeed);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        if (moveInput.sqrMagnitude < 0.01f)
        {
            animator.SetBool("Walking", false);
            return;
        }

        SetFacingDirection(moveInput);
        animator.SetBool("Walking", true);

        spriteRenderer.flipX = moveInput.x < 0;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed && canSlash)
        {
            SetFacingDirection(lastMoveDirection);
            SFXScript.Instance.PlaySlash();
            animator.SetBool("Attacking", true);
            StartCoroutine(PerformSlash());
        }
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed && GameManagerScript.instance.Stamina > 0)
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
        currentMoveSpeed = GameManagerScript.instance.RunSpeed;
    }

    void StopSprinting()
    {
        isSprinting = false;
        currentMoveSpeed = GameManagerScript.instance.WalkSpeed;
    }

    IEnumerator PerformSlash()
    {
        canSlash = false;

        Vector3 spawnOffset = Vector3.zero;

        if (lastMoveDirection.x > 0.5f)
            spawnOffset = Vector3.right * GameManagerScript.instance.AttackRange;
        else if (lastMoveDirection.x < -0.5f)
            spawnOffset = Vector3.left * GameManagerScript.instance.AttackRange;
        else if (lastMoveDirection.y > 0.6f)
            spawnOffset = Vector3.up * GameManagerScript.instance.AttackRange;
        else if (lastMoveDirection.y < -0.6f)
            spawnOffset = Vector3.down * GameManagerScript.instance.AttackRange;

        Vector3 spawnPosition = firePoint.position + spawnOffset;

        GameObject slash = Instantiate(Slash, spawnPosition, Quaternion.identity);
        slash.transform.parent = Slash.transform.parent;
        slash.SetActive(true);

        Destroy(slash, slashLifetime);
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("Attacking", false);
        yield return new WaitForSeconds(GameManagerScript.instance.AttackCooldown - 0.2f);
        canSlash = true;
    }

    private void SetFacingDirection(Vector2 dir)
    {
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            // Sağa veya sola bakıyorsa ikisini de true yap
            animator.SetBool("goingRight", true);
            animator.SetBool("goingLeft", true);
            animator.SetBool("goingUp", false);
            animator.SetBool("goingDown", false);
        }
        else
        {
            // Yukarı veya aşağı bakıyorsa yatayları false yap
            animator.SetBool("goingRight", false);
            animator.SetBool("goingLeft", false);
            animator.SetBool("goingUp", dir.y > 0);
            animator.SetBool("goingDown", dir.y < 0);
        }
    }

}
