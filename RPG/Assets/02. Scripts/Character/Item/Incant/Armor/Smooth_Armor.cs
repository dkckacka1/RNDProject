using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    public class Smooth_Armor : ArmorIncant
    {
        public Smooth_Armor()
        {
            incantType = IncantType.suffix;
            itemType = EquipmentItemType.Armor;
            name = "매끈 ";
            addDesc = "이동속도 +0.5";
            minusDesc = "방어력 -1";
        }

        public override void IncantEquipment(Equipment equipment)
        {
            Armor armor = equipment as Armor;

            armor.movementSpeed += 0.5f;
            armor.defencePoint -= 1;
        }

        public override void RemoveIncant(Equipment equipment)
        {
            Armor armor = equipment as Armor;

            armor.movementSpeed -= 0.5f;
            armor.defencePoint += 1;
        }
    } 
}
