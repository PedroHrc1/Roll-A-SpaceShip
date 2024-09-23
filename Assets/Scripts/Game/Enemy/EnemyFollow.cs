using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    private Transform player;  // The player's transform
    public float speed = 2f;  // The speed at which the enemy will follow
    private bool isChasingPlayer = true;  // Flag to control whether the enemy is chasing the player
    private Rigidbody rb;  // If using Rigidbody for movement

    void Start()
    {
        rb = GetComponent<Rigidbody>();  // If using Rigidbody, initialize it here
    }

    // Method to set the player's transform
    public void SetPlayer(Transform playerTransform)
    {
        player = playerTransform;  // Set the player reference
    }

    void Update()
    {
        if (player != null && isChasingPlayer)
        {
            // Calculate direction towards the player
            Vector3 direction = (player.position - transform.position).normalized;

            // Move the enemy towards the player
            transform.position += direction * speed * Time.deltaTime;

            // If using Rigidbody, you could use the following instead:
            // rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
        }
    }

    // Method to stop the enemy from chasing the player
    public void StopChasingPlayer()
    {
        isChasingPlayer = false;
        Debug.Log("Enemy has stopped chasing the player");  // Check if this gets printed

        // If using Rigidbody, stop its movement
        if (rb != null)
        {
            rb.velocity = Vector3.zero;  // Stop any velocity if Rigidbody is used
        }
    }
}
