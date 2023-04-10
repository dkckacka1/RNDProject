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
            IncantName = "����� ";
            addDesc = "���� ü���� 100 ȸ���ϴ� ��Ƽ�� ��ų �ο�";
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
