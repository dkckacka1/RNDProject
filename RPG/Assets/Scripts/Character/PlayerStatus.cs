using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Battle.Character
{
    public class PlayerStatus : Status
    {
        public override void Initialize()
        {
            base.Initialize();
            EquipItem(currentArmor);
            EquipItem(currentHelmet);
            EquipItem(currentPants);
        }
    }
}