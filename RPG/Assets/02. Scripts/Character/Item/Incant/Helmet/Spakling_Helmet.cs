using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    public class Spakling_Helmet : HelmetIncant
    {
        public override void IncantEquipment(Equipment equipment)
        {
            Helmet helmet = equipment as Helmet;

            helmet.EvasionCritical += 0.2f;
            helmet.DefencePoint -= 2;
        }

        public override void RemoveIncant(Equipment equipment)
        {
            Helmet helmet = equipment as Helmet;

            helmet.EvasionCritical -= 0.2f;
            helmet.DefencePoint += 2;
        }
    } 
}
