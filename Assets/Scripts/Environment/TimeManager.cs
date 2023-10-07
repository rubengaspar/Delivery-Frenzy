using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [Header("UI Settings")]
    [SerializeField] private TMP_Text timeText;

    [Header("Year Rotation Settings")]
    [SerializeField] public float realMinutesPerDay = 0.5f;

    [Header("Box Spawner Settings")]
    [SerializeField] private GameObject boxSpawnManager;
    [SerializeField] private float initialSpawnRate = 5f;

    private int daysPerYear = 360;
    private float dayProgression;
    private int currentDay = 0;
    private bool newDayStarted = false;
    private GameManager gameManager;

    public void Initialize(GameManager gameManager)
    {
        this.gameManager = gameManager;
        boxSpawnManager.GetComponent<BoxSpawner>().SetSpawnRate(initialSpawnRate);
        StartCoroutine(YearRotation());
    }

    public void CustomUpdate()
    {
        
    }

    private IEnumerator YearRotation()
    {
        float dayDurationInSeconds = realMinutesPerDay * 60f;
        while (true)
        {
            dayProgression += Time.deltaTime / dayDurationInSeconds;

            UpdateTimeUI();

            // If a day has passed: reset the day, update month and update spawn rate
            if (dayProgression >= 1)
            {
                dayProgression = 0;
                currentDay = (currentDay + 1) % daysPerYear;

                UpdateSpawnRate();

                newDayStarted = true;
            }

            yield return null;
        }
    }

    public bool HasNewDayStarted()
    {
        if (newDayStarted)
        {
            newDayStarted = false;
            return true;
        }
        return false;
    }

    public int GetCurrentDay()
    {
        return currentDay;
    }

    public int GetCurrentMonth()
    {
        return (currentDay % daysPerYear) / 30 + 1;
    }    
    public int GetCurrentYear()
    {
        return currentDay / daysPerYear + 1;
    }

    private void UpdateSpawnRate()
    {
        float newSpawnRate = initialSpawnRate + dayProgression;
        boxSpawnManager.GetComponent<BoxSpawner>().SetSpawnRate(newSpawnRate);
    }

    private void UpdateTimeUI()
    {
        int year = currentDay / daysPerYear + 1;
        int month = (currentDay % daysPerYear) / 30 + 1;
        int day = (currentDay % daysPerYear) % 30 + 1;
        float timeOfDay = dayProgression * 24;

        int wholeHours = (int)Math.Floor(timeOfDay);

        timeText.text = $"Day {day}\n Month: {month} \n Year:{year} \n{wholeHours}h";

    }

    private void UpdateLighting(float timeOfDay)
    { 

        // Idea for later:
        // Rotate light during the day around the world center
        // When light is out, player needs to switch on the lights in the warehouse

    }

}
