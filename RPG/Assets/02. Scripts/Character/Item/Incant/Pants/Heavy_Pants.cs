using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    public class Heavy_Pants : PantsIncant
    {
        public Heavy_Pants(int incantID) : base(incantID)
        {
            incantType = IncantType.suffix;
            itemType = EquipmentItemType.Pants;
            name = "고중량의 ";
            addDesc = "방어력 +4";
            minusDesc = "이동속도 -3";
        }

        public override void IncantEquipment(Equipment equipment)
        {
            Pants pants = equipment as Pants;

            pants.defencePoint += 4;
            pants.movementSpeed -= 3;
        }

        public override void RemoveIncant(Equipment equipment)
        {
            Pants pants = equipment as Pants;

            pants.defencePoint -= 4;
            pants.movementSpeed += 3;
        }
    }

}