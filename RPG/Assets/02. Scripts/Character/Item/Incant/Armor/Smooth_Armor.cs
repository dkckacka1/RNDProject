using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    public class Smooth_Armor : ArmorIncant
    {
        public Smooth_Armor(int incantID) : base(incantID)
        {
            incantType = IncantType.suffix;
            itemType = EquipmentItemType.Armor;
            name = "�Ų� ";
            addDesc = "�̵��ӵ� +0.5";
            minusDesc = "���� -1";
        }

        public override void IncantEquipment(Equipment equipment)
        {
            Armor armor = equipment as Armor;

            armor.MovementSpeed += 0.5f;
            armor.DefencePoint -= 1;
        }

        public override void RemoveIncant(Equipment equipment)
        {
            Armor armor = equipment as Armor;

            armor.MovementSpeed -= 0.5f;
            armor.DefencePoint += 1;
        }
    } 
}
