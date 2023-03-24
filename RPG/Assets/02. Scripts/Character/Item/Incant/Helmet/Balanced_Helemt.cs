using RPG.Character.Equipment;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balanced_Helemt : Incant
{
    public Balanced_Helemt()
    {
        incantType = IncantType.suffix;
        itemType = EquipmentItemType.Helmet;
        name = "������ ";
        addDesc = "ġ��Ÿ ���� ���� +20%";
    }

    public override void IncantEquipment(Equipment equipment)
    {
        Helmet helmet = equipment as Helmet;

        helmet.decreseCriticalDamage += 0.2f;
    }

    public override void RemoveIncant(Equipment equipment)
    {
        Helmet helmet = equipment as Helmet;

        helmet.decreseCriticalDamage -= 0.2f;
    }
}
