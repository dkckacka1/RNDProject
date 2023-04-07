using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    public class Spakling_Helmet : HelmetIncant
    {
        public Spakling_Helmet()
        {
            incantType = IncantType.prefix;
            itemType = EquipmentItemType.Helmet;
            name = "��¦�̴� ";
            addDesc = "ġ��Ÿ ȸ���� +20%";
            minusDesc = "���� -2";
        }

        public override void IncantEquipment(Equipment equipment)
        {
            Helmet helmet = equipment as Helmet;

            helmet.evasionCritical += 0.2f;
            helmet.defencePoint -= 2;
        }

        public override void RemoveIncant(Equipment equipment)
        {
            Helmet helmet = equipment as Helmet;

            helmet.evasionCritical -= 0.2f;
            helmet.defencePoint += 2;
        }
    } 
}
