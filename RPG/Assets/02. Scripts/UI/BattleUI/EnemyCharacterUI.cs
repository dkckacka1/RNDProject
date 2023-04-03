using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.UI;
using RPG.Character.Status;

public class EnemyCharacterUI : CharacterUI
{
    public Vector3 hpBarOffset = new Vector3(0, 1.5f, 0);

    public override void SetUp()
    {
        base.SetUp();
        SetUpHPUI();
    }

    public override void Init()
    {
        base.Init();
        this.gameObject.SetActive(true);
    }

    private void LateUpdate()
    {
        UpdateHPBarPosition(transform.position + hpBarOffset);
    }

    public void UpdateHPBarPosition(Vector3 position)
    {
        hpBar.transform.transform.position = Camera.main.WorldToScreenPoint(position);
    }

    public void SetUpHPUI()
    {
        if (battleCanvas == null)
        {
            return;
        }
        hpBar = Instantiate(hpBar, battleCanvas.transform);
    }
}
