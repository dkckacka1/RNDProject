using RPG.Character.Equipment;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quick_Pants : Incant
{
    public Quick_Pants()
    {
        incantType = IncantType.prefix;
        itemType = EquipmentItemType.Pants;
        name = "����� ";
        addDesc = "�̵��ӵ� +1.5";
    }

    public override void IncantEquipment(Equipment equipment)
    {
        Pants pants = equipment as Pants;

        pants.movementSpeed += 1.5f;
    }

    public override void RemoveIncant(Equipment equipment)
    {
        Pants pants = equipment as Pants;

        pants.movementSpeed -= 1.5f;
    }
}