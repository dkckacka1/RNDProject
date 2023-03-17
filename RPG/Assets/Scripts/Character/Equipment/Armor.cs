using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    public class Armor : Equipment
    {
        public int defencePoint;
        public int hpPoint;
        public float movementSpeed;
        public float evasionPoint;

        public Armor(ArmorData data) : base(data)
        {
            defencePoint = data.defencePoint;
            hpPoint = data.hpPoint;
            movementSpeed = data.movementSpeed;
            evasionPoint = data.evasionPoint;
        }
    } 
}
