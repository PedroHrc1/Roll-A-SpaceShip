using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;

    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    // Reference to the PlayerHealth script
    public PlayerHealth playerHealth;

    // Reference to the Restart Button
    public GameObject restartButton;

    // Threshold for falling off the map (Y-axis value)
    public float fallThreshold = -10f;

    // Cronometro (Timer)
    public TextMeshProUGUI timerText;
    private float startTime;
    private bool isRunning = true;

    // Reference to the player's collider
    private Collider playerCollider;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<Collider>();  // Get the player's collider
        count = 0;
        winTextObject.SetActive(false);

        // Make sure the restart button is hidden at the start
        if (restartButton != null)
        {
            restartButton.SetActive(false);
        }

        // Start the timer
        startTime = Time.time;
        SetCountText();
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if (count >= 12)  // When the player wins (collected all pickups)
        {
            winTextObject.SetActive(true);
            StopGame();

            // Show the restart button when the player wins
            if (restartButton != null)
            {
                restartButton.SetActive(true);
            }

            // Make the player immortal, disable collisions, and prevent falling
            MakePlayerImmortalAndStopFalling();
        }
    }

    void FixedUpdate()
    {
        if (playerHealth != null && playerHealth.IsPlayerAlive())
        {
            // Apply player movement
            Vector3 movement = new Vector3(movementX, 0.0f, movementY);
            rb.AddForce(movement * speed);
        }

        // Update the timer, even if the player has fallen or is near death
        UpdateTimer();

        // Check if the player has fallen below the map (fallThreshold)
        if (transform.position.y < fallThreshold && playerHealth != null)
        {
            playerHealth.Die();  // Call Die() when the player falls
        }
    }

    private void UpdateTimer()
    {
        if (isRunning)
        {
            float t = Time.time - startTime;

            int minutes = ((int)t / 60);
            int seconds = ((int)t % 60);
            float milliseconds = (t * 100) % 100;

            timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;

            SetCountText();
        }
    }

    public void StopGame()
    {
        isRunning = false;  // Stop the timer when the game ends (either win or lose)
    }

    // Make the player immortal, disable collisions, and stop falling
    private void MakePlayerImmortalAndStopFalling()
    {
        // Disable the player's health script so they can't take damage
        if (playerHealth != null)
        {
            playerHealth.enabled = false;  // Disable health script to stop taking damage
        }

        // Set the player's collider to be a trigger to avoid collisions
        if (playerCollider != null)
        {
            playerCollider.isTrigger = true;  // Enable trigger to stop physical collisions
        }

        // Disable the player's gravity and freeze Y-axis movement
        if (rb != null)
        {
            rb.useGravity = false;  // Disable gravity so the player won't fall down
            rb.velocity = Vector3.zero;  // Stop any current movement
            rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;  // Freeze Y-axis position and all rotations
        }
    }
}
