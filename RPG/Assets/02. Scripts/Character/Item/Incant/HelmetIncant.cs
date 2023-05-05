using RPG.Character.Status;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    public class HelmetIncant : Incant
    {
        public int hpPoint;
        public int defencePoint;
        public float decreseCriticalDamage;
        public float evasionCritical;

        public float skillCoolTime;

        public HelmetIncant(HelmetIncantData data) : base(data)
        {
            hpPoint = data.hpPoint;
            defencePoint = data.defencePoint;
            decreseCriticalDamage = data.decreseCriticalDamage;
            evasionCritical = data.evasionCritical;
        }

        public virtual void criticalAttackEvent(BattleStatus player, BattleStatus enemy)
        {
            Debug.Log("criticalAttackEvent is Nothing");
        }

        public virtual void ActiveSkill(BattleStatus player)
        {
            Debug.Log("Helmet ActiveSkill is Nothing");
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

            if (decreseCriticalDamage > 0)
            {
                returnStr += $"치명타데미지감소(+{decreseCriticalDamage * 100}%)";
            }

            if (evasionCritical > 0)
            {
                returnStr += $"치명타회피율(+{evasionCritical * 100}%)";
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

            if (decreseCriticalDamage < 0)
            {
                returnStr += $"치명타데미지감소({decreseCriticalDamage * 100}%)";
            }

            if (evasionCritical < 0)
            {
                returnStr += $"치명타회피율({evasionCritical * 100}%)";
            }

            return returnStr;
        }
    }
}