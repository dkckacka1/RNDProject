using RPG.Character.Equipment;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smooth_Armor : Incant
{
    public Smooth_Armor()
    {
        incantType = IncantType.suffix;
        itemType = EquipmentItemType.Armor;
        name = "�Ų� ";
        addDesc = "�̵��ӵ� +0.5";
        minusDesc = "���� -1";
    }

    public override void IncantEquipment(Equipment equipment)
    {
        Armor armor = equipment as Armor;

        armor.movementSpeed += 0.5f;
        armor.defencePoint -= 1;
    }

    public override void RemoveIncant(Equipment equipment)
    {
        Armor armor = equipment as Armor;

        armor.movementSpeed -= 0.5f;
        armor.defencePoint += 1;
    }
}
