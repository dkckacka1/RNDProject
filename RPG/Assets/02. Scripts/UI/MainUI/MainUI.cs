using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
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

        [SerializeField] TextMeshProUGUI reinforceCount;
        [SerializeField] TextMeshProUGUI incantCount;
        [SerializeField] TextMeshProUGUI itemGachaTicketCount;

        [SerializeField] Button backButton;

        private void Start()
        {
            userinfo = GameManager.Instance.userInfo;
            status = GameManager.Instance.player;
            Init(status, userinfo);
            UpdateTicketCount();

            equipmentUI.gameObject.SetActive(false);
            statusUI.gameObject.SetActive(false);
        }

        public void Init(PlayerStatus status, UserInfo userInfo)
        {
            equipmentUI.Init(userinfo, status);
            statusUI.Init(userInfo, status);
        }

        #region ButtonPlugin
        public void SetActiveFalseUI()
        {
            equipmentUI.gameObject.SetActive(false);
            statusUI.gameObject.SetActive(false);
            backButton.gameObject.SetActive(false);
        }

        public void ShowStatusUI()
        {
            equipmentUI.gameObject.SetActive(true);
            statusUI.gameObject.SetActive(true);
            backButton.gameObject.SetActive(true);
            Init(status, userinfo);
        }

        public void IncantItem()
        {
            if(userinfo.itemIncantCount <= 0)
            {
                return;
            }

            userinfo.itemIncantCount--;

            Incant incant;
            if (RandomGacha.GachaIncat(equipmentUI.choiceItem.equipmentType, GameManager.Instance.incantDic, out incant))
            {
                equipmentUI.choiceItem.Incant(incant);
            }

            UpdateTicketCount();
            equipmentUI.choiceItem.UpdateItem();
            status.SetEquipment();
            statusUI.UpdateStatusText();
            statusUI.UpdateUserText();
            equipmentUI.ShowUserScrollCount();
            equipmentUI.ShowEquipmentItem(equipmentUI.choiceItem);
        }

        public void ReinforceItem()
        {
            if(userinfo.itemReinforceCount <= 0)
            {
                return;
            }

            userinfo.itemReinforceCount--;

            UpdateTicketCount();
            equipmentUI.choiceItem.ReinforceItem();
            equipmentUI.choiceItem.UpdateItem();
            status.SetEquipment();
            statusUI.UpdateStatusText();
            statusUI.UpdateUserText();
            equipmentUI.ShowUserScrollCount();
            equipmentUI.ShowEquipmentItem(equipmentUI.choiceItem);
        }
        #endregion

        public void UpdateTicketCount()
        {
            this.itemGachaTicketCount.text = $"»Ì±â±Ç : {userinfo.itemGachaTicket}";
            this.reinforceCount.text = $"°­È­±Ç : {userinfo.itemReinforceCount}";
            this.incantCount.text = $"ÀÎÃ¦Æ®±Ç : {userinfo.itemIncantCount}";
        }
    }
}