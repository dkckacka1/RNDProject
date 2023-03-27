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
        [SerializeField] Color suffixColor; // ���� ǥ�� �÷�
        [SerializeField] Color prefixColor; // ���� ǥ�� �÷�

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
            ShowUserScrollCount();
            ShowWeapon();
        }

        #region ButtonPlugin
        public void ShowWeapon()
        {
            ShowEquipmentItem(status.currentWeapon);
        }

        public void ShowArmor()
        {
            ShowEquipmentItem(status.currentArmor);
        }

        public void ShowHelmet()
        {
            ShowEquipmentItem(status.currentHelmet);
        }

        public void ShowPants()
        {
            ShowEquipmentItem(status.currentPants);
        }

        
        #endregion

        public void ShowUserScrollCount()
        {
            reinforceCount.text = $"X {userInfo.itemReinforceCount}";
            incantCount.text = $"X {userInfo.itemIncantCount}";
            itemTicketCount.text = $"X {userInfo.itemGachaTicket}";
        }

        public void ShowEquipmentItem(Equipment item)
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
            name = (equipment.suffix != null) ? MyUtility.returnColorText(equipment.suffix.name, suffixColor, equipmentName.color) + name : name;
            name = (equipment.prefix != null) ? MyUtility.returnColorText(equipment.prefix.name, prefixColor, equipmentName.color) + name : name;
            equipmentName.text = name;
        }

        public void ShowDescText(Equipment equipment)
        {
            string desc = equipment.description;
            desc = (equipment.prefix != null) ? $"{desc}\n{equipment.prefix.ShowDesc(equipmentDesc.color)}" : desc;
            desc = (equipment.suffix != null) ? $"{desc}\n{equipment.suffix.ShowDesc(equipmentDesc.color)}" : desc;
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

        #region ��� ���� ���� �κ�


        public void ShowItemStatus(Weapon weapon)
        {
            string status =
                $"���ݷ�\t\t{weapon.attackDamage}\n" +
                $"���� �ӵ�\t\t�ʴ� {weapon.attackSpeed}ȸ Ÿ��\n" +
                $"���� ����\t\t{weapon.attackRange}\n" +
                $"�̵� �ӵ�\t\t{weapon.movementSpeed}\n" +
                $"ġ��Ÿ Ȯ��\t{weapon.criticalChance * 100}%\n" +
                $"ġ��Ÿ ������\t�⺻ ���ݷ��� {weapon.criticalDamage * 100}%\n" +
                $"���߷�\t\t{weapon.attackChance * 100}%";


            equipmentStatus.text = status;
        }

        public void ShowItemStatus(Armor armor)
        {
            string status =
        $"ü��\t\t\t{armor.hpPoint}\n" +
        $"����\t\t{armor.defencePoint}\n" +
        $"�̵� �ӵ�\t\t{armor.movementSpeed}\n" +
        $"ȸ����\t\t{armor.evasionPoint * 100}%";


            equipmentStatus.text = status;
        }

        public void ShowItemStatus(Helmet helmet)
        {
            string status =
        $"ü��\t\t\t{helmet.hpPoint}\n" +
        $"����\t\t{helmet.defencePoint}\n" +
        $"ġ��Ÿ ȸ����\t{helmet.decreseCriticalDamage * 100}%\n" +
        $"ġ��Ÿ ���� ����\t{helmet.evasionCritical * 100}%";


            equipmentStatus.text = status;
        }

        public void ShowItemStatus(Pants pants)
        {
            string status =
        $"ü��\t\t\t{pants.hpPoint}\n" +
        $"����\t\t{pants.defencePoint}\n" +
        $"�̵� �ӵ�\t{pants.movementSpeed}\n";


            equipmentStatus.text = status;
        }
        #endregion


    }

}