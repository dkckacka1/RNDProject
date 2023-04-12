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
        public GameObject target;

        private void Start()
        {

        }

        private void OnGUI()
        {
            if (GUI.Button(new Rect(10, 10, 200, 60), "스킬 사용"))
            {
                BattleManager.ObjectPool.GetText("1000", target.transform.position);

            }
        }

    }
}
