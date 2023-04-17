using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character.Equipment;
using RPG.Character.Status;

public abstract class WeaponIncant : Incant
{
    public int attackDamage;
    public float attackSpeed;
    public float attackRange;
    public float movementSpeed;
    public float criticalChance;
    public float criticalDamage;
    public float attackChance;

    public virtual void AttackEvent(BattleStatus player, BattleStatus enemy)
    {
        Debug.Log("AttackEvent is Nothing");
    }

    public override string GetAddDesc()
    {
        string returnStr = "";
        if (attackDamage > 0)
        {
            returnStr += $"공격력(+{attackDamage})";
        }

        if (attackSpeed > 0)
        {
            returnStr += $"공격속도(+{attackSpeed})";
        }

        if (attackRange > 0)
        {
            returnStr += $"공격범위(+{attackRange})";
        }

        if (movementSpeed > 0)
        {
            returnStr += $"이동속도(+{movementSpeed})";
        }

        if (criticalChance > 0)
        {
            returnStr += $"치명타 적중률(+{criticalChance * 100}%)";
        }

        if (criticalDamage > 0)
        {
            returnStr += $"치명타 데미지(+{criticalDamage * 100}%)";
        }

        if (attackChance > 0)
        {
            returnStr += $"적중률(+{attackChance * 100}%)";
        }

        return returnStr;
    }

    public override string GetMinusDesc()
    {
        string returnStr = "";
        if (attackDamage < 0)
        {
            returnStr += $"공격력(-{attackDamage})";
        }

        if (attackSpeed < 0)
        {
            returnStr += $"공격속도(-{attackSpeed})";
        }

        if (attackRange < 0)
        {
            returnStr += $"공격범위(-{attackRange})";
        }

        if (movementSpeed < 0)
        {
            returnStr += $"이동속도(-{movementSpeed})";
        }

        if (criticalChance < 0)
        {
            returnStr += $"치명타 적중률(-{criticalChance * 100}%)";
        }

        if (criticalDamage < 0)
        {
            returnStr += $"치명타 데미지(-{criticalDamage * 100}%)";
        }

        if (attackChance < 0)
        {
            returnStr += $"적중률(-{attackChance * 100}%)";
        }

        return returnStr;
    }
}
