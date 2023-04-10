using RPG.Character.Status;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    public abstract class PantsIncant : Incant
    {
        public float skillCoolTime;
        public virtual void MoveEvent(BattleStatus player)
        {
            Debug.Log("MoveEvent is Nothing");
        }

        public virtual void ActiveSkill(BattleStatus player)
        {
            Debug.Log("Pants ActiveSkill is Nothing");
        }
    }
}