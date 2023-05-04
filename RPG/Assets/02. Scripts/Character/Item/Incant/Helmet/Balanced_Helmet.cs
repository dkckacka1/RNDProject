using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    public class Balanced_Helmet : HelmetIncant
    {
        public Balanced_Helmet(IncantData data) : base(data)
        {
            decreseCriticalDamage = 0.2f;
        }
    }
}