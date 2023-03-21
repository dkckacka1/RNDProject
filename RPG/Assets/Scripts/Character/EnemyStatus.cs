using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.Core;

namespace RPG.Character.Status
{
    public class EnemyStatus : Status
    {
        public string enemyName;

        protected override void OnEnable()
        {
            base.OnEnable();
        }

        protected override void LateUpdate()
        {
            base.LateUpdate();
            SetHpBarPosition(transform.position + hpBarUIOffset);
        }

        public override void Initialize()
        {
            base.Initialize();
            SetHpBar();
        }

        public void SetHpBar()
        {
            hpBarUI = Instantiate(hpBarUI, BattleManager.GetInstance().hpBarCanvas.transform);
            hpBarUI.SetHpSlider(maxHp, CurrentHp);
        }

        public void SetHpBarPosition(Vector3 position)
        {
            hpBarUI.transform.transform.position = Camera.main.WorldToScreenPoint(position);
            //hpBarUI.hpSlider.transform.position = Camera.main.WorldToScreenPoint(position);
        }


        public void SetEnemyData(EnemyData data)
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