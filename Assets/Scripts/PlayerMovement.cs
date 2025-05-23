using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    public int facingDirection = 1; // 1 for right, -1 for left
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator animator;
    private bool isKnockedBack;
    public Player_Combat playerCombat; // Reference to the Player_Combat script
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Attack"))
        {
            playerCombat.Attack();
        }
        if (!isKnockedBack)
            {
                rb.velocity = moveSpeed * moveInput;
            }
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (isKnockedBack == false)
        {
            // Read the movement input
            moveInput = context.ReadValue<Vector2>();

            // Extract horizontal and vertical components
            float horizontal = moveInput.x;
            float vertical = moveInput.y;

            // Update animator parameters
            animator.SetFloat("horizontal", Mathf.Abs(horizontal));
            animator.SetFloat("vertical", Mathf.Abs(vertical));

            // Flip the character based on horizontal movement
            if (horizontal > 0 && transform.localScale.x < 0 ||
                horizontal < 0 && transform.localScale.x > 0)
            {
                Flip();
            }
        }
    }

    public void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    public void Knockback(Transform enemy, float force, float stunTime)
    {
        isKnockedBack = true;
        Vector2 direction = (transform.position - enemy.position).normalized;
        if(direction == Vector2.zero)
        {
            direction = new Vector2(facingDirection, 0); // Default direction if the player is at the same position as the enemy
        }
        direction = new Vector2(Mathf.Sign(direction.x), 0);
        rb.velocity = direction * force;
        StartCoroutine(KnockbackCounter(stunTime));
    }

    IEnumerator KnockbackCounter(float stunTime)
    {
        yield return new WaitForSeconds(stunTime);
        rb.velocity = Vector2.zero;
        isKnockedBack = false;
    }
}
