using RPG.Character.Status;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    public class Cursed_Armor : ArmorIncant
    {
        public override void TakeDamageEvent(BattleStatus mine, BattleStatus whoHitMe)
        {
            whoHitMe.TakeDebuff(DebuffType.Curse, 5f);
        }
    }

}