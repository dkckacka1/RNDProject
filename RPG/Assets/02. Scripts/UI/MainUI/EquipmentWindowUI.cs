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
        public Equipment choiceItem;

        [Header("StatusText")]
        [SerializeField] TextMeshProUGUI equipmentName;
        [SerializeField] TextMeshProUGUI equipmentDesc;
        [SerializeField] TextMeshProUGUI equipmentStatus;
        [SerializeField] TextMeshProUGUI equipmentReinforce;

        [Header("ButtonText")]
        [SerializeField] TextMeshProUGUI reinforceCount;
        [SerializeField] TextMeshProUGUI reinforceProbailityText;
        [SerializeField] TextMeshProUGUI incantCount;
        [SerializeField] TextMeshProUGUI itemTicketCount;

        [Header("IncantColor")]
        [SerializeField] Color suffixColor; // ���� ǥ�� �÷�
        [SerializeField] Color prefixColor; // ���� ǥ�� �÷�

        [Header("ReinfoceColor")]
        [SerializeField] Color defaultColor;
        [SerializeField] Color reinfoce10;
        [SerializeField] Color reinfoce20;
        [SerializeField] Color reinfoce30;
        [SerializeField] Color reinfoce40;

        private void OnEnable()
        {
            ShowWeapon();
        }

        public void Init()
        {
            ShowUserResource();
            ShowWeapon();
        }

        #region ButtonPlugin
        public void ShowWeapon()
        {
            UpdateItem(GameManager.Instance.Player.currentWeapon);
        }

        public void ShowArmor()
        {
            UpdateItem(GameManager.Instance.Player.currentArmor);
        }

        public void ShowHelmet()
        {
            UpdateItem(GameManager.Instance.Player.currentHelmet);
        }

        public void ShowPants()
        {
            UpdateItem(GameManager.Instance.Player.currentPants);
        }


        #endregion

        public void ShowUserResource()
        {
            if (reinforceCount == null)
            {
                Debug.LogError("UI ������");
                return;
            }

            if (GameManager.Instance.UserInfo == null)
            {
                Debug.LogError("userinfo ����!");
                return;
            }

            reinforceCount.text = $"X {GameManager.Instance.UserInfo.itemReinforceCount}";
            incantCount.text = $"X {GameManager.Instance.UserInfo.itemIncantCount}";
            itemTicketCount.text = $"X {GameManager.Instance.UserInfo.itemGachaTicket}";
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
            ShowReinforceSuccessProbaility();
        }

        private void ShowReinforceSuccessProbaility()
        {
            reinforceProbailityText.text = $"(��ȭȮ��:{RandomSystem.ReinforceCalc(choiceItem)}%)";
        }

        public void ShowNameText(Equipment equipment)
        {
            string name = $"<color=\"white\">{equipment.itemName}";
            if (equipment.isIncant())
            {
                name = "\n" + name;
            }
            name = (equipment.suffix != null) ? MyUtility.returnColorText(equipment.suffix.IncantName, suffixColor) + name : name;
            name = (equipment.prefix != null) ? MyUtility.returnColorText(equipment.prefix.IncantName, prefixColor) + name : name;
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

        #region ��� ���� ���� �κ�


        public void ShowItemStatus(Weapon weapon)
        {
            string status =
                $"{MyUtility.returnSideText("���ݷ� :", weapon.AttackDamage.ToString())}\n" +
                $"{MyUtility.returnSideText("���� �ӵ� :", $"�ʴ� {weapon.AttackSpeed}ȸ Ÿ��")}\n" +
                $"{MyUtility.returnSideText("���� ���� :", weapon.AttackRange.ToString())}\n" +
                $"{MyUtility.returnSideText("�̵� �ӵ� :", weapon.MovementSpeed.ToString())}\n" +
                $"{MyUtility.returnSideText("ġ��Ÿ Ȯ�� :", $"{weapon.CriticalChance * 100}%")}\n" +
                $"{MyUtility.returnSideText("ġ��Ÿ ������ :", $"�⺻ ���ݷ��� {weapon.CriticalDamage * 100}%")}\n" +
                $"{MyUtility.returnSideText("���߷� :", $"{weapon.AttackChance * 100}%")}";

            equipmentStatus.text = status;
        }

        public void ShowItemStatus(Armor armor)
        {
            string status =
                $"{MyUtility.returnSideText("ü�� :", armor.HpPoint.ToString())}\n" +
                $"{MyUtility.returnSideText("���� :", $"{armor.DefencePoint}")}\n" +
                $"{MyUtility.returnSideText("�̵� �ӵ� :", armor.MovementSpeed.ToString())}\n" +
                $"{MyUtility.returnSideText("ȸ���� :", $"{armor.EvasionPoint * 100}%")}";

            equipmentStatus.text = status;
        }

        public void ShowItemStatus(Helmet helmet)
        {
            string status =
                $"{MyUtility.returnSideText("ü�� :", helmet.HpPoint.ToString())}\n" +
                $"{MyUtility.returnSideText("���� :", $"{helmet.DefencePoint}")}\n" +
                $"{MyUtility.returnSideText("ġ��Ÿ ȸ���� :", $"{helmet.EvasionCritical * 100}%")}\n" +
                $"{MyUtility.returnSideText("ġ��Ÿ ���� ���� :", $"{helmet.DecreseCriticalDamage * 100}%")}";


            equipmentStatus.text = status;
        }

        public void ShowItemStatus(Pants pants)
        {
            string status =
                $"{MyUtility.returnSideText("ü�� :", pants.HpPoint.ToString())}\n" +
                $"{MyUtility.returnSideText("���� :", $"{pants.DefencePoint}")}\n" +
                $"{MyUtility.returnSideText("�̵� �ӵ� :", pants.MovementSpeed.ToString())}";

            equipmentStatus.text = status;
        }
        #endregion


    }

}