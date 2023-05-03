using RPG.Battle.Core;
using RPG.Character.Status;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    public class Emit_Helmet : HelmetIncant
    {
        public Emit_Helmet()
        {
            skillCoolTime = 1f;
        }

        public override void ActiveSkill(BattleStatus player)
        {
            var ability = BattleManager.ObjectPool.GetAbility(7);
            ability.InitAbility(player.transform, hitAction, chainAction);
        }

        private void chainAction(BattleStatus target)
        {
            Debug.Log("Chain");
            target.TakeDamage(20);
        }

        private void hitAction(BattleStatus target)
        {
            Debug.Log("hit Effect ���");
            target.TakeDamage(30);
        }
    }

}