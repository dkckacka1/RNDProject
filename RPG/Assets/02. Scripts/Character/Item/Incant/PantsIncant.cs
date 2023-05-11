using RPG.Character.Status;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    public class PantsIncant : Incant
    {
        public int hpPoint;
        public int defencePoint;
        public float movementSpeed;

        public float skillCoolTime;

        public PantsIncant(PantsIncantData data) : base(data)
        {
            hpPoint = data.hpPoint;
            defencePoint = data.defencePoint;
            movementSpeed = data.movementSpeed;

    }

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
                if (returnStr == string.Empty)
                {
                    returnStr = $"ü��(+{hpPoint})";
                }
                else
                {
                    returnStr = string.Join("\n", returnStr, $"ü��(+{hpPoint})");
                }
            }

            if (defencePoint > 0)
            {
                if (returnStr == string.Empty)
                {
                    returnStr = $"����(+{defencePoint})";
                }
                else
                {
                    returnStr = string.Join("\n", returnStr, $"����(+{defencePoint})");
                }
            }

            if (movementSpeed > 0)
            {
                if (returnStr == string.Empty)
                {
                    returnStr = $"�̵��ӵ�(+{movementSpeed})";
                }
                else
                {
                    returnStr = string.Join("\n", returnStr, $"�̵��ӵ�(+{movementSpeed})");
                }
            }

            return returnStr;
        }

        public override string GetMinusDesc()
        {
            string returnStr = "";
            if (hpPoint < 0)
            {
                if (returnStr == string.Empty)
                {
                    returnStr = $"ü��({hpPoint})";
                }
                else
                {
                    returnStr = string.Join("\n", returnStr, $"ü��({hpPoint})");
                }
            }

            if (defencePoint < 0)
            {
                if (returnStr == string.Empty)
                {
                    returnStr = $"����({defencePoint})";
                }
                else
                {
                    returnStr = string.Join("\n", returnStr, $"����({defencePoint})");
                }
            }

            if (movementSpeed < 0)
            {
                if (returnStr == string.Empty)
                {
                    returnStr = $"�̵��ӵ�({movementSpeed})";
                }
                else
                {
                    returnStr = string.Join("\n", returnStr, $"�̵��ӵ�({movementSpeed})");
                }
            }

            return returnStr;
        }
    }
}