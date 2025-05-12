using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator animator;

    public int facingDirection = 1; // 1 for right, -1 for left
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = moveSpeed * moveInput;
    }

    public void Move(InputAction.CallbackContext context)
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

    public void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
}
