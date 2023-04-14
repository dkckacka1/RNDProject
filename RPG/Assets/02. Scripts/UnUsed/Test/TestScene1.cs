using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using RPG.Character.Status;
using RPG.Character.Equipment;
using RPG.Battle.Core;
using RPG.Main.UI;
using RPG.Battle.UI;
using RPG.Battle.Control;

namespace RPG.Core
{
    public class TestScene1 : MonoBehaviour
    {
        List<IEnumerator> list = new List<IEnumerator>();
        IEnumerator test;

        private void Start()
        {
            test = Test();
            StartCoroutine(test);
            list.Add(test);
        }

        private void Update()
        {
            if (test != null)
            {
                //Debug.Log("test != NULL");
            }
            else
            {
                //Debug.Log("test == NULL");
            }
        }
        IEnumerator Test()
        {
            float Time = 0;
            while (true)
            {
                Time += 1;
                Debug.Log(Time);
                if (Time > 5)
                {
                    break;
                }
                yield return new WaitForSeconds(1f);
            }
            Debug.Log("메소드 종료");
            test = null;
        }

        private void OnGUI()
        {
            if (GUI.Button(new Rect(100, 10, 80, 80), "Test"))
            {
                StopCoroutine(test);
            }

            if (GUI.Button(new Rect(100, 100, 80, 80), "Test"))
            {
                test.Reset();
            }

            if (GUI.Button(new Rect(100, 200, 80, 80), "Test"))
            {
                foreach (var item in list)
                {
                    if (item == test)
                    {
                        Debug.Log("있음");
                    }
                }
            }
        }


    }
}
