using RPG.Character.Status;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    public class Regenerative_Armor : ArmorIncant
    {
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