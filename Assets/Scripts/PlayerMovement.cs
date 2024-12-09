using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool canJump = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
        if (Input.GetButtonDown("Jump") && canJump)
        {
            Jump();
        }
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        Vector2 velocity = rb.velocity;
        velocity.x = horizontal * moveSpeed;
        rb.velocity = velocity;
    }

    private void Jump()
    {
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        canJump = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player has collided with the ground layer
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            canJump = true; // Reset jump ability
        }
    }
}
