using System.IO;
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
        private void Start()
        {
            string FileName = @"E:\workspace\Git\RNDProject\RNDProject\R&D Project\Assets\Resources\Test.xlsx";
            using (var stream = File.Open(FileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet();

                    for (int i = 0; i < result.Tables.Count; i++)
                    {
                        for (int j = 0; j < result.Tables[i].Rows.Count; j++)
                        {
                            string data1 = result.Tables[i].Rows[j][0].ToString();
                            string data2 = result.Tables[i].Rows[j][1].ToString();
                            Debug.Log($"{data1} : {data2}");
                        }
                    }
                    
                }
            }
        }
    }
}