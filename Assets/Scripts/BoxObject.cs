using System;
using UnityEngine;

public class BoxObject : MonoBehaviour
{
    public enum ColorType
    {
        Green,
        Purple,
        Yellow,
        Bomb
    }
    
    public enum DeliveryType
    {
        Standard,
        TwoDay,
        Overnight,
        SameDay,
    }

    [Header("Settings")]
    [SerializeField] public Color color;
    [SerializeField] public ColorType colorType;
    [SerializeField] private int currentPoints = 50; // starting points
    [SerializeField] private int timeBonusMultiplier = 5;
    [SerializeField] public DeliveryType deliveryType;

    [Header("Variable Observation")]
    [SerializeField] private float spawnTime;
    [SerializeField] public float deliveryDeadline = 0;
    [SerializeField] private float deliveryMinutesRemaining;


    private TimeManager timeManager;

    void Start()
    {
        timeManager = TimeManager.Instance;

        if (timeManager != null)
        {
            spawnTime = timeManager.GetCurrentGameTimeInMinutes();
        }
    }

    public float GetCurrentPoints()
    {
        

        float maxDimBonus = 15f; // max width + max height + max length
        float maxTimeBonus = deliveryDeadline - spawnTime;
        float maxWeightBonus = 10f; // max weight

        Rigidbody rb = GetComponent<Rigidbody>();
        
        if (rb == null)
        {
            return 0;
        }

        float weight = rb.mass;
        float dimBonus = rb.transform.localScale.x + rb.transform.localScale.y + rb.transform.localScale.z;
        float timeBonus = CalculateTimeBonus();

        // Normalizing each component
        float normalizedWeightBonus = Mathf.Min(10, (weight / maxWeightBonus) * 10);
        float normalizedDimBonus = Mathf.Min(10, (dimBonus / maxDimBonus * 10));
        float normalizedTimeBonus = Mathf.Min(10, (timeBonus / maxTimeBonus) * 10);

        currentPoints = Mathf.RoundToInt(normalizedWeightBonus + normalizedDimBonus + normalizedTimeBonus);

        return currentPoints;
    }


    private float CalculateTimeBonus()
    {       

        deliveryMinutesRemaining = GetRemainingDeliveryTime();
        
        float bonus = 0;

        bonus = deliveryMinutesRemaining > 0 ? deliveryMinutesRemaining : 0;

        return bonus;
    }

    public float GetRemainingDeliveryTime()
    {
        timeManager = TimeManager.Instance;

        if (timeManager == null)
        {
            return 0;
        }

        deliveryMinutesRemaining = deliveryDeadline - (timeManager.GetCurrentGameTimeInMinutes() - spawnTime);

        return deliveryMinutesRemaining > 0 ? deliveryMinutesRemaining : 0;
    }


    public float SetDeliveryDeadline() 
    {

        timeManager = TimeManager.Instance;

        switch(this.deliveryType)
        {
            case (DeliveryType.Standard):
                deliveryDeadline = timeManager.minutesInADay * 7f;
                return deliveryDeadline;
            case (DeliveryType.TwoDay):
                deliveryDeadline = timeManager.minutesInADay * 2f;
                return deliveryDeadline;
            case (DeliveryType.Overnight):
                deliveryDeadline = timeManager.minutesInADay * 1f;
                return deliveryDeadline;
            case (DeliveryType.SameDay):
                deliveryDeadline = timeManager.minutesInADay * 0.5f;
                return deliveryDeadline;
        }

        return deliveryDeadline;

    }
    public void SetColorType(BoxObject.ColorType colorType)
    {
        this.colorType = colorType;
    }

}
