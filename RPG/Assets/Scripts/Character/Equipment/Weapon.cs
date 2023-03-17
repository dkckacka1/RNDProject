using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Character.Equipment
{
    public class Weapon : Equipment
    {
        public GameObject weaponLook;

        public int attackDamage;
        public float attackSpeed;
        public float attackDistance;
        public float movementSpeed;
        public float criticalChance;
        public float criticalDamage;
        public float attackChance;

        public Weapon(WeaponData data) : base(data)
        {
            attackDamage = data.attackDamage;
            attackSpeed = data.attackSpeed;
            attackDistance = data.attackDistance;
            movementSpeed = data.movementSpeed;
            criticalChance = data.criticalChance;
            criticalDamage = data.criticalDamage;
            attackChance = data.attackChance;
        }
    }
}