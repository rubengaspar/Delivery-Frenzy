using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoxSpawner : MonoBehaviour
{
    public static BoxSpawner Instance { get; private set; }

    [Header("Prefab")]
    [SerializeField] private GameObject[] boxPrefabs;
    [SerializeField] private GameObject[] giftBoxPrefabs;
    [SerializeField] private GameObject bombBoxPrefab;

    [Header("Box Type Probability")]
    [SerializeField, Range(0, 1)] private float boxProbability = 0.92f;
    [SerializeField, Range(0, 1)] private float giftBoxProbability = 0.06f;
    [SerializeField, Range(0, 1)] private float bombProbability = 0.02f;

    [Header("Spawn Settings")]
    [SerializeField] private GameObject spawnCenter;
    [SerializeField] private float spawnRadius = 1f;
    [SerializeField] private float spawnRate = 2f; // per second
    [SerializeField] private float spawnRateUpdateFrequency = 10f;
    [SerializeField] private float spawnLambdaWeight = 2f; // for exponential implementation of spawn
    [SerializeField] private float maxLambdaWeight = 10f; // Max spawn rate corresponds to lambdaWeight of 10
    [SerializeField] private float lambdaDecreaseRate = 0.1333f; 


    [Header("Exponential Distribution Settings")]
    [SerializeField] private float lambdaWeight = 1.0f;

    [Header("Box Limiters")]
    [SerializeField, Range(0, 50)] public int minRate = 2;
    [SerializeField, Range(0, 50)] public int maxRate = 5;

    [SerializeField, Range(1, 3)] public float minWidth = 1f;
    [SerializeField, Range(1, 3)] public float minHeight = 1f;
    [SerializeField, Range(1, 3)] public float minLength = 1f;
    [SerializeField, Range(3, 5)] public float maxWidth = 5f;
    [SerializeField, Range(3, 5)] public float maxHeight = 3f;
    [SerializeField, Range(3, 5)] public float maxLength = 7f;

    [SerializeField, Range(0, 1)] public float minRotation = 0.5f;
    [SerializeField, Range(0, 1)] public float maxRotation = 0.5f;

    [SerializeField, Range(1, 2)] public float minWeight = 0.5f;
    [SerializeField, Range(2, 10)] public float maxWeight = 100f;

    [SerializeField] private TMP_Text gameInfo;

    private float nextSpawnTime;
    private float elapsedTime;
    private GameManager gameManager;

    // Show on Screen Variables
    private int count_BT_Standard = 0;
    private int count_BT_Present = 0;
    private int count_BT_Bomb = 0;

    private int count_DT_Standard = 0;
    private int count_DT_TwoDay = 0;
    private int count_DT_Overnight = 0;
    private int count_DT_SameDay = 0;

    private float lastSpawnRateUpdate;

    public void Initialize(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    private void Start()
    {
        nextSpawnTime = Time.time;
        lastSpawnRateUpdate = Time.time;
    }

    public void CustomUpdate()
    {
        if (Time.time >= lastSpawnRateUpdate + spawnRateUpdateFrequency)
        {
            spawnRate = GetRandomSpawnRate();
            lastSpawnRateUpdate = Time.time;
        }

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

        UpdateDisplay();


    }

    void UpdateDisplay()
    {
        gameInfo.text = $"Spawn Rate: {spawnRate:F0} per second\n" +
                        $"\n" + 
                        $"Box Type Count\n" +
                        $"Standard: {count_BT_Standard:F0}\n" +
                        $"Present: {count_BT_Present:F0}\n" +
                        $"Bomb: {count_BT_Bomb:F0}\n" +
                        $"\n" +
                        $"Delivery Type Count\n" +
                        $"Standard: {count_DT_Standard:F0}\n" +
                        $"Two Days: {count_DT_TwoDay:F0}\n" +
                        $"Overnight: {count_DT_Overnight:F0}\n" +
                        $"Same Day {count_DT_SameDay:F0}";
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
        boxObject.SetDeliveryDeadline();

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
        return Mathf.Clamp(Distribution.Exponential(lambdaWeight, minWeight, 2f),minWeight, maxWeight);
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
        float value = GetRandomDeliveryTypeValue();

        // Updated thresholds based on calculations
        float standardThreshold = 1.20f;
        float twoDayThreshold = 2.30f;
        float overnightThreshold = 3.51f;

        if (value < standardThreshold)
        {
            count_DT_Standard++;
            return BoxObject.DeliveryType.Standard;
        }
        else if (value < twoDayThreshold)
        {
            count_DT_TwoDay++;
            return BoxObject.DeliveryType.TwoDay;
        }
        else if (value < overnightThreshold)
        {
            count_DT_Overnight++;
            return BoxObject.DeliveryType.Overnight;
        }
        else
        {
            count_DT_SameDay++;
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
            count_BT_Standard++;
            return boxPrefabs[Random.Range(0, boxPrefabs.Length)];
        }
        // Interval = ] box probability, present probability ]
        else if (value <= (boxProbability + giftBoxProbability)) 
        {
            count_BT_Present++;
            return giftBoxPrefabs[Random.Range(0, giftBoxPrefabs.Length)];
        }
        // Interval = ] gift robability, 100 ]
        count_BT_Bomb++;
        return bombBoxPrefab;
    }

    public float GetRandomBoxTypeValue()
    {
        return Distribution.Uniform(0f, 1f);
    }

    #endregion
}