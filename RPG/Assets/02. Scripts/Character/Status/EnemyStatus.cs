using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.Core;

namespace RPG.Character.Status
{
    public class EnemyStatus : Status
    {
        public int enemyID;
        public int apperenceNum;
        public string enemyName;

        public void Init(EnemyData data)
        {
            enemyID = data.ID;
            apperenceNum = data.apperenceNum;

            enemyName = data.enemyName;
            MaxHp = data.maxHp;
            AttackDamage = data.attackDamage;
            AttackRange = data.attackRange;
            AttackSpeed = data.attackSpeed;
            CriticalChance = data.criticalChance;
            CriticalDamage = data.criticalDamage;
            AttackChance = data.attackChance;

            DefencePoint = data.defencePoint;
            EvasionPoint = data.evasionPoint;
            DecreseCriticalDamage = data.decreseCriticalDamage;
            EvasionCritical = data.evasionCritical;

            MovementSpeed = data.movementSpeed;
        }
    }
}