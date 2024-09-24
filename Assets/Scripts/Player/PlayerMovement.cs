using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float forwardForce = 2000f;
    public float sidewaysForce = 80f; // Default value for non-mobile platforms
    public float jumpForce = 300f;
    public LayerMask groundLayers; // Layer to specify what is considered "ground"
    public float groundCheckRadius = 0.3f; // Radius to check for the ground below the player
    public Transform groundCheck; // Empty GameObject to define where to check for ground

    private bool isGrounded;
    private bool hasJumped; // To track if the player has jumped once in the current level

    private void Start()
    {
        // Check if running on mobile and reduce sideways force
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            sidewaysForce *= 0.5f; // Reduce side force by 50% for mobile platforms
        }

        isGrounded = true; // Start with the assumption that the player is grounded
        hasJumped = false; // Player has not jumped yet in the current level
    }

    private void FixedUpdate()
    {
        // Add a forward force
        rb.AddForce(0, 0, forwardForce * Time.deltaTime);

        // Check if the player is on the ground using Physics.OverlapSphere
        CheckIfGrounded();

        // Handle all types of inputs: Keyboard, Mouse, and Mobile
        HandleInput();

        // Check if the player's position is below a certain threshold
        if (rb.position.y < -1f)
        {
            FindObjectOfType<GameManager>().EndGame();
        }
    }

    private void HandleInput()
    {
        // Handle Keyboard Input
        HandleKeyboardInput();

        // Handle Mouse Input
        HandleMouseInput();

        // Handle Mobile Touch Input
        HandleMobileInput();
    }

    private void HandleKeyboardInput()
    {
        // Keyboard movement: A/D or Arrow keys
        if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddForce(sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

        if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddForce(-sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

        // Jump with Spacebar if grounded and hasn't jumped yet
        if (isGrounded && !hasJumped && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void HandleMouseInput()
    {
        // Handle Mouse Click Input
        if (Input.GetMouseButton(0)) // Left mouse button or single touch on mobile
        {
            Vector3 mousePos = Input.mousePosition;

            // Check if the click is on the left side of the screen
            if (mousePos.x < Screen.width / 2)
            {
                // Move left
                rb.AddForce(-sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
            }
            // Check if the click is on the right side of the screen
            else if (mousePos.x > Screen.width / 2)
            {
                // Move right
                rb.AddForce(sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
            }
        }
    }

    private void HandleMobileInput()
    {
        // Handle Mobile Touch Input
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);

                if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Stationary)
                {
                    // Check if the touch is on the left side of the screen
                    if (touch.position.x < Screen.width / 2)
                    {
                        // Move left
                        rb.AddForce(-sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
                    }
                    // Check if the touch is on the right side of the screen
                    else if (touch.position.x > Screen.width / 2)
                    {
                        // Move right
                        rb.AddForce(sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
                    }
                }

                // Optional: Jump with a double-tap gesture
                if (isGrounded && !hasJumped && touch.tapCount == 2)
                {
                    Jump();
                }
            }
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