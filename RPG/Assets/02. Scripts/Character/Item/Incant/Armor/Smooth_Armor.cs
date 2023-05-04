using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    public class Smooth_Armor : ArmorIncant
    {
        public Smooth_Armor(IncantData data) : base(data)
        {
            movementSpeed = 0.5f;
            defencePoint = -1;
        }
    } 
}
