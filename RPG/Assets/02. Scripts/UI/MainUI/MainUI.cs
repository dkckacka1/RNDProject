using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using RPG.Character.Status;

namespace RPG.Main.UI
{
    public class MainUI : MonoBehaviour
    {
        public EquipmentWindowUI equipmentUI;

        private void Start()
        {
            Init(GameManager.Instance.status);
        }

        public void Init(PlayerStatus status)
        {
            equipmentUI.Init(status);
        }
    }
}