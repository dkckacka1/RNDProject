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
        Status status;
        IDamagedable target;

        public Attack(Status status)
        {
            this.status = status;

            canAttack = true;
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
        }

        public void TargetTakeDamage()
        {
            if (target == null)
            {
                Debug.Log($"{status.name}�� Ÿ���� ������ AttackAnimEvent�� ȣ��Ǿ����ϴ�.");
                return;
            }

            target.TakeDamage(status.attackDamage);
            // TODO : ���� �ʿ� ��Ʈ�ѷ��� ������� �ʱ�
            //if (target.IsDead)
            //{
            //    controller.target = null;
            //}
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