using System.Drawing;
using System.IO;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] private GameObject[] boxPrefabs;

    [Header("Spawn Parameters")]
    [SerializeField] private GameObject spawnCenter;
    [SerializeField] private float spawnRadius = 1f;
    [SerializeField] private float spawnRate = 5f; // Boxes per second
    [SerializeField] private Vector3 minScale;
    [SerializeField] private Vector3 maxScale;

    private float nextSpawnTime;
    private GameManager gameManager;

    public void Initialize(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public void CustomUpdate()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnBox();
            nextSpawnTime = Time.time + 1f / spawnRate;
        }
    }

    void SpawnBox()
    {
        // Random selected box
        GameObject selectedPrefab = boxPrefabs[Random.Range(0, boxPrefabs.Length)];

        // Random spawn location
        Vector3 randomPosition = new Vector3(Random.Range(-spawnRadius, spawnRadius),
                                            Random.Range(-spawnRadius, spawnRadius),
                                            Random.Range(-spawnRadius, spawnRadius));

        Vector3 spawnLocation = spawnCenter.transform.position + randomPosition;

        // Random spawn rotation
        Quaternion randomRotation = Quaternion.Euler(Random.Range(0f, 360f),
                                                    Random.Range(0f, 360f),
                                                    Random.Range(0f, 360f));

        // Instantiate the prefab at the spawn location
        GameObject spawnedBox = Instantiate(selectedPrefab, spawnLocation, randomRotation);

        // Random scale
        Vector3 randomScale = new Vector3(Random.Range(minScale.x, maxScale.x),
                                        Random.Range(minScale.y, maxScale.y),
                                        Random.Range(minScale.z, maxScale.z));

        spawnedBox.transform.localScale = randomScale;


        Rigidbody rb = spawnedBox.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Assuming the box should fall downwards, influenced by gravity
            rb.useGravity = true;
            rb.isKinematic = false;
            rb.transform.SetParent(this.transform);

            // Random rotation vector
            Vector3 randomImpulse = new Vector3(
                UnityEngine.Random.Range(-1, 1),
                UnityEngine.Random.Range(-1, 1),
                UnityEngine.Random.Range(-1, 1)
            );

            // Apply rotation vector
            rb.AddTorque(randomImpulse, ForceMode.Impulse);

            // Apply random Mass
            rb.mass = Random.Range(1f, 5f);
        }
    }

    // Add randomization complexity functions

    public void SetSpawnRate(float newSpawnRate)
    {
        spawnRate = newSpawnRate;
    }
}