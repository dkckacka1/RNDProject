
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Newtonsoft.Json;
using ExcelDataReader;

namespace ThirdParty.ExcelReader
{
    public class ExcelReader : MonoBehaviour
    {
        string xlsxPath = @"C:\Users\G\Documents\KHW\RNDProject\R&D Project\Assets\ThirdParty\ExcelReader\data.xlsx";
        string jsonPath = @"C:\Users\G\Documents\KHW\RNDProject\R&D Project\Assets\ThirdParty\ExcelReader\Test.json";

        private void Start()
        {
            using (var stream = File.Open(xlsxPath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    using(var writer = new JsonTextWriter(File.CreateText(jsonPath)))
                    {
                        writer.Formatting = Formatting.Indented;
                        writer.WriteStartArray();
                        reader.Read();
                        do
                        {
                            while (reader.Read())
                            {
                                writer.WriteStartObject();

                                writer.WritePropertyName("id");
                                writer.WriteValue(int.Parse(reader.GetValue(0).ToString()));

                                writer.WritePropertyName("name");
                                writer.WriteValue(reader.GetString(1));

                                writer.WriteEndObject();
                            }
                        }
                        while (reader.NextResult());

                        writer.WriteEndArray();

                    }
                }
            }
        }
    }
}