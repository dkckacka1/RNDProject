using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    public class Quick_Pants : PantsIncant
    {
        public Quick_Pants()
        {
            incantType = IncantType.prefix;
            itemType = EquipmentItemType.Pants;
            name = "재빠른 ";
            addDesc = "이동속도 +1.5";
        }

        public override void IncantEquipment(Equipment equipment)
        {
            Pants pants = equipment as Pants;

            pants.movementSpeed += 1.5f;
        }

        public override void RemoveIncant(Equipment equipment)
        {
            Pants pants = equipment as Pants;

            pants.movementSpeed -= 1.5f;
        }
    }
}