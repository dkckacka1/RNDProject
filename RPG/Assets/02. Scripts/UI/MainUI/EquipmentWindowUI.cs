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
    }

    public void ShowNameText(Weapon weapon)
    {
        string name = "<color=\"black\">" + weapon.itemName;
        name = (weapon.suffix != null) ? "<color=\"green\">" + weapon.suffix.name + " " + name : name;
        name = (weapon.prefix != null) ? "<color=\"red\">" + weapon.prefix.name + " " + name : name;
        equipmentName.text = name;
    }

    public void ShowDescText(Weapon weapon)
    {
        string desc = weapon.description;
    }

    public void ShowItemStatus(Weapon weapon)
    {
        string status;
    }
}
