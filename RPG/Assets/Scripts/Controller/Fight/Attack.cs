using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.Control;
using RPG.Character.Status;

namespace RPG.Battle.Fight
{
    public class Attack
    {
        public bool canAttack;
        public readonly float attackDelay = 0.3f;

        // Component
        Controller controller;
        Status status;
        IDamagedable target;

        public Attack(Controller controller, Status status)
        {
            this.controller = controller;
            this.status = status;
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
            controller.AttactAction();
        }

        public void TargetTakeDamage()
        {
            if (target == null)
            {
                Debug.Log($"{status.name}의 타겟이 없지만 AttackAnimEvent가 호출되었습니다.");
                return;
            }

            target.TakeDamage(status.attackDamage);
            if (target.IsDead)
            {
                controller.Target = null;
            }
        }

        public IEnumerator WaitAttackDelay()
        {
            yield return new WaitForSeconds(status.attackSpeed);

            canAttack = true;
        }

        public IEnumerator WaitAttackTime()
        {
            yield return new WaitForSeconds(attackDelay);

            TargetTakeDamage();
        }
    }
}