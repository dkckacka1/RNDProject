using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character.Status;

namespace RPG.Character.Equipment
{
    public class Dull_Weapon : WeaponIncant
    {
        public override void IncantEquipment(Equipment equipment)
        {
        }

        public override void RemoveIncant(Equipment equipment)
        {
        }

        public override void AttackEvent(BattleStatus player, BattleStatus enemy)
        {
            if (MyUtility.ProbailityCalc(70f, 0f, 100f))
            {
                enemy.TakeDebuff(DebuffType.Stern, 2f);
            }
        }
    }

}