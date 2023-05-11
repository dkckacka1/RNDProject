using RPG.Character.Status;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    public class ArmorIncant : Incant
    {
        public int hpPoint;
        public int defencePoint;
        public float movementSpeed;
        public float evasionPoint;

        public ArmorIncant(ArmorIncantData data) : base(data)
        {
            hpPoint = data.hpPoint;
            defencePoint = data.defencePoint;
            movementSpeed = data.movementSpeed;
            evasionPoint = data.evasionPoint;
        }

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
            string returnStr = string.Empty;

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
                    returnStr = $"�̵� �ӵ�(+{movementSpeed})";
                }
                else
                {
                    returnStr = string.Join("\n", returnStr, $"�̵� �ӵ�(+{movementSpeed})");
                }
            }

            if (evasionPoint > 0)
            {
                if (returnStr == string.Empty)
                {
                    returnStr = $"ȸ����(+{evasionPoint * 100}%)";
                }
                else
                {
                    returnStr = string.Join("\n", returnStr, $"ȸ����(+{evasionPoint * 100}%)");
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
                    returnStr = $"�̵� �ӵ�({movementSpeed})";
                }
                else
                {
                    returnStr = string.Join("\n", returnStr, $"�̵� �ӵ�({movementSpeed})");
                }
            }

            if (evasionPoint < 0)
            {
                if (returnStr == string.Empty)
                {
                    returnStr = $"ȸ����({evasionPoint * 100}%)";
                }
                else
                {
                    returnStr = string.Join("\n", returnStr, $"ȸ����({evasionPoint * 100}%)");
                }
            }


            return returnStr;

        }
    }
}