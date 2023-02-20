using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character;
using RPG.Control;

namespace RPG.Fight
{
    public class Attack : MonoBehaviour
    {
        public bool canAttack = true;

        // Component
        Stats stats;
        IDamagedable target;


        private void Awake()
        {
            stats = GetComponent<Stats>();
        }

        public void AttackTarget(IDamagedable target)
        {
            if (target.IsDead) return;

            this.target = target;
            canAttack = false;
            GetComponent<Controller>().animator.SetTrigger("Attack");

            StartCoroutine(AttackDelayCalculate());
        }

        IEnumerator AttackDelayCalculate()
        {
            yield return new WaitForSeconds(stats.attackDelay);

            canAttack = true;
        }

        public void AttackAnimEvent()
        {
            target.TakeDamage(stats.attackDamage);
        }
    }
}