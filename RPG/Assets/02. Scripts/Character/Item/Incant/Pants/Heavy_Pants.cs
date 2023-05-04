using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    public class Heavy_Pants : PantsIncant
    {
        public Heavy_Pants(IncantData data) : base(data)
        {
            defencePoint = 4;
            movementSpeed = -3;
        }
    }

}