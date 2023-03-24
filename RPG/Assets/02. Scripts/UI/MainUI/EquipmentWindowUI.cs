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

    // TODO : ǥ���Ŀ� �÷� ǥ���ϱ�
    [SerializeField] Color suffixColor; // ���� ǥ�� �÷�
    [SerializeField] Color prefixColor; // ���� ǥ�� �÷�

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
            $"���ݷ�\t\t{weapon.attackDamage}\n" +
            $"���� �ӵ�\t\t�ʴ� {weapon.attackSpeed}ȸ Ÿ��\n" +
            $"���� ����\t\t{weapon.attackRange}\n" +
            $"�̵� �ӵ�\t\t{weapon.movementSpeed}\n" +
            $"ġ��Ÿ Ȯ��\t{weapon.criticalChance * 100}%\n" +
            $"ġ��Ÿ ������\t�⺻ ���ݷ��� {weapon.criticalDamage * 100}%\n" +
            $"���߷�\t\t{weapon.attackChance * 100}%";


        equipmentStatus.text = status;
    }
}
