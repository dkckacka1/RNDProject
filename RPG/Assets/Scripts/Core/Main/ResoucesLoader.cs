using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character.Equipment;

namespace RPG.Core
{
    public static class ResoucesLoader
    {
        public static T LoadEquipment<T>(string equpimentName, EquipmentItemType type)
            where T : EquipmentData
        {
            string path = "Data/";

            switch (type)
            {
                case EquipmentItemType.Weapon:
                    path += ("Weapon/" + equpimentName);
                    break;
                case EquipmentItemType.Armor:
                    path += ("Armor/" + equpimentName);
                    break;
                case EquipmentItemType.Pants:
                    path += ("Pants/" + equpimentName);
                    break;
                case EquipmentItemType.Helmet:
                    path += ("Helmet/" + equpimentName);
                    break;
                    //case EquipmentType.Accessory:
                    //    path += ("Weapon/" + equpimentName);
                    //    break;
            }

            T item = Resources.Load(path) as T;

            if (item == null)
            {
                Debug.LogError("찾는 아이템이 없습니다.");
            }

            return item;
        }

    } 
}
