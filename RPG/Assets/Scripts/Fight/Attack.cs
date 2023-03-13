using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.Character;
using RPG.Battle.Control;

namespace RPG.Battle.Fight
{
    public class Attack : MonoBehaviour
    {
        public bool canAttack;

        // Component
        Controller controller;
        Status status;
        IDamagedable target;


        private void Awake()
        {
            controller = GetComponent<Controller>();
            status = GetComponent<Status>();
        }

        public void SetTarget(IDamagedable target)
        {
            this.target = target;
        }

        public void AttackTarget()
        {
            if (target.IsDead) return;
            if (!canAttack) return;

            canAttack = false;
            controller.transform.LookAt(controller.transform);
            controller.animator.SetTrigger("Attack");
            StartCoroutine(WaitAttackDelay());
        }

        public void AttackAnimEvent()
        {
            target.TakeDamage(status.attackDamage);
            if (target.IsDead)
            {
                controller.Target = null;
            }
        }


        IEnumerator WaitAttackDelay()
        {
            yield return new WaitForSeconds(status.attackDelay);

            canAttack = true;
        }
    }
}