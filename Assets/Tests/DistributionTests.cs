using System.Collections.Generic;
using UnityEngine;

namespace Tests
{
    public static class DistributionTests
    {
        #region Generate Scale Data

        public static void GenerateScaleData(int sampleSize, int samples)
        {
            BoxSpawner boxSpawner = new BoxSpawner();

            for (int s = 0; s < samples; s++)
            {
                List<Vector3> scaleData = new List<Vector3>();

                for (int i = 0; i < sampleSize; i++)
                {
                    scaleData.Add(boxSpawner.GetRandomScale());
                }

                Common.WriteToCsvVAOutput(scaleData, "Scale", s);
            }
        }

        #endregion

        #region Generate Weight Data

        public static void GenerateWeightData(int sampleSize, int samples)
        {
            BoxSpawner boxSpawner = new BoxSpawner();

            for (int s = 0; s < samples; s++)
            {
                List<float> weightData = new List<float>();

                for (int i = 0; i < sampleSize; i++)
                {
                    weightData.Add(boxSpawner.GetRandomWeight());
                }

                Common.WriteToCsvVAOutput(weightData, "Weight", s);
            }
        }

        #endregion

        #region Generate Spawn Delay Data

        public static void GenerateSpawnDelayData(int sampleSize, int samples)
        {
            BoxSpawner boxSpawner = new BoxSpawner();

            for (int s = 0; s < samples; s++)
            {
                List<float> spawnDelayData = new List<float>();

                for (int i = 0; i < sampleSize; i++)
                {
                    spawnDelayData.Add(boxSpawner.GetRandomSpawnDelay());
                }

                Common.WriteToCsvVAOutput(spawnDelayData, "SpawnDelay", s);
            }
        }

        #endregion

        #region Generate Delivery Type Data

        public static void GenerateDeliveryTypeData(int sampleSize, int samples)
        {
            BoxSpawner boxSpawner = new BoxSpawner();

            for (int s = 0; s < samples; s++)
            {
                List<float> deliveryTypeData = new List<float>();

                for (int i = 0; i < sampleSize; i++)
                {
                    deliveryTypeData.Add(boxSpawner.GetRandomDeliveryTypeValue());
                }

                Common.WriteToCsvVAOutput(deliveryTypeData, "DeliveryType", s);
            }
        }

        #endregion

        #region Generate Box Type Data

        public static void GenerateBoxTypeData(int sampleSize, int samples)
        {
            BoxSpawner boxSpawner = new BoxSpawner();

            for (int s = 0; s < samples; s++)
            {
                List<float> boxTypeData = new List<float>();

                for (int i = 0; i < sampleSize; i++)
                {
                    boxTypeData.Add(boxSpawner.GetRandomBoxTypeValue());
                }

                Common.WriteToCsvVAOutput(boxTypeData, "BoxType", s);
            }
        }

        #endregion
    }
}