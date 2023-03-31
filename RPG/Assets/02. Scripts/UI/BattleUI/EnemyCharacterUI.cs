using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.UI;
using RPG.Character.Status;

public class EnemyCharacterUI : CharacterUI
{
    public Vector3 hpBarOffset = new Vector3(0, 1.5f, 0);

    public override void Initialize(BattleStatus battleStatus)
    {
        base.Initialize(battleStatus);
        CreateHPUI();
        InitHPUI(battleStatus.status.maxHp);
    }

    private void LateUpdate()
    {
        SetHpBarPosition(transform.position + hpBarOffset);
    }

    public void SetHpBarPosition(Vector3 position)
    {
        hpBar.transform.transform.position = Camera.main.WorldToScreenPoint(position);
        //hpBarUI.hpSlider.transform.position = Camera.main.WorldToScreenPoint(position);
    }

    public void CreateHPUI()
    {
        hpBar = Instantiate(hpBar, battleCanvas.transform);
    }
}
