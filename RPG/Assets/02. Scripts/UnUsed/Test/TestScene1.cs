using System.Collections;
using System.Collections.Generic;
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
        private void Start()
        {
        }

        private void OnGUI()
        {
            if (GUI.Button(new Rect(10, 10, 200, 60), "공격하기"))
            {
            }

            if (GUI.Button(new Rect(10, 70, 200, 60), ""))
            {
            }

            if (GUI.Button(new Rect(10, 130, 200, 60), ""))
            {
            }
        }

    }
}
