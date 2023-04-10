using RPG.Character.Status;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    public class Regenerative_Helmet : HelmetIncant
    {
        public Regenerative_Helmet()
        {
            incantType = IncantType.suffix;
            itemType = EquipmentItemType.Helmet;
            IncantName = "재생의 ";
            addDesc = "사용시 체력을 100 회복하는 액티브 스킬 부여";
            minusDesc = "";
            skillCoolTime = 20f;
        }

        public override void IncantEquipment(Equipment equipment)
        {
        }

        public override void RemoveIncant(Equipment equipment)
        {
        }

        public override void ActiveSkill(BattleStatus player)
        {
            player.Heal(100);
        }
    }
}
