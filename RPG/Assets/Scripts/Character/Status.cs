using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.Core;
using RPG.Battle.Fight;
using RPG.Battle.Move;
using RPG.Battle.Control;
using RPG.Battle.UI;
using UnityEngine.AI;

namespace RPG.Battle.Character
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
            attackDamage += weapon.attackDamage;
            attackSpeed += weapon.attackSpeed;
            movementSpeed += weapon.movementSpeed;
            criticalChance += weapon.criticalChance;
            ciriticalDamage += weapon.criticalDamage;
            attackChance += weapon.attackChance;
        }

        public void EquipItem(Armor armor)
        {
            maxHp += armor.hpPoint;
            defencePoint += armor.defencePoint;
            movementSpeed += armor.movementSpeed;
            evasionPoint += armor.evasionPoint;
        }

        public void EquipItem(Helmet helmet)
        {
            maxHp += helmet.hpPoint;
            defencePoint += helmet.defencePoint;
            decreseCriticalDamage += helmet.decreseCriticalDamage;
            evasionCritical += helmet.evasionCritical;
        }

        public void EquipItem(Pants pants)
        {
            maxHp += pants.hpPoint;
            defencePoint += pants.defencePoint;
            movementSpeed += pants.movementSpeed;
        }
        #endregion
    }
}