using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    public class Balanced_Helmet : HelmetIncant
    {
        public override void IncantEquipment(Equipment equipment)
        {
            Helmet helmet = equipment as Helmet;

            helmet.DecreseCriticalDamage += 0.2f;
        }

        public override void RemoveIncant(Equipment equipment)
        {
            Helmet helmet = equipment as Helmet;

            helmet.DecreseCriticalDamage -= 0.2f;
        }
    }

}