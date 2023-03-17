using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResoucesLoader 
{
    public static T LoadEquipment<T>(string equpimentName, EquipmentType type) where T : Equipment
    {
        string path = "Data/";

        switch (type)
        {
            case EquipmentType.Weapon:
                path += ("Weapon/" + equpimentName);
                break;
            case EquipmentType.Armor:
                path += ("Armor/" + equpimentName);
                break;
            case EquipmentType.Pants:
                path += ("Pants/" + equpimentName);
                break;
            case EquipmentType.Helmet:
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
