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
        public MainUI ui;

        private void Start()
        {
            ui.ShowStatusUI();
        }

        public void OnGUI()
        {
            if (GUI.Button(new Rect(10, 10, 200, 60), "장비 확인"))
            {
                print(ui.equipmentUI.choiceItem.ToString());
            }
        }
    } 
}
