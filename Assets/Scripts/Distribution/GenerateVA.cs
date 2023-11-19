using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public static class GenerateVA
{

    public static void WriteToCSV<T>(List<T> dataList, string filePath)
    {
        StringBuilder sb = new StringBuilder();

        foreach (var data in dataList)
        {
            sb.AppendLine(data.ToString());
        }

        File.WriteAllText(filePath, sb.ToString());
    }

    // Scale Data
    #region Generate Scale Data
    public static void GenerateScaleData(int sampleSize, int samples)
    {
        BoxSpawner boxSpawner = new BoxSpawner();
        
        // File path
        string folderPath = Application.dataPath + "/VA_Outputs";
        string filePath = folderPath + "/scaleData"; ;

        // Check if the VA_Outputs directory exists
        if (!Directory.Exists(folderPath))
        {
            // If it doesn't exist, create it
            Directory.CreateDirectory(folderPath);
        }

        for (int s = 0; s < samples; s++)
        {
            List<Vector3> scaleData = new List<Vector3>();

            for (int i = 0; i < sampleSize; i++)
            {
                scaleData.Add(boxSpawner.GetRandomScale());
            }

            string newFilePath = filePath + s + ".csv";
            GenerateVA.WriteToCSV(scaleData, newFilePath);
        }
    }
    #endregion

    // Weight Data
    #region Generate Weight Data
    public static void GenerateWeightData(int sampleSize, int samples)
    {
        BoxSpawner boxSpawner = new BoxSpawner();

        // File path
        string folderPath = Application.dataPath + "/VA_Outputs";
        string filePath = folderPath + "/weightData"; ;

        // Check if the VA_Outputs directory exists
        if (!Directory.Exists(folderPath))
        {
            // If it doesn't exist, create it
            Directory.CreateDirectory(folderPath);
        }

        for (int s = 0; s < samples; s++)
        {
            List<float> weightData = new List<float>();

            for (int i = 0; i < sampleSize; i++)
            {
                weightData.Add(boxSpawner.GetRandomWeight());
            }

            string newFilePath = filePath + s + ".csv";
            GenerateVA.WriteToCSV(weightData, newFilePath);
        }
    }
    #endregion

    #region Generate Delivery Type Data
    public static void GenerateDeliveryTypeData(int sampleSize, int samples)
    {
        BoxSpawner boxSpawner = new BoxSpawner();

        // File path
        string folderPath = Application.dataPath + "/VA_Outputs";
        string filePath = folderPath + "/deliveryTypeData"; ;

        // Check if the VA_Outputs directory exists
        if (!Directory.Exists(folderPath))
        {
            // If it doesn't exist, create it
            Directory.CreateDirectory(folderPath);
        }

        for (int s = 0; s < samples; s++)
        {
            List<float> deliveryTypeData = new List<float>();
         
            for (int i = 0; i < sampleSize; i++)
            {
                deliveryTypeData.Add(boxSpawner.GetRandomDeliveryTypeValue());
            }

            string newFilePath = filePath + s + ".csv";
            GenerateVA.WriteToCSV(deliveryTypeData, newFilePath);
        }

    }
    #endregion

    #region Generate Box Type Data
    public static void GenerateBoxTypeData(int sampleSize, int samples)
    {
        BoxSpawner boxSpawner = new BoxSpawner();

        // File path
        string folderPath = Application.dataPath + "/VA_Outputs";
        string filePath = folderPath + "/boxTypeData"; ;

        // Check if the VA_Outputs directory exists
        if (!Directory.Exists(folderPath))
        {
            // If it doesn't exist, create it
            Directory.CreateDirectory(folderPath);
        }

        for (int s = 0; s < samples; s++)
        {
            List<float> boxTypeData = new List<float>();

            for (int i = 0; i < sampleSize; i++)
            {
                boxTypeData.Add(boxSpawner.GetRandomBoxTypeValue());
            }

            string newFilePath = filePath + s + ".csv";
            GenerateVA.WriteToCSV(boxTypeData, newFilePath);
        }

    }
    #endregion



}