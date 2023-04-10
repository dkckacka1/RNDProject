using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character.Equipment;
using RPG.Character.Status;

public abstract class Incant
{
    public int incantID;

    // ������ ���� �� �ִ°�?
    public EquipmentItemType itemType;
    // �����ΰ�? �����ΰ�?
    public IncantType incantType;

    // ��æƮ�� �̸�
    public string name;

    // ��æƮ�� ����
    public string addDesc;
    public string minusDesc;

    // ��æƮ �� ��ų
    public bool isIncantSkill;
    public int skillID;

    public Incant(int incantID)
    {
        this.incantID = incantID;
    }

    public abstract void IncantEquipment(Equipment equipment);
    public abstract void RemoveIncant(Equipment equipment);

    public string ShowDesc()
    {
        return $"{name}\t{MyUtility.returnColorText(addDesc,Color.green)} \n\t\t {MyUtility.returnColorText(minusDesc, Color.red)}";
    }
}
