using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class DataGeneratorEditor : Editor
{

    // Data Generation / Generate Scale Data
    [MenuItem("Data Generation/Generate Scale Data")]
    private static void GenerateScaleData()
    {
        GenerateVA.GenerateScaleData(100000, 5);
        Debug.Log("Scale data generation complete.");
    }

    // Data Generation / Generate Weight Data
    [MenuItem("Data Generation/Generate Weight Data")]
    private static void GenerateWeightData()
    {
        GenerateVA.GenerateWeightData(100000, 5);
        Debug.Log("Weight data generation complete.");
    }

    // Data Generation / Generate Spawn Rate Data
    //[MenuItem("Data Generation/Generate Spawn Rate Data")]
    //private static void GenerateSpawnRateData()
    //{
    //    GenerateVA.GenerateSpawnRateData(100000, 1);
    //    Debug.Log("Spawn Rate data generation complete.");
    //}

    // Data Generation / Generate Box Type Data
    [MenuItem("Data Generation/Generate Box Type Data")]
    private static void GenerateBoxTypeData()
    {
        GenerateVA.GenerateBoxTypeData(100000, 5);
        Debug.Log("Box Type data generation complete.");
    }

    // Data Generation / Generate Delivery Type Data
    [MenuItem("Data Generation/Generate Delivery Type Data")]
    private static void GenerateDeliveryTypeData()
    {
        GenerateVA.GenerateDeliveryTypeData(100000, 5);
        Debug.Log("Delivery Type data generation complete.");
    }
}
