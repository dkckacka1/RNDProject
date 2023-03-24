using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

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

        public void Incant(int incantID)
        {
            Incant incant = GameManager.Instance.incantDic[incantID];

            if (incant == null)
            {
                Debug.Log("잘못된 인챈트 호출");
                return;
            }

            // 인챈트 타입과 아이템 타입이 맞는지 확인
            if (this.equipmentType != incant.itemType)
            {
                Debug.Log("장비 타입과 인챈트 장비 타입이 다릅니다.");
                return;
            }

            switch (incant.incantType)
            {
                case IncantType.prefix:
                    if (prefix != null)
                    {
                        prefix.RemoveIncant(this);
                    }
                    prefix = incant;
                    break;
                case IncantType.suffix:
                    if (suffix != null)
                    {
                        suffix.RemoveIncant(this);
                    }
                    suffix = incant;
                    break;
            }

            incant.IncantEquipment(this);
        }

        public bool isIncant()
        {
            return (prefix != null || suffix != null);
        }
    }
}