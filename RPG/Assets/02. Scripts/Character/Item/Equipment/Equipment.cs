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
                Debug.Log("�߸��� ��æƮ ȣ��");
                return;
            }

            // ��æƮ Ÿ�԰� ������ Ÿ���� �´��� Ȯ��
            if (this.equipmentType != incant.itemType)
            {
                Debug.Log("��� Ÿ�԰� ��æƮ ��� Ÿ���� �ٸ��ϴ�.");
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