using RPG.Character.Status;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    public abstract class PantsIncant : Incant
    {
        protected PantsIncant(int incantID) : base(incantID)
        {
        }

        public virtual void MoveEvent(BattleStatus player)
        {
            Debug.Log("MoveEvent is Nothing");
        }

        public virtual void ActiveSkill()
        {
            Debug.Log("Pants ActiveSkill is Nothing");
        }
    }
}