using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character.Equipment;
using RPG.Character.Status;

public abstract class WeaponIncant : Incant
{
    protected WeaponIncant(int incantID) : base(incantID)
    {
    }

    public virtual void AttackEvent(BattleStatus player, BattleStatus enemy)
    {
        Debug.Log("AttackEvent is Nothing");
    }
}
