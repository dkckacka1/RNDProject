using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RPG.Character.Equipment;

public class EquipmentWindowUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI equipmentName;
    [SerializeField] TextMeshProUGUI equipmentDesc;
    [SerializeField] TextMeshProUGUI equipmentStatus;

    // TODO : 표현식에 컬러 표시하기
    [SerializeField] Color suffixColor; // 접두 표현 컬러
    [SerializeField] Color prefixColor; // 접미 표현 컬러

    public void ShowEquipmentItem(Weapon weapon)
    {
        ShowNameText(weapon);
        ShowDescText(weapon);
        ShowItemStatus(weapon);
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
}
