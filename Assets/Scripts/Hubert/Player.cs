using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject player;
    public Rigidbody2D rb;

    private float speed = 3f;
    private bool canJump = false;
    private float jumpForce = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Jump();
    }

    void Movement()
    {
        // Continuous movement for right and left
        float move = 0f;

        if (Input.GetKey(KeyCode.D)) // Move right
        {
            move = 1f;
        }
        if (Input.GetKey(KeyCode.A)) // Move left
        {
            move = -1f;
        }

        // Apply movement over time
        rb.linearVelocity = new Vector2(move * speed, rb.linearVelocity.y); // Update only the x velocity

        // Optional: Flip the player's sprite based on direction (for visual effect)
        if (move > 0)
        {
            player.transform.localScale = new Vector3(1f, 1f, 1f); // Facing right
        }
        else if (move < 0)
        {
            player.transform.localScale = new Vector3(-1f, 1f, 1f); // Facing left
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump) // Jump when pressing space
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); // Apply jump force on the y-axis
            canJump = false; // Disable further jumping until landing
        }
    }

    // This function checks if the player is grounded (for jumping)
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // Check if the player is touching the ground
        {
            canJump = true; // Allow jumping again
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // Player leaves the ground
        {
            canJump = false; // Disable jumping while in the air
        }
    }
}
