using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    public class Balanced_Helmet : HelmetIncant
    {
        public Balanced_Helmet(int incantID) : base(incantID)
        {
            incantType = IncantType.suffix;
            itemType = EquipmentItemType.Helmet;
            name = "������ ";
            addDesc = "ġ��Ÿ ���� ���� +20%";
        }

        public override void IncantEquipment(Equipment equipment)
        {
            Helmet helmet = equipment as Helmet;

            helmet.decreseCriticalDamage += 0.2f;
        }

        public override void RemoveIncant(Equipment equipment)
        {
            Helmet helmet = equipment as Helmet;

            helmet.decreseCriticalDamage -= 0.2f;
        }
    }

}