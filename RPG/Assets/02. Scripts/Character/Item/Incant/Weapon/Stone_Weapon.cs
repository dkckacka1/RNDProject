using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character.Status;
using RPG.Battle.Core;

namespace RPG.Character.Equipment
{
    public class Stone_Weapon : WeaponIncant
    {
        public override void IncantEquipment(Equipment equipment)
        {
        }

        public override void RemoveIncant(Equipment equipment)
        {
        }

        public override void AttackEvent(BattleStatus player, BattleStatus enemy)
        {
            var ability = BattleManager.ObjectPool.GetAbility(1);
            ability.InitAbility(player.transform);
        }
    }
}