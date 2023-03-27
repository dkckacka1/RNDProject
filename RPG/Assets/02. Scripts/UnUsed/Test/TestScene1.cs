using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character.Status;
using RPG.Character.Equipment;
using RPG.Battle.Core;
using RPG.Main.UI;

namespace RPG.Core
{
    public class TestScene1 : MonoBehaviour
    {
        HelmetData helmetData;

        private void Start()
        {
            TestClass.TestMethod();
        }

        public void OnGUI()
        {
            if (GUI.Button(new Rect(10, 10, 200, 60), "°©¿Ê »Ì±â"))
            {
                if (RandomGacha.GachaRandomData(GameManager.Instance.helmetDataDic, out helmetData))
                {
                    print(helmetData.EquipmentName);
                }
            }
        }
    } 
}
