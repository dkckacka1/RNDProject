using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character.Equipment;

public class Fast : WeaponIncant
{
    public Fast()
    {
        incantType = IncantType.suffix;
        itemType = EquipmentItemType.Weapon;
        name = "신속의 ";
        desc = "공격속도를을 1 만큼 증가시킵니다.";
    }

    public override void IncantRemove(Weapon weapon)
    {
        weapon.attackSpeed += 1f;
    }

    public override void IncantWeapon(Weapon weapon)
    {
        weapon.attackSpeed -= 1f;
    }
}
