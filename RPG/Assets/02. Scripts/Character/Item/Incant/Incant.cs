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
        public string IncantName;

        // ��æƮ�� ����
        public string addDesc;
        public string minusDesc;

        // ��æƮ �� ��ų
        public bool isIncantSkill;
        public int skillID;


        public abstract void IncantEquipment(Equipment equipment);

        public abstract void RemoveIncant(Equipment equipment);

        public string ShowDesc()
        {
            return $"{IncantName}\t{MyUtility.returnColorText(addDesc, Color.green)} \n\t\t {MyUtility.returnColorText(minusDesc, Color.red)}";
        }
    }

}