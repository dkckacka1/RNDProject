using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.Core;

namespace RPG.Character.Status
{
    public class EnemyStatus : BattleStatus
    {
        public string enemyName;

        protected override void OnEnable()
        {
            base.OnEnable();
        }

        protected override void LateUpdate()
        {
            base.LateUpdate();
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public void SetStatus(EnemyData data)
        {
            enemyName = data.enemyName;
            maxHp = data.maxHp;
            attackDamage = data.attackDamage;
            attackRange = data.attackRange;
            attackSpeed = data.attackSpeed;
            criticalChance = data.criticalChance;
            criticalDamage = data.criticalDamage;
            attackChance = data.attackChance;

            defencePoint = data.defencePoint;
            evasionPoint = data.evasionPoint;
            decreseCriticalDamage = data.decreseCriticalDamage;
            evasionCritical = data.evasionCritical;

            movementSpeed = data.movementSpeed;
        }
    }
}