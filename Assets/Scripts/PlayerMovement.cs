using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    public GameObject Slash;
    public GameObject GameManager;
    public Transform firePoint;
    public float slashLifetime = 0.2f;
    public int currentMoveSpeed;
    private Animator animator;
    public bool currentDirXorY;
    private SpriteRenderer spriteRenderer;


    private Vector2 lastMoveDirection = Vector2.right;
    private bool canSlash = true;
    private Vector2 moveInput;
    private bool isSprinting = false;

    private float currentStamina;

    private void Start()
    {
        if(GameManagerScript.instance == null)
        Instantiate(GameManager);
        currentMoveSpeed = GameManagerScript.instance.WalkSpeed;
        currentStamina = GameManagerScript.instance.MaxStamina;
        animator = this.gameObject.GetComponent<Animator>();
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (moveInput.sqrMagnitude > 0.01f)
        {
            lastMoveDirection = moveInput.normalized;
        }

        if (isSprinting && currentStamina > 0)
        {
            currentStamina -= 25f * Time.fixedDeltaTime; // Using fixed drain rate for now
            if (currentStamina <= 0)
            {
                currentStamina = 0;
                StopSprinting();
            }
        }
        else if (!isSprinting && currentStamina < GameManagerScript.instance.MaxStamina)
        {
            currentStamina += 15f * Time.fixedDeltaTime; // Using fixed regen rate for now
            if (currentStamina > GameManagerScript.instance.MaxStamina)
                currentStamina = GameManagerScript.instance.MaxStamina;
        }

        transform.Translate(moveInput.normalized * currentMoveSpeed * Time.deltaTime);
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

        Vector3 offset = (Vector3)(lastMoveDirection.normalized * GameManagerScript.instance.AttackRange);
        Vector3 spawnPosition = firePoint.position + offset;

        GameObject slash = Instantiate(Slash, spawnPosition, Quaternion.identity);
        slash.transform.parent = Slash.transform.parent;
        slash.SetActive(true);

        Destroy(slash, slashLifetime);

        yield return new WaitForSeconds(GameManagerScript.instance.AttackCooldown);
        canSlash = true;
    }
    
}
