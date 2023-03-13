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
        [SerializeField] public int maxHp = 100;
        [SerializeField] public int currentHp = 100;
        [SerializeField] public HPBarUI hpBarUI;
        [SerializeField] public Vector3 hpBarUIOffset = new Vector3(0f, 1f,0f);
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
                hpBarUI.SetHpSlider(currentHp);
                if (currentHp <= 0)
                {
                    Dead();
                    Destroy(gameObject,10f);
                }
            }
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
    }

}