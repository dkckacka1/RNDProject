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
        public ItemPopupUI ui;
        private void Start()
        {
            Incant incant = GameManager.Instance.incantDic[0];
            GameManager.Instance.Player.currentWeapon.Incant(incant);
            ui.InitGacha();
            ui.ChoiceItem(GameManager.Instance.Player.currentWeapon);
            
        }

        private void Update()
        {
            
        }
        

        private void OnGUI()
        {
        }
    }
}
