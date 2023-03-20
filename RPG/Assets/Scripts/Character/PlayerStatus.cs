using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character.Equipment;

namespace RPG.Character.Status
{
    public class PlayerStatus : Status
    {
        public override void Initialize()
        {
            base.Initialize();
            currentHp = maxHp;
        }

        #region ¿Â∫Ò_¿Â¬¯
        public void EquipItem(Weapon weapon)
        {
            if (currentWeapon != null)
            {
                attackDamage -= currentWeapon.attackDamage;
                attackSpeed -= currentWeapon.attackSpeed;
                movementSpeed -= currentWeapon.movementSpeed;
                criticalChance -= currentWeapon.criticalChance;
                criticalDamage -= currentWeapon.criticalDamage;
                attackChance -= currentWeapon.attackChance;
            }

            currentWeapon = weapon;
            Instantiate(currentWeapon.weaponLook, weaponHandle);

            attackDamage += currentWeapon.attackDamage;
            attackSpeed += currentWeapon.attackSpeed;
            movementSpeed += currentWeapon.movementSpeed;
            criticalChance += currentWeapon.criticalChance;
            criticalDamage += currentWeapon.criticalDamage;
            attackChance += currentWeapon.attackChance;
        }

        public void EquipItem(Armor armor)
        {
            if (currentArmor != null)
            {
                maxHp -= armor.hpPoint;
                defencePoint -= armor.defencePoint;
                movementSpeed -= armor.movementSpeed;
                evasionPoint -= armor.evasionPoint;
            }

            currentArmor = armor;

            maxHp += currentArmor.hpPoint;
            defencePoint += currentArmor.defencePoint;
            movementSpeed += currentArmor.movementSpeed;
            evasionPoint += currentArmor.evasionPoint;
        }

        public void EquipItem(Helmet helmet)
        {
            if (currentHelmet != null)
            {
                maxHp -= helmet.hpPoint;
                defencePoint -= helmet.defencePoint;
                decreseCriticalDamage -= helmet.decreseCriticalDamage;
                evasionCritical -= helmet.evasionCritical;
            }

            currentHelmet = helmet;

            maxHp += currentHelmet.hpPoint;
            defencePoint += currentHelmet.defencePoint;
            decreseCriticalDamage += currentHelmet.decreseCriticalDamage;
            evasionCritical += currentHelmet.evasionCritical;
        }

        public void EquipItem(Pants pants)
        {
            if (currentPants != null)
            {
                maxHp -= pants.hpPoint;
                defencePoint -= pants.defencePoint;
                movementSpeed -= pants.movementSpeed;
            }

            currentPants = pants;

            maxHp += currentPants.hpPoint;
            defencePoint += currentPants.defencePoint;
            movementSpeed += currentPants.movementSpeed;
        }
        #endregion
    }
}