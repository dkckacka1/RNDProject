using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    public class Equipment
    {
        int reinforceCount = 0;
        public string itemName;
        public EquipmentItemType equipmentType;
        public EquipmentItemTier equipmentTier;
        public string description;

        public Incant prefix;
        public Incant suffix;

        public Equipment(EquipmentData data)
        {
            itemName = data.EquipmentName;
            equipmentType = data.equipmentType;
            equipmentTier = data.equipmentTier;
            description = data.description;
        }
    }
}