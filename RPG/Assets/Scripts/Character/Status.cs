using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Fight;
using RPG.Move;
using RPG.Control;
using RPG.UI;

namespace RPG.Character
{
    public class Status : MonoBehaviour, IDamagedable
    {
        [Header("Health")]
        [SerializeField] public int maxHp = 100;
        [SerializeField] public int currentHp = 100;
        [SerializeField] public HPBarUI hpBarUI;
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

        public void SetHpBar()
        {
            hpBarUI = Instantiate(hpBarUI, BattleManager.GetInstance().hpBarCanvas.transform);
            hpBarUI.SetHpSlider(maxHp, CurrentHp);
        }

        public void SetHpBarPosition(Vector3 position)
        {
            hpBarUI.hpSlider.transform.position = Camera.main.WorldToScreenPoint(position);
        }

        public void TakeDamage(int damage)
        {
            if (isDead) return;

            CurrentHp -= damage;
        }

        public void Dead()
        {
            isDead = true;
            GetComponent<Controller>().DeadAction();
        }

        public void Heal(int healPoint)
        {
            CurrentHp += healPoint;
        }
    }

}