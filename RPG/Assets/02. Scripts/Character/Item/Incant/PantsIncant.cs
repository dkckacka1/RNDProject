using RPG.Character.Status;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    public abstract class PantsIncant : Incant
    {
        public int hpPoint;
        public int defencePoint;
        public float movementSpeed;

        public float skillCoolTime;
        public virtual void MoveEvent(BattleStatus player)
        {
            Debug.Log("MoveEvent is Nothing");
        }

        public virtual void ActiveSkill(BattleStatus player)
        {
            Debug.Log("Pants ActiveSkill is Nothing");
        }

        public override string GetAddDesc()
        {
            string returnStr = "";
            if (hpPoint > 0)
            {
                returnStr += $"체력(+{hpPoint})";
            }

            if (defencePoint > 0)
            {
                returnStr += $"방어력(+{defencePoint})";
            }

            if (movementSpeed > 0)
            {
                returnStr += $"이동속도(+{movementSpeed})";
            }

            return returnStr;
        }

        public override string GetMinusDesc()
        {
            string returnStr = "";
            if (hpPoint < 0)
            {
                returnStr += $"체력({hpPoint})";
            }

            if (defencePoint < 0)
            {
                returnStr += $"방어력({defencePoint})";
            }

            if (movementSpeed < 0)
            {
                returnStr += $"이동속도({movementSpeed})";
            }

            return returnStr;
        }
    }
}