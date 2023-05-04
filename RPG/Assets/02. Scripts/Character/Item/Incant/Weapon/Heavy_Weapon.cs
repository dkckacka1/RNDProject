using RPG.Character.Status;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    public class Heavy_Weapon : WeaponIncant
    {
        public Heavy_Weapon(IncantData data) : base(data)
        {
            attackDamage = 30;
            attackSpeed = -1;
        }
    }
}
