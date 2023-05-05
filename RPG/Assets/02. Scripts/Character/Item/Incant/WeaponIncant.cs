using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character.Equipment;
using RPG.Character.Status;

public class WeaponIncant : Incant
{
    public int attackDamage;
    public float attackSpeed;
    public float attackRange;
    public float movementSpeed;
    public float criticalChance;
    public float criticalDamage;
    public float attackChance;

    public WeaponIncant(WeaponIncantData data) : base(data)
    {
        attackDamage = data.attackDamage;
        attackSpeed = data.attackSpeed;
        attackRange = data.attackRange;
        movementSpeed = data.movementSpeed;
        criticalChance = data.criticalChance;
        criticalDamage = data.criticalDamage;
        attackChance = data.movementSpeed;
    }

    public virtual void AttackEvent(BattleStatus player, BattleStatus enemy)
    {
        Debug.Log("AttackEvent is Nothing");
    }

    public override string GetAddDesc()
    {
        string returnStr = "";
        if (attackDamage > 0)
        {
            returnStr += $"���ݷ�(+{attackDamage})";
        }

        if (attackSpeed > 0)
        {
            returnStr += $"���ݼӵ�(+{attackSpeed})";
        }

        if (attackRange > 0)
        {
            returnStr += $"���ݹ���(+{attackRange})";
        }

        if (movementSpeed > 0)
        {
            returnStr += $"�̵��ӵ�(+{movementSpeed})";
        }

        if (criticalChance > 0)
        {
            returnStr += $"ġ��Ÿ ���߷�(+{criticalChance * 100}%)";
        }

        if (criticalDamage > 0)
        {
            returnStr += $"ġ��Ÿ ������(+{criticalDamage * 100}%)";
        }

        if (attackChance > 0)
        {
            returnStr += $"���߷�(+{attackChance * 100}%)";
        }

        return returnStr;
    }

    public override string GetMinusDesc()
    {
        string returnStr = "";
        if (attackDamage < 0)
        {
            returnStr += $"���ݷ�({attackDamage})";
        }

        if (attackSpeed < 0)
        {
            returnStr += $"���ݼӵ�({attackSpeed})";
        }

        if (attackRange < 0)
        {
            returnStr += $"���ݹ���({attackRange})";
        }

        if (movementSpeed < 0)
        {
            returnStr += $"�̵��ӵ�({movementSpeed})";
        }

        if (criticalChance < 0)
        {
            returnStr += $"ġ��Ÿ ���߷�({criticalChance * 100}%)";
        }

        if (criticalDamage < 0)
        {
            returnStr += $"ġ��Ÿ ������({criticalDamage * 100}%)";
        }

        if (attackChance < 0)
        {
            returnStr += $"���߷�({attackChance * 100}%)";
        }

        return returnStr;
    }
}
