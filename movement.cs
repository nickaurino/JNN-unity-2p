using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2behavior : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private bool isJumping = false;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
    if (Input.GetKeyDown(KeyCode.UpArrow) && !isJumping)
    {
        // Apply an upward force to initiate the jump
        rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
        isJumping = true;
    }
    }

        private void FixedUpdate()
{
    float moveHorizontal = Input.GetKey(KeyCode.RightArrow) ? 1f : Input.GetKey(KeyCode.LeftArrow) ? -1f : 0f;
    float moveVertical = 0f;

    Vector2 movement = new Vector2(moveHorizontal, moveVertical);
    movement = movement.normalized * moveSpeed * Time.fixedDeltaTime;

    rb.velocity = new Vector2(movement.x, rb.velocity.y);
}

        private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("p1"))
        {
            // Reset the jump state when landing on the ground
            isJumping = false;
        }
    }
}
