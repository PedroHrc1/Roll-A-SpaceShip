using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;  // The enemy prefab to spawn
    public Transform[] spawnPoints;  // Array of spawn points
    public float spawnInterval = 5f;  // Time interval between spawns

    private float timer = 0f;
    private Transform playerTransform;  // Reference to the player transform

    void Start()
    {
        // Find the player in the scene and get its Transform component
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;  // Store the player's transform
        }
        else
        {
            Debug.LogError("Player object not found. Make sure the player has the 'Player' tag.");
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];

        // Instantiate a new enemy
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

        // Get the EnemyFollow component from the new enemy and set the player reference
        EnemyFollow enemyFollow = newEnemy.GetComponent<EnemyFollow>();
        if (enemyFollow != null && playerTransform != null)
        {
            enemyFollow.SetPlayer(playerTransform);  // Pass the player's transform to the enemy
        }
    }
}
