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
        public TierType equipmentTier;
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
                    prefix = incant;
                    break;
                case IncantType.suffix:
                    suffix = incant;
                    break;
            }
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
                    prefix = incant;
                    break;
                case IncantType.suffix:
                    suffix = incant;
                    break;
            }
        }

        public void RemoveAllIncant()
        {
            if (prefix != null)
            {
                prefix = null;
            }

            if (suffix != null)
            {
                suffix = null;
            }
        }

        public bool isIncant()
        {
            return (prefix != null || suffix != null);
        }
        #endregion

        public int GetPrefixIncantID()
        {
            if (prefix == null)
            {
                return -1;
            }

            return prefix.incantID;
        }

        public int GetSuffixIncantID()
        {
            if (suffix == null)
            {
                return -1;
            }

            return suffix.incantID;
        }

        public bool hasAbilitySkill()
        {
            return (hasPrefixAbilitySkill() || hasSuffixAbilitySkill());
        }

        public bool hasPrefixAbilitySkill()
        {
            if (prefix == null) return false;
            if (!prefix.isIncantAbility) return false;

            return true;
        }

        public bool hasSuffixAbilitySkill()
        {
            if (suffix == null) return false;
            if (!suffix.isIncantAbility) return false;

            return true;
        }

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

        public override string ToString()
        {
            return
                $"����̸� : {itemName}\n" +
                $"���Ƽ�� : {equipmentTier}\n" +
                $"������� : {equipmentType}\n" +
                $"������æƮ : {(prefix != null ? prefix.incantName : "����")}\n" +
                $"������æƮ : {(suffix != null ? suffix.incantName : "����")}";
        }

        public string ToStringTier()
        {
            switch (equipmentTier)
            {
                case TierType.Normal:
                    return "�븻";
                case TierType.Rare:
                    return "����";
                case TierType.Unique:
                    return "����ũ";
                case TierType.Legendary:
                    return "����";
            }

            return "";
        }

        public string ToStringEquipmentType()
        {
            switch (equipmentType)
            {
                case EquipmentItemType.Weapon:
                    return "����";
                case EquipmentItemType.Armor:
                    return "����";
                case EquipmentItemType.Pants:
                    return "����";
                case EquipmentItemType.Helmet:
                    return "����";
            }

            return "";
        }
    }

}