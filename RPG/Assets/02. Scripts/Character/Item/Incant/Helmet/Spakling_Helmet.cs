using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    public class Spakling_Helmet : HelmetIncant
    {
        public Spakling_Helmet(IncantData data) : base(data)
        {
            evasionCritical = 0.2f;
            defencePoint = -2;
        }
    } 
}
