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
