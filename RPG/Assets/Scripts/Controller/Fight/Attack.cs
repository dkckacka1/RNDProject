using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.Control;
using RPG.Character.Status;

namespace RPG.Battle.Fight
{
    public class Attack
    {
        public bool canAttack = true;
        public float attackDelay = 0.3f;
        public float attackAnimPoint = 0.3f;

        // Component
        Transform transform;
        Status status;
        IDamagedable target;

        public Attack(Transform transform, Status status)
        {
            this.transform = transform;
            this.status = status;
            attackDelay = CalcAttackDelay(status.attackSpeed);
        }

        public void SetTarget(IDamagedable target)
        {
            this.target = target;
        }

        public void AttackTarget()
        {
            canAttack = false;
        }

        public void TargetTakeDamage()
        {
            if (target.IsDead) return;
            if (target == null)
            {
                Debug.Log($"{status.name}�� Ÿ���� ������ AttackAnimEvent�� ȣ��Ǿ����ϴ�.");
                return;
            }

            target.TakeDamage(status.attackDamage);
        }

        public IEnumerator WaitAttackDelay()
        {
            yield return new WaitForSeconds(attackDelay);
            canAttack = true;
        }

        public IEnumerator WaitAttackTime()
        {
            yield return new WaitForSeconds(attackAnimPoint);
            TargetTakeDamage();
        }

        float CalcAttackDelay(float attackSpeed)
        {
            return (1 / attackSpeed);
        }
    }
}