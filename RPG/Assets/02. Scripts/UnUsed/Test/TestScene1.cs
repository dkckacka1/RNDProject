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
        public int skillID;
        public GameObject startPos;
        private void Start()
        {
        }

        private void OnGUI()
        {
            if (GUI.Button(new Rect(10, 10, 200, 60), "스킬 사용"))
            {
                var ab = BattleManager.ObjectPool.GetAbility(skillID);
                ab.InitAbility(startPos.transform);
            }
        }

    }
}
