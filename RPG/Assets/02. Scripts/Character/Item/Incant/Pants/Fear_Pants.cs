using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character.Status;
using RPG.Battle.Core;

namespace RPG.Character.Equipment
{
    public class Fear_Pants : PantsIncant
    {
        public Fear_Pants()
        {
            skillCoolTime = 2f;
        }

        public override void IncantEquipment(Equipment equipment)
        {

        }

        public override void RemoveIncant(Equipment equipment)
        {
        }

        public override void ActiveSkill(BattleStatus player)
        {
            var ability = BattleManager.ObjectPool.GetAbility(2, player.transform, Fear);
        }

        public void Fear(BattleStatus character)
        {
            if (character.status is EnemyStatus)
            {
                character.TakeDebuff(DebuffType.Fear, 4f);
            }
        }
    }
}