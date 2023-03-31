using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Character.Equipment
{
    public abstract class Equipment
    {
        public int reinforceCount = 0;
        public string itemName;
        public EquipmentItemType equipmentType;
        public EquipmentItemTier equipmentTier;
        public string description;

        public EquipmentData data;

        public Incant prefix;
        public Incant suffix;

        public Equipment(Equipment equipment)
        {
            data = equipment.data;
            itemName = equipment.itemName;
            equipmentType = equipment.equipmentType;
            equipmentTier = equipment.equipmentTier;
            description = equipment.description;

            reinforceCount = equipment.reinforceCount;
            prefix = equipment.prefix;
            suffix = equipment.suffix;
        }

        public Equipment(EquipmentData data)
        {
            this.data = data;
            itemName = data.EquipmentName;
            equipmentType = data.equipmentType;
            equipmentTier = data.equipmentTier;
            description = data.description;
        }

        public virtual void ChangeData(EquipmentData data)
        {
            RemoveAllIncant();
            reinforceCount = 0;

            this.data = data;
            itemName = data.EquipmentName;
            equipmentType = data.equipmentType;
            equipmentTier = data.equipmentTier;
            description = data.description;
        }

        #region Incant

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

        public void Incant(Incant incant)
        {
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

        public void RemoveAllIncant()
        {
            if (prefix != null)
            {
                prefix.RemoveIncant(this);
                prefix = null;
            }

            if (suffix != null)
            {
                suffix.RemoveIncant(this);
                suffix = null;
            }
        }

        public bool isIncant()
        {
            return (prefix != null || suffix != null);
        }
        #endregion

        public bool isReinforce()
        {
            return !(reinforceCount == 0);
        }

        public void ReinforceItem()
        {
            reinforceCount++;
        }

        public void RemoveReinforce()
        {
            reinforceCount = 0;
        }

        public void UpdateItem()
        {
            UpdateReinfoce();
            UpdateIncant();
        }

        public void UpdateIncant()
        {
            if (prefix != null)
            {
                prefix.IncantEquipment(this);
            }

            if (suffix != null)
            {
                suffix.IncantEquipment(this);
            }
        }

        public abstract void UpdateReinfoce();

        public override string ToString()
        {
            return 
                $"����̸� : {itemName}\n" +
                $"���Ƽ�� : {equipmentTier}\n" +
                $"������� : {equipmentType}\n" +
                $"������æƮ : {(prefix != null ? prefix.name : "����")}\n" +
                $"������æƮ : {(suffix != null ? suffix.name : "����")}";
        }
    }

}