using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    public class Helmet : Equipment
    {
        public int defencePoint;
        public int hpPoint;
        public float decreseCriticalDamage;
        public float evasionCritical;

        public Helmet(HelmetData data) : base(data)
        {
            defencePoint = data.defencePoint;
            hpPoint = data.hpPoint;
            decreseCriticalDamage = data.decreseCriticalDamage;
            evasionCritical = data.evasionCritical;
        }
    }
}