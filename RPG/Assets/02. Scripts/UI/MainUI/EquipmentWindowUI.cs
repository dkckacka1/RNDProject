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

    [SerializeField] Color suffixColor; // ���� ǥ�� �÷�
    [SerializeField] Color prefixColor; // ���� ǥ�� �÷�

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
}
