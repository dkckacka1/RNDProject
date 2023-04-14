using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using RPG.Battle.Control;
using RPG.Character.Status;
using RPG.Battle.Event;

namespace RPG.Battle.Fight
{
    public class Attack
    {
        public bool canAttack = true;
        public bool isAttack = false;
        public float attackDelay = 1;
        public float attackAnimPoint = 1.2f;
        public float defaultAttackAnimLength;

        // AttackEvent
        AttackEvent attackEvent;
        CriticalAttackEvent criticalAttackEvent;

        // Component
        BattleStatus character;
        BattleStatus target;
        public Attack(BattleStatus character)
        {
            this.character = character;
            attackEvent = new AttackEvent();
            criticalAttackEvent = new CriticalAttackEvent();
        }

        public void AddAttackEvent(UnityAction<BattleStatus, BattleStatus> action)
        {
            attackEvent.AddListener(action);
        }

        public void AddCriticalAttackEvent(UnityAction<BattleStatus, BattleStatus> action)
        {
            criticalAttackEvent.AddListener(action);
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
            float defenceAverage = 1 - (target.status.DefencePoint / 100);
            if (defenceAverage >= 1)
                defenceAverage = 0.9f;

            // 1. �����Ѵ�
            if (AttackChangeCalc(character, target))
                // 2. ������ �����Ѵ�.
            {
                attackEvent.Invoke(character, target);
                if (AttackCriticalCalc(character, target))
                    // 3. ���� ġ��Ÿ�� �߻��Ѵ�.
                {
                    criticalAttackEvent.Invoke(character, target);
                    int criticalDamage = (int)(character.status.AttackDamage * (1 + character.status.CriticalDamage));
                    target.TakeDamage(DamageCalc(criticalDamage,defenceAverage), DamagedType.Ciritical);
                }
                else
                    // 3. ���� ġ��Ÿ�� �߻����� �ʴ´�.
                {
                    target.TakeDamage(DamageCalc(character.status.AttackDamage, defenceAverage), DamagedType.Normal);
                }
            }
            else
                // 2. ������ �����Ѵ�.
            {
                target.TakeDamage(character.status.AttackDamage, DamagedType.MISS);
            }



            //target.TakeDamage(attackDamage);
        }

        private int DamageCalc(int damage, float defenceAverage)
        {
            //Debug.Log($"���ݷ� : {damage}\n" +
            //    $"����� : {defenceAverage * 100}%" +
            //    $"���� ������ ��ġ : {(int)(damage * defenceAverage)}");

            return (int)(damage * defenceAverage);
        }

        private bool AttackChangeCalc(BattleStatus character, BattleStatus target)
        {
            float chance = character.status.AttackChance * (1 - target.status.EvasionPoint);

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
            float criticalChance = character.status.CriticalChance * (1 - target.status.EvasionCritical);

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
            isAttack = true;
            yield return new WaitForSeconds(attackDelay);
            canAttack = true;
            isAttack = false;
        }

        public IEnumerator WaitAttackTime()
        {
            yield return new WaitForSeconds(attackAnimPoint);
            TargetTakeDamage();
        }
    }
}