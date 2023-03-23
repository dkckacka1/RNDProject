using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    public class Pants : Equipment
    {
        public int defencePoint;
        public int hpPoint;
        public float movementSpeed;

        public Pants(PantsData data) : base(data)
        {
            defencePoint = data.defencePoint;
            hpPoint = data.hpPoint;
            movementSpeed = data.movementSpeed;
        }
    }
}