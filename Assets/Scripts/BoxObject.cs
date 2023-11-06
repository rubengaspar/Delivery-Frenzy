using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoxObject : MonoBehaviour
{

    public enum DeliveryStatus
    {
        NotDelivered,
        Delivered,
        Destroyed,
        Lost,
        Stolen
    }

    public enum DeliveryType
    {
        Standard,
        TwoDay,
        Overnight,
        SameDay
    }

    [Header("Settings")]
    [SerializeField] public Color color;
    [SerializeField] private int currentPoints = 50; // starting points
    [SerializeField] private int timeBonusMultiplier = 5;
    [SerializeField] public float maxDeliveryTime;
    [SerializeField] public int destroyedPointDeduction = 50;
    [SerializeField] public int lostPointDeduction = 20;
    [SerializeField] public int stolenPointDeduction = 10;

    [Header("Variable Observation")]
    [SerializeField] public DeliveryStatus deliveryStatus = DeliveryStatus.NotDelivered;
    [SerializeField] private float spawnTime;
    [SerializeField] private float timeToDeliver;
    [SerializeField] public DeliveryType deliveryType; 
    [SerializeField, Range(0, 1)] private float waterDamage = 0;
    [SerializeField, Range(0, 1)] private float fireDamage = 0;

    

    void Start()
    {
        // Record the spawn time
        spawnTime = Time.time;
    }

    public float GetCurrentPoints()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            return 0;
        }

        float weight = rb.mass;
        float size = rb.transform.localScale.x * rb.transform.localScale.y * rb.transform.localScale.z;
        float timeBonus = 0;
        float damageFactor = 1 - Mathf.Clamp((waterDamage + fireDamage), 0, 1);

        switch (deliveryStatus)
        {
            case DeliveryStatus.Delivered:
                timeBonus = (maxDeliveryTime - timeToDeliver) * timeBonusMultiplier;
                currentPoints = Mathf.RoundToInt((weight + size + timeBonus) * damageFactor);
                break;

            case DeliveryStatus.NotDelivered:
                timeBonus = (maxDeliveryTime - Time.time) * timeBonusMultiplier;
                currentPoints = Mathf.RoundToInt((weight + size + timeBonus) * damageFactor);
                break;

            case DeliveryStatus.Destroyed:
                currentPoints = destroyedPointDeduction;
                break;

            case DeliveryStatus.Lost:
                currentPoints = lostPointDeduction;
                break;

            case DeliveryStatus.Stolen:
                currentPoints = stolenPointDeduction;
                break;

            default:
                break;
        }

        return currentPoints;

    }

    public void Burn(float amount)
    {
        Mathf.Clamp(fireDamage += amount, 0, 1);
    }

    public void WaterDamage(float amount)
    {
        Mathf.Clamp(waterDamage += amount, 0, 1);
    }

    public void Deliver()
    {
        timeToDeliver = Time.time;
        deliveryStatus = DeliveryStatus.Delivered;
    }

    public void Destroy()
    {
       deliveryStatus = DeliveryStatus.Destroyed;
    }

    public void Steal()
    {
        deliveryStatus = DeliveryStatus.Stolen;
    }

    public void Loose()
    {
        deliveryStatus = DeliveryStatus.Lost;
    }



}
