using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damageAmount = 1;  // Amount of damage the enemy deals

    void OnCollisionEnter(Collision collision)
    {
        // Check if the enemy collides with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Get the PlayerHealth component from the player
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

            // Apply damage to the player and pass the enemy's position as the source of damage
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount, transform.position);  // Pass enemy's position
            }
        }
    }
}
