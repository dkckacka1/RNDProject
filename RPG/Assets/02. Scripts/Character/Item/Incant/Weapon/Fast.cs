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
        name = "�ż��� ";
        desc = "���ݼӵ����� 1 ��ŭ ������ŵ�ϴ�.";
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
