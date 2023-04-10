using RPG.Character.Status;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    public abstract class HelmetIncant : Incant
    {
        public float skillCoolTime;

        public virtual void criticalAttackEvent(BattleStatus player, BattleStatus enemy)
        {
            Debug.Log("criticalAttackEvent is Nothing");
        }

        public virtual void ActiveSkill(BattleStatus player)
        {
            Debug.Log("Helmet ActiveSkill is Nothing");
        }
    }
}