using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using RPG.Character.Status;

namespace RPG.Main.UI
{
    public class MainUI : MonoBehaviour
    {
        UserInfo userinfo;
        PlayerStatus status;

        public EquipmentWindowUI equipmentUI;
        public PlayerStatusWindowUI statusUI;

        private void Start()
        {
            userinfo = GameManager.Instance.userInfo;
            status = GameManager.Instance.player;
            Init(status, userinfo);
        }

        public void Init(PlayerStatus status, UserInfo userInfo)
        {
            equipmentUI.Init(status);
            statusUI.Init(userInfo, status);
        }

        #region ButtonPlugin
        public void IncantItem()
        {
            Incant incant;
            if (GameManager.Instance.GachaIncat(equipmentUI.choiceItem.equipmentType, out incant))
            {
                equipmentUI.choiceItem.Incant(incant);
            }
            equipmentUI.choiceItem.UpdateItem();
            status.SetEquipment();
            statusUI.UpdateStatusText();
            statusUI.UpdateUserText();
            equipmentUI.ShowEquipmentItem(equipmentUI.choiceItem);
        }

        public void ReinforceItem()
        {
            equipmentUI.choiceItem.ReinforceItem();
            equipmentUI.choiceItem.UpdateItem();
            status.SetEquipment();
            statusUI.UpdateStatusText();
            statusUI.UpdateUserText();
            equipmentUI.ShowEquipmentItem(equipmentUI.choiceItem);
        }

        #endregion
    }
}