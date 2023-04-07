using RPG.Character.Status;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    public class Revenge_Armor : ArmorIncant
    {
        public Revenge_Armor()
        {
            incantType = IncantType.suffix;
            itemType = EquipmentItemType.Armor;
            name = "���� ";
            addDesc = "�ǰݽ� 2�ʰ� ������ 5 ���(��ø ����)";
            minusDesc = "";
        }

        public override void IncantEquipment(Equipment equipment)
        {
        }

        public override void RemoveIncant(Equipment equipment)
        {
        }

        public override void TakeDamageEvent(BattleStatus status)
        {
            status.StartCoroutine(Revenge(status, 2f));
        }

        public IEnumerator Revenge(BattleStatus battleStatus, float time)
        {
            battleStatus.status.attackDamage += 5;
            yield return new WaitForSeconds(time);
            battleStatus.status.attackDamage -= 5;
        }
    }
}