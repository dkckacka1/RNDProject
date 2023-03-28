using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    public class EquipmentData : Data
    {
        public string EquipmentName;
        public EquipmentItemType equipmentType;
        public EquipmentItemTier equipmentTier;
        [Space()]
        [TextArea()]
        public string description;
    }

}