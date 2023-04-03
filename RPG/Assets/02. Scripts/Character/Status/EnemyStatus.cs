using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.Core;

namespace RPG.Character.Status
{
    public class EnemyStatus : Status
    {
        public string enemyName;

        public void Init(EnemyData data)
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