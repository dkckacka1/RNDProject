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
    public class EquipmentWindowUI : MonoBehaviour
    {
        PlayerStatus status;
        UserInfo userInfo;
        public Equipment choiceItem;

        [Header("Text")]
        [SerializeField] TextMeshProUGUI equipmentName;
        [SerializeField] TextMeshProUGUI equipmentDesc;
        [SerializeField] TextMeshProUGUI equipmentStatus;
        [SerializeField] TextMeshProUGUI equipmentReinforce;

        [Header("IncantColor")]
        [SerializeField] Color suffixColor; // 접두 표현 컬러
        [SerializeField] Color prefixColor; // 접미 표현 컬러

        [Header("ReinfoceColor")]
        [SerializeField] Color defaultColor;
        [SerializeField] Color reinfoce10;
        [SerializeField] Color reinfoce20;
        [SerializeField] Color reinfoce30;
        [SerializeField] Color reinfoce40;

        [Header("Buttons")]
        [SerializeField] TextMeshProUGUI reinforceCount;
        [SerializeField] TextMeshProUGUI incantCount;
        [SerializeField] TextMeshProUGUI itemTicketCount;

        public void Init(UserInfo userInfo, PlayerStatus status)
        {
            this.status = status;
            this.userInfo = userInfo;
            UpdateUserScroll();
            ShowWeapon();
        }

        #region ButtonPlugin
        public void ShowWeapon()
        {
            UpdateItem(status.currentWeapon);
        }

        public void ShowArmor()
        {
            UpdateItem(status.currentArmor);
        }

        public void ShowHelmet()
        {
            UpdateItem(status.currentHelmet);
        }

        public void ShowPants()
        {
            UpdateItem(status.currentPants);
        }


        #endregion

        public void UpdateUserScroll()
        {
            if (reinforceCount == null)
            {
                Debug.LogError("UI 미적용");
                return;
            }

            if (userInfo == null)
            {
                Debug.LogError("userinfo 없음!");
                return;
            }


            reinforceCount.text = $"X {userInfo.itemReinforceCount}";
            incantCount.text = $"X {userInfo.itemIncantCount}";
            itemTicketCount.text = $"X {userInfo.itemGachaTicket}";
        }

        public void UpdateItem(Equipment item)
        {
            choiceItem = item;

            ShowNameText(choiceItem);
            ShowDescText(choiceItem);
            switch (choiceItem.equipmentType)
            {
                case EquipmentItemType.Weapon:
                    ShowItemStatus((choiceItem as Weapon));
                    break;
                case EquipmentItemType.Armor:
                    ShowItemStatus((choiceItem as Armor));
                    break;
                case EquipmentItemType.Pants:
                    ShowItemStatus((choiceItem as Pants));
                    break;
                case EquipmentItemType.Helmet:
                    ShowItemStatus((choiceItem as Helmet));
                    break;
            }
            ShowReinforceCount(choiceItem);
        }

        public void ShowNameText(Equipment equipment)
        {
            string name = $"<color=\"white\">{equipment.itemName}";
            if (equipment.isIncant())
            {
                name = "\n" + name;
            }
            name = (equipment.suffix != null) ? MyUtility.returnColorText(equipment.suffix.name, suffixColor) + name : name;
            name = (equipment.prefix != null) ? MyUtility.returnColorText(equipment.prefix.name, prefixColor) + name : name;
            equipmentName.text = name;
        }

        public void ShowDescText(Equipment equipment)
        {
            string desc = equipment.description;
            desc = (equipment.prefix != null) ? $"{desc}\n{equipment.prefix.ShowDesc()}" : desc;
            desc = (equipment.suffix != null) ? $"{desc}\n{equipment.suffix.ShowDesc()}" : desc;
            equipmentDesc.text = desc;

        }

        public void ShowReinforceCount(Equipment equipment)
        {
            if (!equipment.isReinforce())
            {
                equipmentReinforce.gameObject.SetActive(false);
                return;
            }

            equipmentReinforce.gameObject.SetActive(true);
            equipmentReinforce.text = $"+{equipment.reinforceCount}";

            if (equipment.reinforceCount < 10)
            {
                equipmentReinforce.color = defaultColor;
            }
            else if (equipment.reinforceCount < 20)
            {
                equipmentReinforce.color = reinfoce10;
            }
            else if (equipment.reinforceCount < 30)
            {
                equipmentReinforce.color = reinfoce20;
            }
            else if (equipment.reinforceCount < 40)
            {
                equipmentReinforce.color = reinfoce30;
            }
            else if (equipment.reinforceCount < 50)
            {
                equipmentReinforce.color = reinfoce40;
            }
        }

        #region 장비 스텟 설명 부분


        public void ShowItemStatus(Weapon weapon)
        {
            string status =
                $"{MyUtility.returnSideText("공격력 :", weapon.attackDamage.ToString())}\n" +
                $"{MyUtility.returnSideText("공격 속도 :", $"초당 {weapon.attackSpeed}회 타격")}\n" +
                $"{MyUtility.returnSideText("공격 범위 :", weapon.attackRange.ToString())}\n" +
                $"{MyUtility.returnSideText("이동 속도 :", weapon.movementSpeed.ToString())}\n" +
                $"{MyUtility.returnSideText("치명타 확률 :", $"{weapon.criticalChance * 100}%")}\n" +
                $"{MyUtility.returnSideText("치명타 데미지 :", $"기본 공격력의 {weapon.criticalDamage * 100}%")}\n" +
                $"{MyUtility.returnSideText("명중률 :", $"{weapon.attackChance * 100}%")}";

            equipmentStatus.text = status;
        }

        public void ShowItemStatus(Armor armor)
        {
            string status =
                $"{MyUtility.returnSideText("체력 :", armor.hpPoint.ToString())}\n" +
                $"{MyUtility.returnSideText("방어력 :", $"{armor.defencePoint}")}\n" +
                $"{MyUtility.returnSideText("이동 속도 :", armor.movementSpeed.ToString())}\n" +
                $"{MyUtility.returnSideText("회피율 :", $"{armor.evasionPoint * 100}%")}";

            equipmentStatus.text = status;
        }

        public void ShowItemStatus(Helmet helmet)
        {
            string status =
                $"{MyUtility.returnSideText("체력 :", helmet.hpPoint.ToString())}\n" +
                $"{MyUtility.returnSideText("방어력 :", $"{helmet.defencePoint}")}\n" +
                $"{MyUtility.returnSideText("치명타 회피율 :", $"{helmet.evasionCritical * 100}%")}\n" +
                $"{MyUtility.returnSideText("치명타 피해 감소 :", $"{helmet.decreseCriticalDamage * 100}%")}";


            equipmentStatus.text = status;
        }

        public void ShowItemStatus(Pants pants)
        {
            string status =
                $"{MyUtility.returnSideText("체력 :", pants.hpPoint.ToString())}\n" +
                $"{MyUtility.returnSideText("방어력 :", $"{pants.defencePoint}")}\n" +
                $"{MyUtility.returnSideText("이동 속도 :", pants.movementSpeed.ToString())}";

            equipmentStatus.text = status;
        }
        #endregion


    }

}