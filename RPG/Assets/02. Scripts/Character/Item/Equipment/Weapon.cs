using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;


namespace RPG.Character.Equipment
{
    public class Weapon : Equipment
    {
        public GameObject weaponLook;

        private int attackDamage;
        private float attackSpeed;
        private float attackRange;
        private float movementSpeed;
        private float criticalChance;
        private float criticalDamage;
        private float attackChance;

        // Encapsulation
        public int AttackDamage { get => attackDamage; set => attackDamage = value; }
        public float AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
        public float AttackRange { get => attackRange; set => attackRange = value; }
        public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
        public float CriticalChance { get => criticalChance; set => criticalChance = value; }
        public float CriticalDamage { get => criticalDamage; set => criticalDamage = value; }
        public float AttackChance { get => attackChance; set => attackChance = value; }

        public Weapon(Weapon weapon) : base(weapon)
        {
            weaponLook = weapon.weaponLook;
            AttackDamage = weapon.AttackDamage;
            AttackSpeed = weapon.AttackSpeed;
            AttackRange = weapon.AttackRange;
            MovementSpeed = weapon.MovementSpeed;
            CriticalChance = weapon.CriticalChance;
            CriticalDamage = weapon.CriticalDamage;
            AttackChance = weapon.AttackChance;

            this.UpdateItem();
        }

        public Weapon(WeaponData data) : base(data)
        {
            weaponLook = data.weaponLook;
            AttackDamage = data.attackDamage;
            AttackSpeed = data.attackSpeed;
            AttackRange = data.attackRange;
            MovementSpeed = data.movementSpeed;
            CriticalChance = data.criticalChance;
            CriticalDamage = data.criticalDamage;
            AttackChance = data.attackChance;
        }

        public override void ChangeData(EquipmentData data)
        {
            if (!(data is WeaponData))
            {
                Debug.LogError("잘못된 데이타 형식입니다.");
            }

            base.ChangeData(data);
            AttackDamage = (data as WeaponData).attackDamage;
            AttackSpeed = (data as WeaponData).attackSpeed;
            AttackRange = (data as WeaponData).attackRange;
            MovementSpeed = (data as WeaponData).movementSpeed;
            CriticalChance = (data as WeaponData).criticalChance;
            CriticalDamage = (data as WeaponData).criticalDamage;
            AttackChance = (data as WeaponData).attackChance;
        }

        public override void UpdateReinfoce()
        {
            AttackDamage = (data as WeaponData).attackDamage + (int)((data as WeaponData).attackDamage * 0.1 * reinforceCount);
            AttackSpeed = (data as WeaponData).attackSpeed;
            AttackRange = (data as WeaponData).attackRange;
            MovementSpeed = (data as WeaponData).movementSpeed;
            CriticalChance = (data as WeaponData).criticalChance;
            CriticalDamage = (data as WeaponData).criticalDamage;
            AttackChance = (data as WeaponData).attackChance;
        }
    }
}