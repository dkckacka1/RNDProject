using RPG.Character.Status;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    public class Sharpness_Weapon : WeaponIncant
    {
        public Sharpness_Weapon(IncantData data) : base(data)
        {
            attackDamage = 10;
        }
    }
}