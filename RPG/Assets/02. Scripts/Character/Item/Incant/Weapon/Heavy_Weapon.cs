using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    public class Heavy_Weapon : Incant
    {
        public Heavy_Weapon()
        {
            incantType = IncantType.prefix;
            itemType = EquipmentItemType.Weapon;
            name = "무거운 ";
            addDesc = "공격력 +30";
            minusDesc = "공격속도 -2";
        }

        public override void IncantEquipment(Equipment equipment)
        {
            Weapon weapon = equipment as Weapon;

            weapon.attackDamage += 30;
            weapon.attackSpeed -= 2;
        }

        public override void RemoveIncant(Equipment equipment)
        {
            Weapon weapon = equipment as Weapon;

            weapon.attackDamage -= 30;
            weapon.attackSpeed += 2;
        }
    }

}
