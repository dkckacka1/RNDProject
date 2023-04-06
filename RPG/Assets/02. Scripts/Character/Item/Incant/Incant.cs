using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character.Equipment;
using RPG.Character.Status;

public abstract class Incant
{
    // 어느장비에 붙을 수 있는가?
    public EquipmentItemType itemType;
    // 접두인가? 접미인가?
    public IncantType incantType;

    // 인챈트의 이름
    public string name;
    public bool isIncantSkill;

    // 인챈트의 설명
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
