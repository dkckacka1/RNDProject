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
    public class BattleStatus : MonoBehaviour, IDamagedable
    {
        // Component
        [Header("UI")]
        public CharacterUI characterUI;

        [Header("Battle")]
        public int currentHp = 0;
        private bool isDead = false;

        [Header("Status")]
        public Status status;
        // Encapsulation
        public bool IsDead => isDead;
        public Transform transfrom => transform;
        public float AttackChance => status.attackChance;
        public float EvasionPoint => status.evasionPoint;
        public float DecreseCriticalDamage => status.decreseCriticalDamage;
        public float EvasionCritical => status.evasionCritical;
        public int DefencePoint => status.defencePoint;

        public int CurrentHp
        {
            get => currentHp;
            set
            {
                currentHp = Mathf.Clamp(value, 0, status.maxHp);
                if (characterUI != null)
                {
                    characterUI.UpdateHPUI(currentHp);
                }

                if (currentHp <= 0)
                {
                    Dead();
                }
            }
        }


        private void OnEnable()
        {
        }

        protected virtual void Start()
        {
        }

        protected virtual void LateUpdate()
        {

        }

        public virtual void Init()
        {
            currentHp = status.maxHp;
            isDead = false;
        }

        public virtual void Release()
        {
        }

        public void TakeDamage(int damage,DamagedType type)
        {
            if (isDead) return;

            switch (type)
            {
                case DamagedType.Normal:
                    CurrentHp -= damage;
                    characterUI.TakeDamageText(damage.ToString());
                    break;
                case DamagedType.Ciritical:
                    CurrentHp -= damage;
                    characterUI.TakeDamageText(damage.ToString());
                    break;
                case DamagedType.Evasion:
                    characterUI.TakeDamageText("MISS~");
                    break;
            }
        }

        public void Dead()
        {
            isDead = true;
        }

        public void Heal(int healPoint)
        {
            CurrentHp += healPoint;
        }
    }
}