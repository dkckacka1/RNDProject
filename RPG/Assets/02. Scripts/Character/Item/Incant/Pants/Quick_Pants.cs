using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    public class Quick_Pants : PantsIncant
    {
        public Quick_Pants(IncantData data) : base(data)
        {
            movementSpeed = 1.5f;
        }
    }
}