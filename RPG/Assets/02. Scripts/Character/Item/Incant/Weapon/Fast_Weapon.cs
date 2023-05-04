using RPG.Character.Status;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    public class Fast_Weapon : WeaponIncant
    {
        public Fast_Weapon(IncantData data) : base(data)
        {
            attackSpeed = 1;
            attackDamage = -1;
        }
    }
}