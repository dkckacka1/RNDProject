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
        name = "��ī�ο� ";
        desc = "���ݷ��� 10 ��ŭ ������ŵ�ϴ�.";
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
 