using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character.Equipment;

public class Fast_Weapon : Incant
{
    public Fast_Weapon()
    {
        incantType = IncantType.suffix;
        itemType = EquipmentItemType.Weapon;
        name = "�ż��� ";
        addDesc = "���ݼӵ� +1";
        minusDesc = "���ݷ� -1";
    }

    public override void IncantEquipment(Equipment equipment)
    {
        Weapon weapon = equipment as Weapon;

        weapon.attackSpeed += 1;
        weapon.attackDamage -= 1;
    }

    public override void RemoveIncant(Equipment equipment)
    {
        Weapon weapon = equipment as Weapon;

        weapon.attackSpeed -= 1;
        weapon.attackDamage += 1;
    }
}
