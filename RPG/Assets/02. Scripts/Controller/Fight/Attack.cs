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
        public float attackDelay = 1;
        public float attackAnimPoint = 1.2f;
        public float defaultAttackAnimLength;

        // Component
        Transform transform;
        BattleStatus character;
        BattleStatus target;
        

        public Attack(Transform transform,BattleStatus character)
        {
            this.transform = transform;
            this.character = character;
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
            attackDelay = defaultAttackAnimLength / controller.battleStatus.status.attackSpeed;
            attackAnimPoint = attackDelay / 2.8f;
        }

        public void SetTarget(BattleStatus target)
        {
            this.target = target;
        }

        public void AttackTarget()
        {
            canAttack = false;
        }

        public void TargetTakeDamage()
        {
            if (target.isDead) return;
            if (target == null)
            {
                Debug.Log($"Ÿ���� ������ AttackAnimEvent�� ȣ��Ǿ����ϴ�.");
                return;
            }

            // 0. �� ����
            float defenceAverage = 1 - (target.status.defencePoint / 100);
            if (defenceAverage >= 1)
                defenceAverage = 0.9f;

            // 1. �����Ѵ�
            if (AttackChangeCalc(character, target))
                // 2. ������ �����Ѵ�.
            {
                if (AttackCriticalCalc(character, target))
                    // 3. ���� ġ��Ÿ�� �߻��Ѵ�.
                {
                    int criticalDamage = (int)(character.status.attackDamage * (1 + character.status.criticalDamage));
                    target.TakeDamage(DamageCalc(criticalDamage,defenceAverage), DamagedType.Ciritical);
                }
                else
                    // 3. ���� ġ��Ÿ�� �߻����� �ʴ´�.
                {
                    target.TakeDamage(DamageCalc(character.status.attackDamage, defenceAverage), DamagedType.Normal);
                }
            }
            else
                // 2. ������ �����Ѵ�.
            {
                target.TakeDamage(character.status.attackDamage, DamagedType.MISS);
            }



            //target.TakeDamage(attackDamage);
        }

        private int DamageCalc(int damage, float defenceAverage)
        {
            Debug.Log($"���ݷ� : {damage}\n" +
                $"����� : {defenceAverage * 100}%" +
                $"���� ������ ��ġ : {(int)(damage * defenceAverage)}");

            return (int)(damage * defenceAverage);
        }

        private bool AttackChangeCalc(BattleStatus character, BattleStatus target)
        {
            float chance = character.status.attackChance * (1 - target.status.evasionPoint);

            float random = Random.Range(0, 1f);

            //Debug.Log($"{character.name}�� �����Ͽ� {target.name}�� Ÿ���߽��ϴ�.\n" +
            //    $"{character.name}�� ���߷� : {character.status.attackChance * 100}%\n" +
            //    $"{target.name}�� ȸ���� : {target.status.evasionPoint * 100}%\n" +
            //    $"������ ������ Ȯ���� {chance * 100}% �Դϴ�.\n" +
            //    $"�ֻ����� {random * 100}�� ���Խ��ϴ�.");

            if (chance > random)
            {
                // ���� ����
                return true;
            }

            // ���� ����
            return false;
        }

        private bool AttackCriticalCalc(BattleStatus character, BattleStatus target)
        {
            float criticalChance = character.status.criticalChance * (1 - target.status.evasionCritical);

            float random = Random.Range(0, 1f);

    //        Debug.Log($"{character.name}�� �����Ͽ� {target.name}�� Ÿ���߽��ϴ�.\n" +
    //$"{character.name}�� ġ��Ÿ ���߷� : {character.status.attackChance * 100}%\n" +
    //$"{target.name}�� ġ��Ÿ ȸ���� : {target.status.evasionPoint * 100}%\n" +
    //$"������ ġ��Ÿ�� �߻��� Ȯ���� {criticalChance * 100}% �Դϴ�.\n" +
    //$"ġ��Ÿ �������� {(int)(character.status.attackDamage * (1 + character.status.criticalDamage))} �Դϴ�.\n" +
    //$"�ֻ����� {random * 100}�� ���Խ��ϴ�.");

            if (criticalChance > random)
            {
                // ġ��Ÿ ���� ����
                return true;
            }

            // ġ��Ÿ ����
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