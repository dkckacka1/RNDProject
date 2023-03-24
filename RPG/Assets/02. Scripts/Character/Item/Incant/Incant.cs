using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character.Equipment;

public abstract class Incant
{
    // ������ ���� �� �ִ°�?
    public EquipmentItemType itemType;
    // �����ΰ�? �����ΰ�?
    public IncantType incantType;

    // ��æƮ�� �̸�
    public string name;

    // ��æƮ�� ����
    public string addDesc;
    public string minusDesc;

    public abstract void IncantEquipment(Equipment equipment);
    public abstract void RemoveIncant(Equipment equipment);

    public string ShowDesc(Color baseColor)
    {
        return $"{name}\t{MyUtility.returnColorText(addDesc,Color.green,baseColor)} \n\t\t {MyUtility.returnColorText(minusDesc, Color.red, baseColor)}";
    }
}
