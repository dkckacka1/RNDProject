using RPG.Character.Status;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    public class Heavy_Weapon : WeaponIncant
    {
        public override void IncantEquipment(Equipment equipment)
        {
            Weapon weapon = equipment as Weapon;

            weapon.AttackDamage += 30;
            weapon.AttackSpeed -= 2;
        }

        public override void RemoveIncant(Equipment equipment)
        {
            Weapon weapon = equipment as Weapon;

            weapon.AttackDamage -= 30;
            weapon.AttackSpeed += 2;
        }
    }
}
