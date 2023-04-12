using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character.Status;

namespace RPG.Character.Equipment
{
    public abstract class Incant : ScriptableObject
    {
        public int incantID;

        // ������ ���� �� �ִ°�?
        public EquipmentItemType itemType;
        // �����ΰ�? �����ΰ�?
        public IncantType incantType;

        // ��æƮ�� �̸�
        public string incantName;

        // ��æƮ�� ����
        public string addDesc;
        public string minusDesc;

        // ��æƮ �� ��ų
        public bool isIncantAbility;
        public string abilityDesc;
        public Sprite abilityIcon;


        public abstract void IncantEquipment(Equipment equipment);

        public abstract void RemoveIncant(Equipment equipment);

        public string ShowDesc()
        {
            return $"{incantName}\t{MyUtility.returnColorText(addDesc, Color.green)} \n\t\t {MyUtility.returnColorText(minusDesc, Color.red)}";
        }
    }

}