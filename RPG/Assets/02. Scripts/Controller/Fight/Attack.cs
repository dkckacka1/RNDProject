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

            // 1. 공격한다
            // 1-1. 회피했는지 맞았는지 체크



            //target.TakeDamage(attackDamage);
        }
        private bool AttackSuccess(BattleStatus character, IDamagedable target)
        {
            float chance = character.AttackChance * (1 - target.EvasionPoint);

            float random = Random.Range(0, 1f);

            if (chance < random)
            {
                return true;
            }

            return false;
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