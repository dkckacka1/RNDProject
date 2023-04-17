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
                returnStr += $"ü��(+{hpPoint})";
            }

            if (defencePoint > 0)
            {
                returnStr += $"����(+{defencePoint})";
            }

            if (movementSpeed > 0)
            {
                returnStr += $"�̵� �ӵ�(+{movementSpeed})";
            }

            if (evasionPoint > 0)
            {
                returnStr += $"ȸ����(+{evasionPoint * 100}%)";
            }


            return returnStr;
        }

        public override string GetMinusDesc()
        {
            string returnStr = "";

            if (hpPoint < 0)
            {
                returnStr += $"ü��(-{hpPoint})";
            }

            if (defencePoint < 0)
            {
                returnStr += $"����(-{defencePoint})";
            }

            if (movementSpeed < 0)
            {
                returnStr += $"�̵� �ӵ�(-{movementSpeed})";
            }

            if (evasionPoint < 0)
            {
                returnStr += $"ȸ����(-{evasionPoint * 100}%)";
            }


            return returnStr;

        }
    }
}