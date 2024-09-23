using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    private bool isAlive = true;

    public Rigidbody playerRigidbody;
    public TextMeshProUGUI lifeCounterText;

    public SpaceshipController playerController;  // Reference to player controller
    private EnemyFollow[] allEnemies;

    // Die screen UI and restart button
    public GameObject dieTextObject;
    public GameObject restartButton;

    // Knockback parameters
    public float knockbackForce = 5f;  // Strength of knockback
    public float knockbackDuration = 0.1f;  // Duration for disabling movement

    private bool isKnockedBack = false;  // Prevent movement during knockback

    void Start()
    {
        currentHealth = maxHealth;
        UpdateLifeCounterUI();

        if (dieTextObject != null)
            dieTextObject.SetActive(false);

        if (restartButton != null)
            restartButton.SetActive(false);

        allEnemies = FindObjectsOfType<EnemyFollow>();
    }

    public void TakeDamage(int amount, Vector3 damageSourcePosition)
    {
        currentHealth -= amount;
        UpdateLifeCounterUI();

        if (currentHealth > 0)
        {
            // Apply knockback effect
            Knockback(damageSourcePosition);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Knockback method: Apply force and disable movement
    private void Knockback(Vector3 damageSourcePosition)
    {
        if (playerRigidbody != null && !isKnockedBack)
        {
            // Calculate the knockback direction (away from the damage source)
            Vector3 knockbackDirection = (transform.position - damageSourcePosition).normalized;

            // Apply force to the Rigidbody to simulate knockback
            playerRigidbody.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);

            // Temporarily disable movement by disabling the PlayerController script
            if (playerController != null)
            {
                playerController.enabled = false;
            }

            // Set knockback status to true
            isKnockedBack = true;

            // Re-enable movement after knockback duration
            Invoke("EndKnockback", knockbackDuration);
        }
    }

    // End knockback: re-enable movement
    private void EndKnockback()
    {
        if (playerController != null)
        {
            playerController.enabled = true;  // Re-enable player movement
        }
        isKnockedBack = false;
    }

    public bool IsPlayerAlive()
    {
        return isAlive;
    }

    public void Die()
    {
        if (!isAlive) return;

        Debug.Log("Player Died");
        isAlive = false;

        // Stop player movement and input
        if (playerController != null)
        {
            playerController.enabled = false;
        }

        if (allEnemies == null)
        {
            allEnemies = FindObjectsOfType<EnemyFollow>();
        }

        foreach (EnemyFollow enemy in allEnemies)
        {
            if (enemy != null)
            {
                enemy.StopChasingPlayer();
            }
        }

        if (dieTextObject != null)
            dieTextObject.SetActive(true);

        if (restartButton != null)
            restartButton.SetActive(true);
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    void UpdateLifeCounterUI()
    {
        if (lifeCounterText != null)
        {
            lifeCounterText.text = "Lives: " + currentHealth.ToString();
        }
    }
}
