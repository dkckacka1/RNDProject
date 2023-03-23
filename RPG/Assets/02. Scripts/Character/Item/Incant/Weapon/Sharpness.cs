using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character.Equipment;

public class Sharpness : WeaponIncant
{
    public Sharpness()
    {
        incantType = IncantType.prefix;
        itemType = EquipmentItemType.Weapon;
        name = "날카로운 ";
        desc = "공격력을 10 만큼 증가시킵니다.";
    }

    public override void IncantRemove(Weapon weapon)
    {
        weapon.attackDamage -= 10;
    }

    public override void IncantWeapon(Weapon weapon)
    {
        weapon.attackDamage += 10;
    }
}
 