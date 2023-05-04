using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character.Status;

namespace RPG.Character.Equipment
{
    public abstract class Incant
    {
        public int incantID;

        // ������ ���� �� �ִ°�?
        public EquipmentItemType itemType;
        // ��æƮ Ƽ� ����ΰ�?
        public TierType incantTier;
        // �����ΰ�? �����ΰ�?
        public IncantType incantType;

        // ��æƮ�� �̸�
        public string incantName;

        // ��æƮ �� ��ų
        public bool isIncantAbility;
        public string abilityDesc;
        public Sprite abilityIcon;

        public Incant(IncantData data)
        {
            incantID = data.ID;
            incantType = data.incantType;
            itemType = data.itemType;
            incantTier = data.incantTier;
            incantName = data.incantName;
            isIncantAbility = data.isIncantAbility;
            abilityDesc = data.abilityDesc;
            abilityIcon = data.abilityIcon;
        }
        public abstract string GetAddDesc();
        public abstract string GetMinusDesc();
    }

}