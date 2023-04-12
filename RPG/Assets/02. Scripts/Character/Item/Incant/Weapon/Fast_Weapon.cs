using RPG.Character.Status;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    public class Fast_Weapon : WeaponIncant
    {
        public override void IncantEquipment(Equipment equipment)
        {
            Weapon weapon = equipment as Weapon;

            weapon.AttackSpeed += 1;
            weapon.AttackDamage -= 1;
        }

        public override void RemoveIncant(Equipment equipment)
        {
            Weapon weapon = equipment as Weapon;

            weapon.AttackSpeed -= 1;
            weapon.AttackDamage += 1;
        }
    }
}