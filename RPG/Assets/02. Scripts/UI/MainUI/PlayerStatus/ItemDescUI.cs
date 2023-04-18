using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using RPG.Character.Equipment;

namespace RPG.Main.UI.StatusUI
{
    public class ItemDescUI : MonoBehaviour
    {
        [SerializeField] Image itemImage;
        [SerializeField] TextMeshProUGUI itemNameText;

        public void ShowEquipment(Equipment equipment)
        {
            itemImage.sprite = equipment.data.equipmentSprite;

            string text = "";

            switch (equipment.equipmentType)
            {
                case EquipmentItemType.Weapon:
                    text += "���� : ";
                    break;
                case EquipmentItemType.Armor:
                    text += "���� : ";
                    break;
                case EquipmentItemType.Pants:
                    text += "���� : ";
                    break;
                case EquipmentItemType.Helmet:
                    text += "��� : ";
                    break;
            }

            if (equipment.prefix != null)
            {
                text += equipment.prefix.incantName + " ";
            }

            if (equipment.suffix != null)
            {
                text += equipment.suffix.incantName + " ";
            }

            text += equipment.itemName;
            itemNameText.text = text;
        }
    }
}