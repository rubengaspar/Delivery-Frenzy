using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace Tests
{
    public static class Common
    {
        public static void WriteToCsvVAOutput<T>(List<T> dataList, string folderName, int sampleId)
        {
            string folderPath = Path.Join(Application.dataPath, "VA_Outputs", folderName);
            string fileName = "data" + sampleId + ".csv";
            WriteToCsv(dataList, folderPath, fileName);
        }

        private static void CreateFolderIfNotExists(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
        }

        public static void WriteToCsv<T>(List<T> dataList, string folderPath, string fileName)
        {
            CreateFolderIfNotExists(folderPath);

            StringBuilder sb = new StringBuilder();

            foreach (var data in dataList)
            {
                sb.AppendLine(data.ToString());
            }

            File.WriteAllText(Path.Join(folderPath, fileName), sb.ToString());
        }
    }
}