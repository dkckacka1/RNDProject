using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character.Equipment;

namespace RPG.Character.Status
{
    public class PlayerStatus : BattleStatus
    {
        [Header("Equipment")]
        public Transform weaponHandle;
        public Weapon currentWeapon;
        public Armor currentArmor;
        public Helmet currentHelmet;
        public Pants currentPants;

        protected override void OnEnable()
        {

        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public void SetEquipment()
        {
            if (currentWeapon   == null   ||
                currentArmor    == null   ||
                currentHelmet   == null   ||
                currentPants    == null)
                return;

            attackDamage =          currentWeapon.attackDamage;
            attackRange =           currentWeapon.attackChance;
            attackSpeed =           currentWeapon.attackSpeed;
            criticalChance =        currentWeapon.criticalChance;
            criticalDamage =        currentWeapon.criticalDamage;
            attackChance =          currentWeapon.attackChance;

            maxHp =                 currentArmor.hpPoint + currentHelmet.hpPoint + currentPants.hpPoint;
            defencePoint =          currentArmor.defencePoint + currentHelmet.defencePoint + currentPants.defencePoint;
            evasionPoint =          currentArmor.evasionPoint;
            decreseCriticalDamage = currentHelmet.decreseCriticalDamage;
            evasionCritical =       currentHelmet.evasionCritical;

            movementSpeed =         currentWeapon.movementSpeed + currentPants.movementSpeed;
        }

        #region ¿Â∫Ò_¿Â¬¯
        public void EquipItem(Weapon weapon)
        {
            currentWeapon = weapon;
            Instantiate(currentWeapon.weaponLook, weaponHandle);

            SetEquipment();
        }

        public void EquipItem(Armor armor)
        {
            currentArmor = armor;

            SetEquipment();
        }

        public void EquipItem(Helmet helmet)
        {
            currentHelmet = helmet;

            SetEquipment();
        }

        public void EquipItem(Pants pants)
        {
            currentPants = pants;

            SetEquipment();
        }
        #endregion
    }
}