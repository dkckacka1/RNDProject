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
                Debug.Log($"타겟이 없지만 AttackAnimEvent가 호출되었습니다.");
                return;
            }

            // 0. 방어도 계산식
            float defenceAverage = 1 - (target.status.defencePoint / 100);
            if (defenceAverage >= 1)
                defenceAverage = 0.9f;

            // 1. 공격한다
            if (AttackChangeCalc(character, target))
                // 2. 공격이 적중한다.
            {
                if (AttackCriticalCalc(character, target))
                    // 3. 공격 치명타가 발생한다.
                {
                    int criticalDamage = (int)(character.status.attackDamage * (1 + character.status.criticalDamage));
                    target.TakeDamage(DamageCalc(criticalDamage,defenceAverage), DamagedType.Ciritical);
                }
                else
                    // 3. 공격 치명타가 발생하지 않는다.
                {
                    target.TakeDamage(DamageCalc(character.status.attackDamage, defenceAverage), DamagedType.Normal);
                }
            }
            else
                // 2. 공격이 실패한다.
            {
                target.TakeDamage(character.status.attackDamage, DamagedType.MISS);
            }



            //target.TakeDamage(attackDamage);
        }

        private int DamageCalc(int damage, float defenceAverage)
        {
            Debug.Log($"공격력 : {damage}\n" +
                $"방어율 : {defenceAverage * 100}%" +
                $"실제 데미지 수치 : {(int)(damage * defenceAverage)}");

            return (int)(damage * defenceAverage);
        }

        private bool AttackChangeCalc(BattleStatus character, BattleStatus target)
        {
            float chance = character.status.attackChance * (1 - target.status.evasionPoint);

            float random = Random.Range(0, 1f);

            //Debug.Log($"{character.name}가 공격하여 {target.name}을 타격했습니다.\n" +
            //    $"{character.name}의 적중률 : {character.status.attackChance * 100}%\n" +
            //    $"{target.name}의 회피율 : {target.status.evasionPoint * 100}%\n" +
            //    $"공격이 적중할 확률은 {chance * 100}% 입니다.\n" +
            //    $"주사위는 {random * 100}이 나왔습니다.");

            if (chance > random)
            {
                // 적중 성공
                return true;
            }

            // 적중 실패
            return false;
        }

        private bool AttackCriticalCalc(BattleStatus character, BattleStatus target)
        {
            float criticalChance = character.status.criticalChance * (1 - target.status.evasionCritical);

            float random = Random.Range(0, 1f);

    //        Debug.Log($"{character.name}가 공격하여 {target.name}을 타격했습니다.\n" +
    //$"{character.name}의 치명타 적중률 : {character.status.attackChance * 100}%\n" +
    //$"{target.name}의 치명타 회피율 : {target.status.evasionPoint * 100}%\n" +
    //$"공격이 치명타가 발생할 확률은 {criticalChance * 100}% 입니다.\n" +
    //$"치명타 데미지는 {(int)(character.status.attackDamage * (1 + character.status.criticalDamage))} 입니다.\n" +
    //$"주사위는 {random * 100}이 나왔습니다.");

            if (criticalChance > random)
            {
                // 치명타 적중 성공
                return true;
            }

            // 치명타 실패
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