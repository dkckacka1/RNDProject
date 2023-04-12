using RPG.Character.Status;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    public class Revenge_Armor : ArmorIncant
    {

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
            battleStatus.status.AttackDamage += 5;
            yield return new WaitForSeconds(time);
            battleStatus.status.AttackDamage -= 5;
        }
    }
}