using RPG.Character.Status;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    public abstract class ArmorIncant : Incant
    {
        public virtual void PerSecEvent(BattleStatus status)
        {
            Debug.Log("PerSecEvent is Nothing");
        }

        public virtual void TakeDamageEvent(BattleStatus character, BattleStatus target)
        {
            Debug.Log("TakeDamageEvent is Nothing");
        }
    }
}