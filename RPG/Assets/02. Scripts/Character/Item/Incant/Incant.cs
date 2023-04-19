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
        // �����ΰ�? �����ΰ�?
        public IncantType incantType;

        // ��æƮ�� �̸�
        public string incantName;

        // ��æƮ �� ��ų
        public bool isIncantAbility;
        public string abilityDesc;
        public Sprite abilityIcon;
        public abstract string GetAddDesc();
        public abstract string GetMinusDesc();
    }

}