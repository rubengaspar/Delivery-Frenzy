using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    [Header("Weather Settings")]
    [SerializeField] private GameObject rainGameObject;
    [SerializeField, Range(0, 30)] private int[] monthlyRainDays = new int[12];

    [Header("Rain Settings")]
    [SerializeField, Range(0, 1)] private float rainBucket = 0f;
    [SerializeField, Range(0, 1)] private float rainThreshold = 1f;
    [SerializeField, Range(0, 1)] private float rainReductionAfterRainyDay = 0.5f;

    private GameManager gameManager;

    public void Initialize(GameManager gameManager)
    {
        this.gameManager = gameManager;
        rainBucket = 0.5f;  // Initialize to some starting level
        DecideWeatherForToday();
    }

    public void CustomUpdate()
    {
        if (gameManager.timeManager.HasNewDayStarted())
        {
            DecideWeatherForToday();
            UpdateWeather();
        }
    }

    private void DecideWeatherForToday()
    {
        int currentMonth = gameManager.timeManager.GetCurrentMonth();
        float monthFactor = monthlyRainDays[currentMonth - 1] / 30f;

        // Add water to bucket based on month and keep between [0,1]
        rainBucket += Random.Range(-0.2f, 0.2f) + monthFactor;
        rainBucket = Mathf.Clamp(rainBucket, 0f, 1f);
    }

    private void UpdateWeather()
    {
        if (rainBucket >= rainThreshold)
        {
            
            rainGameObject.SetActive(true);
            rainBucket -= rainReductionAfterRainyDay;  // Reduce rain probability after rainny day
        }
        else
        {
            rainGameObject.SetActive(false);
        }
    }
}
