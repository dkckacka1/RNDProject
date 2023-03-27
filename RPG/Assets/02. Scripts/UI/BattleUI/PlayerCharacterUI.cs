using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.UI;
using RPG.Character.Status;

public class PlayerCharacterUI : CharacterUI
{
    public override void Initialize(BattleStatus status)
    {
        base.Initialize(status);
        InitHPUI(status.maxHp);
    }
}
