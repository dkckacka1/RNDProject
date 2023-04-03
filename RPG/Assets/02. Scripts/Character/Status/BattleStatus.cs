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
        public bool IsDead { get => isDead; set => isDead = value; }
        public Transform transfrom { get => transform; }

        public int CurrentHp
        {
            get => currentHp;
            set
            {
                currentHp = Mathf.Clamp(value, 0, status.maxHp);
                if (characterUI != null)
                {
                    characterUI.ChangeHPUI(currentHp);
                }

                if (currentHp <= 0)
                {
                    Dead();
                }
            }
        }

        protected virtual void OnEnable()
        {
            UpdateStatus();
        }

        protected virtual void Start()
        {
        }

        protected virtual void LateUpdate()
        {

        }

        public virtual void UpdateStatus()
        {
            currentHp = status.maxHp;
            isDead = false;
        }

        public void TakeDamage(int damage)
        {
            if (isDead) return;

            CurrentHp -= damage;
            characterUI.TakeDamageText(damage);
        }

        public void Dead()
        {
            isDead = true;
            //GetComponent<NavMeshAgent>().enabled = false;
            //characterUI.RemoveUI(4.5f);
            //Destroy(gameObject,5f);
        }

        public void Heal(int healPoint)
        {
            CurrentHp += healPoint;
        }
    }
}