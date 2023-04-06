using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character.Status;
using RPG.Character.Equipment;

public class Stone_Weapon : Incant
{
    public Stone_Weapon()
    {
        incantType = IncantType.suffix;
        itemType = EquipmentItemType.Weapon;
        name = "µπµ¢¿Ã";
        addDesc = "∞¯∞› Ω√ µπµ¢¿Ã ≈ı√¥";
        minusDesc = "";
        isIncantSkill = true;
    }

    public override void IncantEquipment(Equipment equipment)
    {
    }

    public override void RemoveIncant(Equipment equipment)
    {
    }

    public override void Skill(BattleStatus status)
    {
        base.Skill(status);
        Debug.Log(status.name + "ø°∞‘ µπµ¢¿Ã ∞¯∞›!!");
    }
}
