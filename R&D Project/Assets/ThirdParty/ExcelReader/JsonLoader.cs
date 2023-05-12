using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace ThirdParty.ExcelReader
{
    public class JsonLoader : MonoBehaviour
    {
        string jsonPath = @"C:\Users\G\Documents\KHW\RNDProject\R&D Project\Assets\ThirdParty\ExcelReader\Test.json";

        private void Start()
        {
            var text = File.OpenText(jsonPath);
            string json = text.ReadToEnd();
            Debug.Log(json);
            var testDatas = JsonConvert.DeserializeObject<TestData[]>(json);
            foreach (var data in testDatas)
            {
                Debug.Log(data);
            }
        }
    }
}