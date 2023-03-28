using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RPG.Core;
using RPG.Character.Status;
using RPG.Character.Equipment;

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
            userinfo = GameManager.Instance.UserInfo;
            status = GameManager.Instance.Player;
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

        public void UpdateUI()
        {
            UpdateTicketCount();
            statusUI.UpdateStatusText();
            statusUI.UpdateUserText();
            equipmentUI.UpdateUserScroll();
            equipmentUI.UpdateItem(equipmentUI.choiceItem);
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
            Init(status, userinfo);
            if (backButton == null)
            {
                return;
            }
            backButton.gameObject.SetActive(true);
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

            equipmentUI.choiceItem.UpdateItem();
            status.SetEquipment();
            UpdateUI();
        }

        public void ReinforceItem()
        {
            if(userinfo.itemReinforceCount <= 0)
            {
                return;
            }

            userinfo.itemReinforceCount--;
            equipmentUI.choiceItem.ReinforceItem();

            equipmentUI.choiceItem.UpdateItem();
            status.SetEquipment();
            UpdateUI();
        }

        public void GachaItem()
        {
            if (userinfo.itemGachaTicket <= 0)
            {
                return;
            }

            userinfo.itemGachaTicket--;
            EquipmentItemType type = equipmentUI.choiceItem.equipmentType;
            switch (type)
            {
                case EquipmentItemType.Weapon:
                    {
                        WeaponData data;
                        if (RandomGacha.GachaRandomData(GameManager.Instance.EquipmentDataDic, type, out data))
                        {
                            status.currentWeapon.ChangeData(data);
                            status.currentWeapon.UpdateItem();

                        }
                    }

                    break;
                case EquipmentItemType.Armor:
                    {
                        ArmorData data;
                        if (RandomGacha.GachaRandomData(GameManager.Instance.EquipmentDataDic, type, out data))
                        {
                            status.currentArmor.ChangeData(data);
                            status.currentArmor.UpdateItem();
                        }
                    }
                    break;
                case EquipmentItemType.Pants:
                    {
                        PantsData data;
                        if (RandomGacha.GachaRandomData(GameManager.Instance.EquipmentDataDic, type, out data))
                        {
                            status.currentPants.ChangeData(data);
                            status.currentPants.UpdateItem();
                        }
                    }
                    break;
                case EquipmentItemType.Helmet:
                    {
                        HelmetData data;
                        if (RandomGacha.GachaRandomData(GameManager.Instance.EquipmentDataDic, type, out data))
                        {
                            status.currentHelmet.ChangeData(data);
                            status.currentHelmet.UpdateItem();
                        }
                    }
                    break;
            }
            status.SetEquipment();
            UpdateUI();
        }
        #endregion

        public void UpdateTicketCount()
        {
            if (reinforceCount == null)
            {
                return;
            }
            this.itemGachaTicketCount.text = $"»Ì±â±Ç : {userinfo.itemGachaTicket}";
            this.reinforceCount.text = $"°­È­±Ç : {userinfo.itemReinforceCount}";
            this.incantCount.text = $"ÀÎÃ¦Æ®±Ç : {userinfo.itemIncantCount}";
        }

    }
}