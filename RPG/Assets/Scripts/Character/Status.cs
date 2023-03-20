using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Battle.Core;
using RPG.Battle.Fight;
using RPG.Battle.Move;
using RPG.Battle.Control;
using RPG.Battle.UI;
using RPG.Character.Equipment;

namespace RPG.Character.Status
{
    public class Status : MonoBehaviour, IDamagedable
    {
        [Header("Health")]
        public int maxHp = 0;
        public int currentHp = 0;
        public HPBarUI hpBarUI;
        public Vector3 hpBarUIOffset = new Vector3(0f, 1f,0f);
        private bool isDead = false;

        [Header("Attack")]
        public float attackRange = 0f;
        public int attackDamage = 0;
        public float attackSpeed = 0f;
        public float criticalChance = 0f;
        public float ciriticalDamage = 0f;
        public float attackChance = 0f;

        [Header("Defence")]
        public int defencePoint = 0;
        public float evasionPoint = 0f;
        public float decreseCriticalDamage = 0f;
        public float evasionCritical = 0f;

        [Header("Movement")]
        public float movementSpeed = 0f;

        [Header("Equipment")]
        public Transform weaponHandle;
        public Weapon currentWeapon;
        public Armor currentArmor;
        public Helmet currentHelmet;
        public Pants currentPants;

        // Encapsulation
        public bool IsDead { get => isDead; set => isDead = value; }
        public int CurrentHp 
        {
            get => currentHp; 
            set
            {
                currentHp = Mathf.Clamp(value, 0, maxHp);
                hpBarUI.SetHpSlider(currentHp);
                if (currentHp <= 0)
                {
                    Dead();
                    Destroy(gameObject,10f);
                }
            }
        }

        public virtual void Initialize()
        {
            EquipItem(currentWeapon);
        }

        private void LateUpdate()
        {
            SetHpBarPosition(transform.position + hpBarUIOffset);
        }

        public void SetHpBar()
        {
            hpBarUI = Instantiate(hpBarUI, BattleManager.GetInstance().hpBarCanvas.transform);
            hpBarUI.SetHpSlider(maxHp, CurrentHp);
        }

        public void SetHpBarPosition(Vector3 position)
        {
            hpBarUI.transform.transform.position = Camera.main.WorldToScreenPoint(position);
            //hpBarUI.hpSlider.transform.position = Camera.main.WorldToScreenPoint(position);
        }

        public void TakeDamage(int damage)
        {
            if (isDead) return;

            CurrentHp -= damage;
        }

        public void Dead()
        {
            isDead = true;
            GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<Controller>().DeadAction();
        }

        public void Heal(int healPoint)
        {
            CurrentHp += healPoint;
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
                ciriticalDamage -= currentWeapon.criticalDamage;
                attackChance -= currentWeapon.attackChance;
            }

            currentWeapon = weapon;
            Instantiate(currentWeapon.weaponLook, weaponHandle);

            attackDamage += currentWeapon.attackDamage;
            attackSpeed += currentWeapon.attackSpeed;
            movementSpeed += currentWeapon.movementSpeed;
            criticalChance += currentWeapon.criticalChance;
            ciriticalDamage += currentWeapon.criticalDamage;
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