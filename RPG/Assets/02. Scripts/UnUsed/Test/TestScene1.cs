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

        private void Update()
        {
            
        }
        

        private void OnGUI()
        {
            if (GUI.Button(new Rect(100, 10, 80, 80), "Test"))
            {
                var ability = BattleManager.ObjectPool.GetAbility(2);
                ability.InitAbility(target.transform, Nothing);
            }

            if (GUI.Button(new Rect(100, 100, 80, 80), "Test"))
            {
                
            }

            if (GUI.Button(new Rect(100, 200, 80, 80), "Test"))
            {
            }

        }

        public void Nothing(BattleStatus status)
        {

        }
    }
}
