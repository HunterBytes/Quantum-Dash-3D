using System.Collections;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    // Reference to the obstacle prefab
    public GameObject obstaclePrefab;

    // Reference to the player (moving cube)
    public Transform player;

    // The distance ahead of the player to generate obstacles
    public float spawnDistanceAhead = 20f;

    // Interval for increasing the spawn distance
    public float spawnInterval = 5f;

    // Range for random X and Y positions of obstacle spawning
    public float minX = -7.5f; // Half of width 15
    public float maxX = 7.5f;
    public float minY = 0f; // Adjust according to ground level
    public float maxY = 5f; // Adjust the max Y position

    // The Z position of the last spawned obstacle
    private float nextSpawnZ;

    // Start is called before the first frame update
    void Start()
    {
        // Set the initial spawn position
        nextSpawnZ = player.position.z + spawnDistanceAhead;

        // Start generating obstacles
        StartCoroutine(SpawnObstacles());
    }

    // Coroutine for spawning obstacles
    IEnumerator SpawnObstacles()
    {
        while (true) // Continuously generate obstacles
        {
            // Wait until the player reaches the point to spawn the next obstacle
            if (player.position.z >= nextSpawnZ - spawnDistanceAhead)
            {
                // Generate random X and Y positions within the defined ranges
                float randomX = Random.Range(minX, maxX);
                float randomY = Random.Range(minY, maxY);

                // Define the spawn position based on the random X, Y, and the next spawn Z
                Vector3 spawnPosition = new Vector3(randomX, randomY, nextSpawnZ);

                // Instantiate the obstacle at the spawn position
                Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);

                // Update the next spawn position by adding the interval
                nextSpawnZ += spawnInterval;
            }

            // Yield for a short time before the next loop iteration
            yield return new WaitForSeconds(0.1f); // Adjust if necessary for performance
        }
    }
}