using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Status
{
    public class PlayerStatus : Status
    {
        public override void Initialize()
        {
            base.Initialize();
            EquipItem(currentArmor);
            EquipItem(currentHelmet);
            EquipItem(currentPants);
            currentHp = maxHp;
        }
    }
}