using RPG.Character.Equipment;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hard_Armor : Incant
{
    public Hard_Armor()
    {
        incantType = IncantType.prefix;
        itemType = EquipmentItemType.Armor;
        name = "�ܴ��� ";
        addDesc = "���� +3 ü�� +100";
        minusDesc = "�̵��ӵ� -1";
    }

    public override void IncantEquipment(Equipment equipment)
    {
        Armor armor = equipment as Armor;

        armor.defencePoint += 3;
        armor.hpPoint += 100;
        armor.movementSpeed -= 1;
    }

    public override void RemoveIncant(Equipment equipment)
    {
        Armor armor = equipment as Armor;

        armor.defencePoint -= 3;
        armor.hpPoint -= 100;
        armor.movementSpeed += 1;
    }
}