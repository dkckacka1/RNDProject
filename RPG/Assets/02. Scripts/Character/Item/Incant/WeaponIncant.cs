using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character.Equipment;
using RPG.Character.Status;

public abstract class WeaponIncant : Incant
{
    public abstract void AttackEvent(BattleStatus player, BattleStatus enemy);
}
