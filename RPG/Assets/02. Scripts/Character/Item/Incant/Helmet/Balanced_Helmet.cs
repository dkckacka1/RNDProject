using RPG.Character.Equipment;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balanced_Helmet : Incant
{
    public Balanced_Helmet()
    {
        incantType = IncantType.suffix;
        itemType = EquipmentItemType.Helmet;
        name = "균형의 ";
        addDesc = "치명타 피해 감소 +20%";
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
