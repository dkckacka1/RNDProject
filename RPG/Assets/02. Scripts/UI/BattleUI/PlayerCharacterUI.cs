using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.Core;
using RPG.Battle.UI;
using RPG.Character.Status;

public class PlayerCharacterUI : CharacterUI
{
    public override void SetUp()
    {
        base.SetUp();
        hpBar = BattleManager.GetInstance().playerHPBar;
    }

    public override void Init()
    {
        base.Init();
    }
}
