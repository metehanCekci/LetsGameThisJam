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
    public bool currentDirXorY;
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
        animator = this.gameObject.GetComponent<Animator>();
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        rb = this.gameObject.GetComponent<Rigidbody2D>();  // Reference to Rigidbody2D
    }

    void FixedUpdate()
    {
        if (moveInput.sqrMagnitude > 0.01f)
        {
            lastMoveDirection = moveInput.normalized;
        }

        if (isSprinting && GameManagerScript.instance.Stamina > 0)
        {
            GameManagerScript.instance.Stamina -= 25f * Time.fixedDeltaTime; // Using fixed drain rate for now
            if (GameManagerScript.instance.Stamina <= 0)
            {
                GameManagerScript.instance.Stamina = 0;
                StopSprinting();
            }
        }
        else if (!isSprinting && GameManagerScript.instance.Stamina < GameManagerScript.instance.MaxStamina)
        {
            GameManagerScript.instance.Stamina += 15f * Time.fixedDeltaTime; // Using fixed regen rate for now
            if (GameManagerScript.instance.Stamina > GameManagerScript.instance.MaxStamina)
                GameManagerScript.instance.Stamina = GameManagerScript.instance.MaxStamina;
        }

        MovePlayer(); // Use Rigidbody2D for movement
    }

    void MovePlayer()
    {
        // Update the velocity of the Rigidbody2D directly for movement
        rb.linearVelocity = new Vector2(moveInput.x * currentMoveSpeed, moveInput.y * currentMoveSpeed);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        
        moveInput = context.ReadValue<Vector2>();


        if (moveInput.sqrMagnitude < 0.01f)
        {
            animator.SetBool("goingUp", false);
            animator.SetBool("goingDown", false);
            animator.SetBool("goingLeft", false);
            animator.SetBool("goingRight", false);
            if (currentDirXorY)
            {
                animator.SetBool("CurrentDirXorY", true);
            }
            else
            {
                animator.SetBool("CurrentDirXorY", false);
            }

            return;
        }

        if (Mathf.Abs(moveInput.x) > Mathf.Abs(moveInput.y))
        {
            if (moveInput.x > 0)//right
            {
                animator.SetBool("goingRight", true);
                animator.SetBool("goingLeft", true);
                animator.SetBool("goingUp", false);
                animator.SetBool("goingDown", false);
                currentDirXorY = true; //true X false Y
                spriteRenderer.flipX = false;
                animator.SetBool("lookingDown", false);
            }
            else//left
            {
                animator.SetBool("goingRight", true);
                animator.SetBool("goingLeft", true);
                animator.SetBool("goingUp", false);
                animator.SetBool("goingDown", false);
                currentDirXorY = true;
                spriteRenderer.flipX = true;
                animator.SetBool("lookingDown", false);
            }
        }
        else
        {
            if (moveInput.y > 0)//up
            {
                animator.SetBool("goingUp", true);
                animator.SetBool("goingDown", false);
                animator.SetBool("goingRight", false);
                animator.SetBool("goingLeft", false);
                currentDirXorY = false;
                animator.SetBool("lookingDown", false);
            }
            else//down
            {
                animator.SetBool("goingUp", false);
                animator.SetBool("goingDown", true);
                animator.SetBool("goingRight", false);
                animator.SetBool("goingLeft", false);
                currentDirXorY = false;
                animator.SetBool("lookingDown", true);
            }
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed && canSlash)
        {
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
        // Speed is already set in GameManager so we just rely on that
    }

    void StopSprinting()
    {
        isSprinting = false;
        currentMoveSpeed = GameManagerScript.instance.WalkSpeed;
        // Same here, we just keep using RunSpeed regardless of state
    }

    IEnumerator PerformSlash()
    {
        canSlash = false;
        

        Vector3 spawnOffset = Vector3.zero;

        if (lastMoveDirection.x > 0.5f) // Sağ
            spawnOffset = Vector3.right * GameManagerScript.instance.AttackRange;
        else if (lastMoveDirection.x < -0.5f) // Sol
            spawnOffset = Vector3.left * GameManagerScript.instance.AttackRange;
        else if (lastMoveDirection.y > 0.6f) // Yukarı
            spawnOffset = Vector3.up * GameManagerScript.instance.AttackRange;
        else if (lastMoveDirection.y < -0.6f) // Aşağı
            spawnOffset = Vector3.down * GameManagerScript.instance.AttackRange;

        Vector3 spawnPosition = firePoint.position + spawnOffset;

        GameObject slash = Instantiate(Slash, spawnPosition, Quaternion.identity);
        slash.transform.parent = Slash.transform.parent;
        slash.SetActive(true);

        Destroy(slash, slashLifetime);
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("Attacking", false);
        yield return new WaitForSeconds(GameManagerScript.instance.AttackCooldown-0.2f);
        canSlash = true;
        
    }

}
