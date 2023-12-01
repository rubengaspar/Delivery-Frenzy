using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class DataGeneratorEditor : Editor
{

    // Scale Data
    #region Data Generation / Generate Scale Data
    [MenuItem("Data Generation/Generate Scale Data")]
    private static void GenerateScaleData()
    {
        GenerateVA.GenerateScaleData(100000, 5);
        Debug.Log("Scale data generation complete.");
    }
    #endregion

    // Weight
    #region Data Generation / Generate Weight Data
    [MenuItem("Data Generation/Generate Weight Data")]
    private static void GenerateWeightData()
    {
        GenerateVA.GenerateWeightData(100000, 5);
        Debug.Log("Weight data generation complete.");
    }
    #endregion

    // Spawn Delay & Spawn Rate
    #region Data Generation / Generate Spawn Delay Data
    [MenuItem("Data Generation/Generate Spawn Delay Data")]
    private static void GenerateSpawnDelayData()
    {
        GenerateVA.GenerateSpawnDelayData(100000, 5);
        Debug.Log("Spawn Delay data generation complete.");
    }
    #endregion

    #region Data Generation / Generate Spawn Rate Data
    [MenuItem("Data Generation/Generate Spawn Rate Data")]
    private static void GenerateSpawnRateData()
    {
        GenerateVA.GenerateSpawnRateData(100000, 5);
        Debug.Log("Spawn Rate data generation complete.");
    }
    #endregion

    // Delivery Type
    #region Data Generation / Generate Delivery Type Data
    [MenuItem("Data Generation/Generate Delivery Type Data")]
    private static void GenerateDeliveryTypeData()
    {
        GenerateVA.GenerateDeliveryTypeData(100000, 5);
        Debug.Log("Delivery Type data generation complete.");
    }
    #endregion

    // Box Type
    #region Data Generation / Generate Box Type Data
    [MenuItem("Data Generation/Generate Box Type Data")]
    private static void GenerateBoxTypeData()
    {
        GenerateVA.GenerateBoxTypeData(100000, 5);
        Debug.Log("Box Type data generation complete.");
    }
    #endregion
}
