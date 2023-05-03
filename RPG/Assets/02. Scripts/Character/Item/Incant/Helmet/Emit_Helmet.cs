using RPG.Character.Status;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    public class Emit_Helmet : HelmetIncant
    {
        public Emit_Helmet()
        {
            skillCoolTime = 15f;
        }

        public override void ActiveSkill(BattleStatus player)
        {
            base.ActiveSkill(player);
        }
    }

}