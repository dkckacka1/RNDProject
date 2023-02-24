using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Fight;
using RPG.Move;
using RPG.Control;

namespace RPG.Character
{
    public class Status : MonoBehaviour, IDamagedable, ITargetable
    {
        [Header("Health")]
        [SerializeField] public int maxHp = 100;
        [SerializeField] public int currentHp = 100;
        private bool isDead = false;

        [Header("Movement")]
        [SerializeField] public float moveSpeed = 3f;

        [Header("Attack")]
        [SerializeField] public float attackRange = 1f;
        [SerializeField] public int attackDamage = 10;
        [SerializeField] public float attackDelay = 1f;

        // Encapsulation
        public bool IsDead { get => isDead; set => isDead = value; }
        public int CurrentHp 
        {
            get => currentHp; 
            set
            {
                currentHp = Mathf.Clamp(value, 0, maxHp);
                if (currentHp <= 0)
                {
                    Dead();
                }
            }
        }

        public void TakeDamage(int damage)
        {
            if (isDead) return;

            CurrentHp -= damage;
        }

        public void Dead()
        {
            GetComponent<Controller>().DeadAction();
        }

        public void Heal(int healPoint)
        {
            CurrentHp += healPoint;
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }
    }

}