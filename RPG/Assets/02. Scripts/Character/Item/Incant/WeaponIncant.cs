using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Character.Equipment
{
    public abstract class WeaponIncant : Incant
    {
        public abstract void IncantWeapon(Weapon weapon);
        public abstract void IncantRemove(Weapon weapon);
    }
}