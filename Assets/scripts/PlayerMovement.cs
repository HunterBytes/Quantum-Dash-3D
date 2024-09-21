using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float forwardForce = 2000f;
    public float sidewaysForce = 500f;
    public float jumpForce = 300f;
    public LayerMask groundLayers; // Layer to specify what is considered "ground"
    public float groundCheckRadius = 0.3f; // Radius to check for the ground below the player
    public Transform groundCheck; // Empty GameObject to define where to check for ground

    private bool isGrounded;
    private bool hasJumped; // To track if the player has jumped once in the current level

    private void Start()
    {
        isGrounded = true; // Start with the assumption that the player is grounded
        hasJumped = false; // Player has not jumped yet in the current level
    }

    private void FixedUpdate()
    {
        // Add a forward force
        rb.AddForce(0, 0, forwardForce * Time.deltaTime);

        // Check if the player is on the ground using Physics.OverlapSphere
        CheckIfGrounded();

        if (Input.GetKey("d"))
        {
            rb.AddForce(sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

        if (Input.GetKey("a"))
        {
            rb.AddForce(-sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

        // Jump when the space key is pressed, the player is grounded, and has not jumped yet in this level
        if (isGrounded && !hasJumped && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        // Check if the player's position is below a certain threshold
        if (rb.position.y < -1f)
        {
            FindObjectOfType<GameManager>().EndGame();
        }
    }

    private void Jump()
    {
        Debug.Log("Jumping!"); // Debug message to confirm jump action
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        hasJumped = true; // Set hasJumped to true after jumping to prevent further jumps in this level
    }

    // Method to check if the player is on the ground using Physics.OverlapSphere
    private void CheckIfGrounded()
    {
        // Check if the player is within the ground layer using a small overlap sphere at the player's feet
        Collider[] colliders = Physics.OverlapSphere(groundCheck.position, groundCheckRadius, groundLayers);
        if (colliders.Length > 0)
        {
            isGrounded = true; // Player is on the ground
        }
        else
        {
            isGrounded = false; // Player is not on the ground
        }

        // Visualize the sphere in the Scene view for debugging
        Debug.DrawRay(groundCheck.position, Vector3.down * groundCheckRadius, Color.red);
    }

    // Method to reset the jump status for the new level
    public void ResetJumpStatus()
    {
        hasJumped = false; // Allow jumping again when a new level starts
    }
}