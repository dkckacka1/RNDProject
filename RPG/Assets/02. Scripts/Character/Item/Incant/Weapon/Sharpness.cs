using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character.Equipment;

public class Sharpness : Incant
{
    public Sharpness()
    {
        incantType = IncantType.prefix;
        itemType = EquipmentItemType.Weapon;
        name = "��ī�ο� ";
        addDesc = "���ݷ� +10";
        minusDesc = "";
    }

    public override void IncantEquipment(Equipment equipment)
    {
        Weapon weapon = equipment as Weapon;

        weapon.attackDamage += 10;
    }

    public override void RemoveIncant(Equipment equipment)
    {
        Weapon weapon = equipment as Weapon;

        weapon.attackDamage -= 10;
    }
}
 