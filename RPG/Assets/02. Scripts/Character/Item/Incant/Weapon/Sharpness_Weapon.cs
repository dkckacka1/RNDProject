using RPG.Character.Status;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    public class Sharpness_Weapon : WeaponIncant
    {
        public Sharpness_Weapon()
        {
            incantType = IncantType.prefix;
            itemType = EquipmentItemType.Weapon;
            name = "날카로운 ";
            addDesc = "공격력 +10";
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

        public override void AttackEvent(BattleStatus player, BattleStatus enemy)
        {
        }
    }
}