using RPG.Character.Status;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    public abstract class ArmorIncant : Incant
    {
        public int hpPoint;
        public int defencePoint;
        public float movementSpeed;
        public float evasionPoint;

        public virtual void PerSecEvent(BattleStatus status)
        {
            Debug.Log("PerSecEvent is Nothing");
        }

        public virtual void TakeDamageEvent(BattleStatus character, BattleStatus target)
        {
            Debug.Log("TakeDamageEvent is Nothing");
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
                returnStr += $"이동 속도(+{movementSpeed})";
            }

            if (evasionPoint > 0)
            {
                returnStr += $"회피율(+{evasionPoint * 100}%)";
            }


            return returnStr;
        }

        public override string GetMinusDesc()
        {
            string returnStr = "";

            if (hpPoint < 0)
            {
                returnStr += $"체력(-{hpPoint})";
            }

            if (defencePoint < 0)
            {
                returnStr += $"방어력(-{defencePoint})";
            }

            if (movementSpeed < 0)
            {
                returnStr += $"이동 속도(-{movementSpeed})";
            }

            if (evasionPoint < 0)
            {
                returnStr += $"회피율(-{evasionPoint * 100}%)";
            }


            return returnStr;

        }
    }
}