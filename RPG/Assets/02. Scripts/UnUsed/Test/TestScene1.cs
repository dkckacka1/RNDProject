using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character.Status;
using RPG.Character.Equipment;
using RPG.Battle.Core;
using RPG.Main.UI;
using RPG.Battle.UI;

namespace RPG.Core
{
    public class TestScene1 : MonoBehaviour
    {
        public BattleText text;
        public Transform spawnPosition;

        private void Start()
        {
        }

        private void OnGUI()
        {
            if (GUI.Button(new Rect(10, 10, 200, 60), "클릭버튼"))
            {
                text.SetText((1000).ToString(), spawnPosition.position);
            }
        }
    } 
}
