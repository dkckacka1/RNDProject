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

        public override void UpdateReinfoce()
        {
            
            defencePoint = (data as HelmetData).defencePoint + (int)((data as HelmetData).defencePoint * 0.1 * reinforceCount);
            hpPoint = (data as HelmetData).hpPoint + (int)((data as HelmetData).hpPoint * 0.1 * reinforceCount);
            decreseCriticalDamage = (data as HelmetData).decreseCriticalDamage;
            evasionCritical = (data as HelmetData).evasionCritical;
        }
    }
}