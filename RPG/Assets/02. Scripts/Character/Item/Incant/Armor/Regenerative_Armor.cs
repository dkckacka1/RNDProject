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
            name = "����� ";
            addDesc = "�ʴ� ü���� 1��ŭ ����˴ϴ�.";
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