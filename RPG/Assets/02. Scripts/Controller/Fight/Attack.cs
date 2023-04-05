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
        public int attackDamage;
        public float attackDelay = 1;
        public float attackAnimPoint = 1.2f;
        public float defaultAttackAnimLength;

        // Component
        Transform transform;
        IDamagedable target;
        

        public Attack(Transform transform)
        {
            this.transform = transform;
        }

        public void UpdateStatus(Controller controller)
        {
            RuntimeAnimatorController rc = controller.animator.runtimeAnimatorController;
            foreach (var item in rc.animationClips)
            {
                Debug.Log($"{item.name}의 길이 : {item.length}");
                if (item.name == "MeleeAttack_OneHanded")
                {
                    defaultAttackAnimLength = item.length;
                    break;
                }
            }

            controller.animator.SetFloat("AttackSpeed", controller.battleStatus.status.attackSpeed);
            attackDamage = controller.battleStatus.status.attackDamage;
            attackDelay = defaultAttackAnimLength / controller.battleStatus.status.attackSpeed;
            attackAnimPoint = attackDelay / 2.8f;
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
                Debug.Log($"타겟이 없지만 AttackAnimEvent가 호출되었습니다.");
                return;
            }

            target.TakeDamage(attackDamage);
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
    }
}