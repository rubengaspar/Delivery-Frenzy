using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoxSpawner : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] private GameObject[] boxPrefabs;
    [SerializeField] private GameObject[] presentPrefabs;
    [SerializeField] private GameObject bombBoxPrefab;

    [Header("Box Type Probability")]
    [SerializeField, Range(0, 1)] private float boxProbability = 0.92f;
    [SerializeField, Range(0, 1)] private float presentProbability = 0.06f;
    [SerializeField, Range(0, 1)] private float bombProbability = 0.02f;

    [Header("Spawn Settings")]
    [SerializeField] private GameObject spawnCenter;
    [SerializeField] private float spawnRadius = 1f;
    [SerializeField] private float spawnRate = 5f; // per second
    [SerializeField] private float spawnLambdaWeight = 2f; // for exponential implementation of spawn
    [SerializeField] private float maxLambdaWeight = 10f; // Max spawn rate corresponds to lambdaWeight of 10
    [SerializeField] private float lambdaDecreaseRate = 0.1333f; 


    [Header("Exponential Distribution Settings")]
    [SerializeField] private float lambdaWeight = 1.0f;


    [Header("Box Limiters")]
    [SerializeField, Range(0, 50)] private int minRate = 5;
    [SerializeField, Range(0, 50)] private int maxRate = 16;

    [SerializeField, Range(1, 3)] private float minWidth = 1f;
    [SerializeField, Range(1, 3)] private float minHeight = 1f;
    [SerializeField, Range(1, 3)] private float minLength = 1f;
    [SerializeField, Range(3, 5)] private float maxWidth = 5f;
    [SerializeField, Range(3, 5)] private float maxHeight = 3f;
    [SerializeField, Range(3, 5)] private float maxLength = 7f;

    [SerializeField, Range(0, 1)] private float minRotation = 0.5f;
    [SerializeField, Range(0, 1)] private float maxRotation = 0.5f;

    [SerializeField, Range(1, 2)] private float minWeight = 1f;
    [SerializeField, Range(2, 5)] private float maxWeight = 5f;

    private float nextSpawnTime;
    private float elapsedTime;
    private GameManager gameManager;
    
    public void Initialize(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    private void Start()
    {
        nextSpawnTime = Time.time;
    }

    public void CustomUpdate()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnBox();
            float delay = 1 / spawnRate;
            nextSpawnTime = Time.time + delay;
        }

        //if (Time.time >= nextSpawnTime)
        //{
        //    SpawnBox();
        //    // Set the next spawn time
        //    nextSpawnTime = Time.time + GetRandomSpawnDelay();
        //}
    }

    void SpawnBox()
    {
        // Random selected box
        GameObject selectedPrefab = GetRandomBoxType();

        // Random spawn location
        Vector3 spawnLocation = spawnCenter.transform.position + GetRandomLocation();

        // Random spawn rotation
        Quaternion randomRotation = Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));

        // Instantiate the prefab at the spawn location
        GameObject spawnedBox = Instantiate(selectedPrefab, spawnLocation, randomRotation);

        spawnedBox.transform.localScale = GetRandomScale();

        // Set spawned box's delivery type
        BoxObject boxObject = spawnedBox.GetComponent<BoxObject>();
        boxObject.deliveryType = GetRandomDeliveryType();

        Rigidbody rb = spawnedBox.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Assuming the box should fall downwards, influenced by gravity
            rb.useGravity = true;
            rb.isKinematic = false;
            rb.transform.SetParent(this.transform);

            // Apply rotation vector
            rb.AddTorque(GetRandomRotation(), ForceMode.Impulse);

            // Apply random Weight
            rb.mass = GetRandomWeight();
        }
    }

    // Random Rotation
    #region Random Rotation
    public Vector3 GetRandomRotation()
    {
        // Random rotation vector
        return new Vector3(Random.Range(-minRotation, maxRotation), Random.Range(-minRotation, maxRotation), Random.Range(-minRotation, maxRotation));
    }
    #endregion

    // Random Location
    #region Spawn Location
    public Vector3 GetRandomLocation()
    {
        // Random spawn location
        return new Vector3(Random.Range(-spawnRadius, spawnRadius), Random.Range(-spawnRadius, spawnRadius), Random.Range(-spawnRadius, spawnRadius));
    }
    #endregion

    // Random Scale
    #region Random Scale
    // Random scale (W, H, L)
    public Vector3 GetRandomScale()
    {
        float meanWidth = (minWidth + maxWidth) / 2;
        float meanHeight = (minHeight + maxHeight) / 2;
        float meanLength = (minLength + maxLength) / 2;

        // Random scale for each dimension
        float randomWidth = Distribution.Normal(meanWidth, minWidth, maxWidth);
        float randomHeight = Distribution.Normal(meanHeight, minHeight, maxHeight);
        float randomLength = Distribution.Normal(meanLength, minLength, maxLength);

        return new Vector3(randomWidth, randomHeight, randomLength);
    }
    #endregion

    // Random Weight
    #region Random Weight
    public float GetRandomWeight()
    {
        return Mathf.Clamp(Distribution.Exponential(lambdaWeight), minWeight, maxWeight);
    }

    #endregion

    // Random Spawn Delay
    #region Random Spawn Delay
    //public void SetSpawnRate(float newSpawnRate)
    //{
    //    spawnRate = newSpawnRate;
    //}

    public float GetRandomSpawnDelay()
    {
        // Update elapsedTime
        elapsedTime += Time.deltaTime;

        // Adjust lambdaWeight over time, not allowing it to exceed maxLambdaWeight
        spawnLambdaWeight = Mathf.Min(maxLambdaWeight, spawnLambdaWeight + lambdaDecreaseRate * elapsedTime);

        // result of the distribution
        return Distribution.Exponential(spawnLambdaWeight);
    }

    public int GetRandomSpawnRate()
    {
        return Distribution.Uniform(minRate, maxRate);
    }




    #endregion

    // Random Delivery Type
    #region Random Delivery Type
    public BoxObject.DeliveryType GetRandomDeliveryType()
    {
        float value = GetRandomDeliveryTypeValue(); // lambda

        // Updated thresholds based on calculations
        float standardThreshold = 1.20f;
        float twoDayThreshold = 2.30f;
        float overnightThreshold = 3.51f;

        if (value < standardThreshold)
        {
            return BoxObject.DeliveryType.Standard;
        }
        else if (value < twoDayThreshold)
        {
            return BoxObject.DeliveryType.TwoDay;
        }
        else if (value < overnightThreshold)
        {
            return BoxObject.DeliveryType.Overnight;
        }
        else
        {
            return BoxObject.DeliveryType.SameDay;
        }
    }

    public float GetRandomDeliveryTypeValue()
    {
        return Distribution.Exponential(1.0f);
    }

    #endregion

    // Random Box Type
    #region Random Box Type
    public GameObject GetRandomBoxType()
    {
        float value = Distribution.Uniform(0f, 1f);

        // Interval = [ 0, box probability]
        if (value <= boxProbability) 
        {
            return boxPrefabs[Random.Range(0, boxPrefabs.Length)];
        }
        // Interval = ] box probability, present probability ]
        else if (value <= (boxProbability + presentProbability)) 
        {
            return presentPrefabs[Random.Range(0, presentPrefabs.Length)];
        }
        // Interval = ] present robability, 100 ]
        return bombBoxPrefab;
    }

    public float GetRandomBoxTypeValue()
    {
        return Distribution.Uniform(0f, 1f);
    }

    #endregion
}