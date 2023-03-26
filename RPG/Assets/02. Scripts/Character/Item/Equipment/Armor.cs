using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    public class Armor : Equipment
    {
        public int hpPoint;
        public int defencePoint;
        public float movementSpeed;
        public float evasionPoint;

        public Armor(ArmorData data) : base(data)
        {
            defencePoint = data.defencePoint;
            hpPoint = data.hpPoint;
            movementSpeed = data.movementSpeed;
            evasionPoint = data.evasionPoint;
        }

        public override void UpdateReinfoce()
        {
            defencePoint = (data as ArmorData).defencePoint + (int)((data as ArmorData).defencePoint * 0.1 * reinforceCount);
            hpPoint = (data as ArmorData).hpPoint + (int)((data as ArmorData).hpPoint * 0.1 * reinforceCount);
            movementSpeed = (data as ArmorData).movementSpeed;
            evasionPoint = (data as ArmorData).evasionPoint;
        }
    } 
}
