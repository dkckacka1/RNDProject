using RPG.Character.Status;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    public class Regenerative_Armor : ArmorIncant
    {
        public Regenerative_Armor()
        {
            incantType = IncantType.prefix;
            itemType = EquipmentItemType.Armor;
            name = "재생의 ";
            addDesc = "초당 체력이 1만큼 재생됩니다.";
            minusDesc = "";
        }

        public override void IncantEquipment(Equipment equipment)
        {
        }

        public override void RemoveIncant(Equipment equipment)
        {
        }

        public override void PerSecEvent(BattleStatus status)
        {
            status.Heal(1);
        }
    }

}