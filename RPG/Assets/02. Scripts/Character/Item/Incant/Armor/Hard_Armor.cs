using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    public class Hard_Armor : ArmorIncant
    {
        public Hard_Armor(IncantData data) : base(data)
        {
            defencePoint = 3;
            hpPoint = 100;
            movementSpeed = -1;
        }
    }

}