using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    public class Hard_Armor : ArmorIncant
    {
        public Hard_Armor(int incantID) : base(incantID)
        {
            incantType = IncantType.prefix;
            itemType = EquipmentItemType.Armor;
            name = "단단한 ";
            addDesc = "방어력 +3 체력 +100";
            minusDesc = "이동속도 -1";

        }

        public override void IncantEquipment(Equipment equipment)
        {
            Armor armor = equipment as Armor;

            armor.DefencePoint += 3;
            armor.HpPoint += 100;
            armor.MovementSpeed -= 1;
        }

        public override void RemoveIncant(Equipment equipment)
        {
            Armor armor = equipment as Armor;

            armor.DefencePoint -= 3;
            armor.HpPoint -= 100;
            armor.MovementSpeed += 1;
        }
    }

}