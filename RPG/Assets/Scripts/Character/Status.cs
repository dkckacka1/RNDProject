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
        public float criticalDamage = 0f;
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

        protected virtual void OnEnable()
        {
            Initialize();
        }

        protected virtual void Start()
        {
        }

        protected virtual void LateUpdate()
        {
            
        }

        public virtual void Initialize()
        {
            currentHp = maxHp;
            GetComponent<NavMeshAgent>().speed = movementSpeed;
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
        }

        public void Heal(int healPoint)
        {
            CurrentHp += healPoint;
        }
    }
}