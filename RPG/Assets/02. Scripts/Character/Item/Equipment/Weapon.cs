using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;


namespace RPG.Character.Equipment
{
    public class Weapon : Equipment
    {
        public GameObject weaponLook;

        public int attackDamage;
        public float attackSpeed;
        public float attackRange;
        public float movementSpeed;
        public float criticalChance;
        public float criticalDamage;
        public float attackChance;

        public Weapon(WeaponData data) : base(data)
        {
            weaponLook = data.weaponLook;
            attackDamage = data.attackDamage;
            attackSpeed = data.attackSpeed;
            attackRange = data.attackRange;
            movementSpeed = data.movementSpeed;
            criticalChance = data.criticalChance;
            criticalDamage = data.criticalDamage;
            attackChance = data.attackChance;
        }

        public override void UpdateReinfoce()
        {
            attackDamage = (data as WeaponData).attackDamage + (int)((data as WeaponData).attackDamage * 0.1 * reinforceCount);
            attackSpeed = (data as WeaponData).attackSpeed;
            attackRange = (data as WeaponData).attackRange;
            movementSpeed = (data as WeaponData).movementSpeed;
            criticalChance = (data as WeaponData).criticalChance;
            criticalDamage = (data as WeaponData).criticalDamage;
            attackChance = (data as WeaponData).attackChance;
        }
    }
}