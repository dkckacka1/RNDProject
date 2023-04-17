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
            ui.InitIncant();
            ui.ChoiceItem(GameManager.Instance.Player.currentWeapon);
            
        }
        

        private void OnGUI()
        {
            if (GUI.Button(new Rect(10, 10, 100, 80), "��æƮ"))
            {
                ui.InitIncant();
            }

            if (GUI.Button(new Rect(10, 100, 100, 80), "�̱�"))
            {
                ui.InitGacha();
            }

            if (GUI.Button(new Rect(10, 200, 100, 80), "��ȭ"))
            {
                ui.InitReinforce();
            }

            if (GUI.Button(new Rect(130, 10, 100, 80), "����"))
            {
                ui.ChoiceItem(GameManager.Instance.Player.currentWeapon);
            }

            if (GUI.Button(new Rect(130, 100, 100, 80), "����"))
            {
                ui.ChoiceItem(GameManager.Instance.Player.currentArmor);
            }

            if (GUI.Button(new Rect(130, 200, 100, 80), "����"))
            {
                ui.ChoiceItem(GameManager.Instance.Player.currentPants);
            }

            if (GUI.Button(new Rect(130, 300, 100, 80), "���"))
            {
                ui.ChoiceItem(GameManager.Instance.Player.currentHelmet);
            }
        }
    }
}
