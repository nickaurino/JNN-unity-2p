using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float moveSpeed = 300f;
    public float jumpForce = 5f;
    private bool isJumping = false;
    private bool isInWater = false;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Debug.Log("Hello World!");

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && !isJumping)
        {
            // Apply an upward force to initiate the jump
            rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            isJumping = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("water"))
        {
            isInWater = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
        if (collision.gameObject.CompareTag("water"))
        {
            isJumping = false;
            Debug.Log("test");
        }
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetKey(KeyCode.D) ? 1f : Input.GetKey(KeyCode.A) ? -1f : 0f;
        float moveVertical = 0f;

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        movement = movement.normalized * moveSpeed * Time.fixedDeltaTime;

        rb.velocity = new Vector2(movement.x, rb.velocity.y);

        if (isInWater)
        {
            moveHorizontal = Input.GetAxis("Horizontal");
            moveVertical = Input.GetAxis("Vertical");

            movement = new Vector2(moveHorizontal, moveVertical);
            movement = movement.normalized * moveSpeed * Time.deltaTime;

            rb.velocity = movement;
        }
    }
}
