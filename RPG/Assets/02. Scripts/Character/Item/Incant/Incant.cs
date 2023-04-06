using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character.Equipment;
using RPG.Character.Status;

public abstract class Incant
{
    // ������ ���� �� �ִ°�?
    public EquipmentItemType itemType;
    // �����ΰ�? �����ΰ�?
    public IncantType incantType;

    // ��æƮ�� �̸�
    public string name;
    public bool isIncantSkill;

    // ��æƮ�� ����
    public string addDesc;
    public string minusDesc;

    public abstract void IncantEquipment(Equipment equipment);
    public abstract void RemoveIncant(Equipment equipment);

    public string ShowDesc()
    {
        return $"{name}\t{MyUtility.returnColorText(addDesc,Color.green)} \n\t\t {MyUtility.returnColorText(minusDesc, Color.red)}";
    }

    public virtual void Skill(BattleStatus status)
    {

    }
}
