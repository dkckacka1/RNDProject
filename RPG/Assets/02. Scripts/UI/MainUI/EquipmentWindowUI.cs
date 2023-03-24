using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RPG.Core;
using RPG.Character.Equipment;

public class EquipmentWindowUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI equipmentName;
    [SerializeField] TextMeshProUGUI equipmentDesc;
    [SerializeField] TextMeshProUGUI equipmentStatus;

    [SerializeField] Color suffixColor; // 접두 표현 컬러
    [SerializeField] Color prefixColor; // 접미 표현 컬러

    #region ButtonPlugin
    #endregion

    public void ShowEquipmentItem(Equipment item)
    {
        ShowNameText(item);
        ShowDescText(item);
        switch (item.equipmentType)
        {
            case EquipmentItemType.Weapon:
                ShowItemStatus((item as Weapon));
                break;
            case EquipmentItemType.Armor:
                ShowItemStatus((item as Armor));
                break;
            case EquipmentItemType.Pants:
                ShowItemStatus((item as Pants));
                break;
            case EquipmentItemType.Helmet:
                ShowItemStatus((item as Helmet));
                break;
        }
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

    public void ShowItemStatus(Weapon weapon)
    {
        string status =
            $"공격력\t\t{weapon.attackDamage}\n" +
            $"공격 속도\t\t초당 {weapon.attackSpeed}회 타격\n" +
            $"공격 범위\t\t{weapon.attackRange}\n" +
            $"이동 속도\t\t{weapon.movementSpeed}\n" +
            $"치명타 확률\t{weapon.criticalChance * 100}%\n" +
            $"치명타 데미지\t기본 공격력의 {weapon.criticalDamage * 100}%\n" +
            $"명중률\t\t{weapon.attackChance * 100}%";


        equipmentStatus.text = status;
    }

    public void ShowItemStatus(Armor armor)
    {
        string status =
    $"체력\t\t\t{armor.hpPoint}\n" +
    $"방어력\t\t{armor.defencePoint}\n" +
    $"이동 속도\t\t{armor.movementSpeed}\n" +
    $"회피율\t\t{armor.evasionPoint * 100}%";


        equipmentStatus.text = status;
    }

    public void ShowItemStatus(Helmet helmet)
    {
        string status =
    $"체력\t\t\t{helmet.hpPoint}\n" +
    $"방어력\t\t{helmet.defencePoint}\n" +
    $"치명타 회피율\t{helmet.decreseCriticalDamage * 100}%\n" +
    $"치명타 피해 감소\t{helmet.evasionCritical * 100}%";


        equipmentStatus.text = status;
    }

    public void ShowItemStatus(Pants pants)
    {
        string status =
    $"체력\t\t\t{pants.hpPoint}\n" +
    $"방어력\t\t{pants.defencePoint}\n" +
    $"이동 속도\t{pants.movementSpeed}\n";


        equipmentStatus.text = status;
    }
}
